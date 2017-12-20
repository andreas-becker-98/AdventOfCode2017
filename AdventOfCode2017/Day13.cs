using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day13
    {
        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            string[] input = File.ReadAllLines(Program.path + "/Resources/day13.txt");

            Layer[] layers = new Layer[100];

            int totalSeverity = 0;
            int delays = 10;

            for (int i = 0; i < input.Length; i++)
            {
                Layer l = new Layer(input[i]);
                layers[l.depth] = l;
            }

            if (isPart2)
            {
                bool getsCaught = true;

                while (getsCaught)
                {
                    getsCaught = GetsCaught(layers, delays);
                    delays++;
                }
            }
            else
            {
                totalSeverity = GetTotalSeverity(layers);
            }
            

            Console.Write(string.Format("Output:\t{0}\n\n", isPart2 ? --delays : totalSeverity));

        }

        static int GetTotalSeverity(Layer[] layers)
        {
            int output = 0;

            for (int currentPosition = 0; currentPosition < 100; currentPosition++)
            {
                if (layers[currentPosition] != null)
                {
                    if (layers[currentPosition].scannerPosition == 0)
                    {
                        output += currentPosition * layers[currentPosition].range;
                    }
                }

                foreach (Layer l in layers.Where(x => x != null))
                {
                    l.Step();
                }
            }

            return output;
        }

        static bool GetsCaught(Layer[] layers, int delayedBy = 0)
        {
            for (int i = 0; i < delayedBy; i++)
            {
                foreach (Layer l in layers.Where(x => x != null))
                {
                    l.Step();
                }
            }

            for (int currentPosition = 0; currentPosition < 100; currentPosition++)
            {
                if (layers[currentPosition] != null)
                {
                    if (layers[currentPosition].scannerPosition == 0)
                    {
                        Console.WriteLine(" >> " + delayedBy + " -- " + currentPosition);
                        return true;
                    }
                }

                foreach (Layer l in layers.Where(x => x != null))
                {
                    l.Step();
                }
            }

            return false;
        }

        class Layer
        {
            public int depth;
            public int range;
            public int scannerPosition = 0;
            public int moveDir = 1;

            public Layer(string data)
            {
                string[] subData = data.Split(':');
                depth = int.Parse(subData[0].Trim());
                range = int.Parse(subData[1].Trim());
                moveDir = 1;
            }

            public void Step()
            {
                scannerPosition += moveDir;

                if ((scannerPosition + moveDir) < 0 || (scannerPosition + moveDir) >= range)
                {
                    moveDir *= -1;
                }
            }
        }
    }
}
