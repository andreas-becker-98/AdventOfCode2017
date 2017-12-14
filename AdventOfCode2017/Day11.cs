using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day11
    {
        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            string[] input = File.ReadAllText(Program.path + "/Resources/day11.txt").Split(',');

            int x = 0, y = 0, z = 0;
            int greatestDistance = 0;

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case "n":
                        y++;
                        z--;
                        break;
                    case "ne":
                        x++;
                        z--;
                        break;
                    case "nw":
                        y++;
                        x--;
                        break;
                    case "s":
                        y--;
                        z++;
                        break;
                    case "sw":
                        x--;
                        z++;
                        break;
                    case "se":
                        y--;
                        x++;
                        break;
                }

                if (greatestDistance < (((int)Math.Abs(x) + (int)Math.Abs(y) + (int)Math.Abs(z)) / 2))
                {
                    greatestDistance = (((int)Math.Abs(x) + (int)Math.Abs(y) + (int)Math.Abs(z)) / 2);
                }
            }

            int distance;

            distance = ((int)Math.Abs(x) + (int)Math.Abs(y) + (int)Math.Abs(z)) / 2;

            Console.Write(string.Format("Output:\t{0}\n\n", isPart2 ? greatestDistance : distance));
        }
    }
}
