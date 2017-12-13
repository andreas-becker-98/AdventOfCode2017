using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day7
    {
        static Dictionary<string, TowerProgram> towerPrograms = new Dictionary<string, TowerProgram>();

        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            string[] input = File.ReadAllLines(Program.path + "/Resources/day7.txt");
            string[] keys;

            towerPrograms = new Dictionary<string, TowerProgram>();

            TowerProgram temp;
            TowerProgram origin = new TowerProgram();

            for (int i = 0; i < input.Length; i++)
            {
                temp = Parse(input[i]);
                towerPrograms.Add(temp.name, temp);
            }

            keys = towerPrograms.Keys.ToArray();

            for (int i = 0; i < keys.Length; i++)
            {
                temp = towerPrograms[keys[i]];

                if (temp.programsAbove == null)
                {
                    continue;
                }

                for (int j = 0; j < temp.programsAbove.Length; j++)
                {
                    towerPrograms[temp.programsAbove[j]].SetBelow(temp.name);
                }
            }


            for (int i = 0; i < keys.Length; i++)
            {
                if (string.IsNullOrEmpty(towerPrograms[keys[i]].programBelow))
                {
                    origin = towerPrograms[keys[i]];
                    break;
                }
            }

            if (!isPart2)
            {
                Console.Write(string.Format("Output:\t{0}\n\n", origin.name));
                return;
            }

            int correctWeight = 0;

            origin.CalculateOwnStackWeight();
            string unbalanced = origin.FindUnbalanced(ref correctWeight);
            Console.Write(string.Format("Output:\t{0}\n\n", correctWeight));
        }

        public class TowerProgram
        {
            public string name;
            public int weight;
            public int ownStackWeight = 0;
            public string programBelow;
            public string[] programsAbove;

            public void SetBelow(string programBelow)
            {
                this.programBelow = programBelow;
            }

            public void CalculateOwnStackWeight()
            {
                ownStackWeight = weight;

                if (programsAbove == null)
                {
                    return;
                }

                for (int i = 0; i < programsAbove.Length; i++)
                {
                    if (towerPrograms[programsAbove[i]].ownStackWeight == 0)
                    {
                        towerPrograms[programsAbove[i]].CalculateOwnStackWeight();
                    }

                    ownStackWeight += towerPrograms[programsAbove[i]].ownStackWeight;
                }
            }

            public string FindUnbalanced(ref int idealValue)
            {
                TowerProgram prev;
                TowerProgram curr;
                TowerProgram next;

                int l = programsAbove.Length;
                bool equalsPrev;
                bool equalsNext;

                for (int i = 0; i < programsAbove.Length; i++)
                {
                    prev = towerPrograms[programsAbove[(i + l - 1) % l]];
                    curr = towerPrograms[programsAbove[i]];
                    next = towerPrograms[programsAbove[(i + 1) % l]];

                    equalsPrev = prev.ownStackWeight == curr.ownStackWeight;
                    equalsNext = next.ownStackWeight == curr.ownStackWeight;

                    if (equalsPrev)
                    {
                        if (equalsNext)
                        {
                            continue;
                        }
                        else
                        {
                            idealValue = prev.ownStackWeight;
                            return next.FindUnbalanced(ref idealValue);
                        }
                    }
                    else
                    {
                        if (equalsNext)
                        {
                            idealValue = curr.ownStackWeight;
                            return prev.FindUnbalanced(ref idealValue);
                        }
                        else
                        {
                            idealValue = prev.ownStackWeight;
                            return curr.FindUnbalanced(ref idealValue);
                        }
                    }
                }

                idealValue = weight + idealValue - ownStackWeight;
                return name;
            }
        }

        public static TowerProgram Parse(string input)
        {
            TowerProgram tp = new TowerProgram();
            string[] data = input.Split(' ');

            tp.name = data[0];
            tp.weight = int.Parse(data[1].Substring(1, data[1].Length - 2));

            if (data.Length >= 3)
            {
                // This TowerProgram carries other TowerPrograms
                tp.programsAbove = new string[data.Length - 3];

                for (int i = 0; i < data.Length - 3; i++)
                {
                    if (data[i + 3].Contains(','))
                    {
                        data[i + 3] = data[i + 3].Replace(',', ' ').Trim();
                    }

                    tp.programsAbove[i] = data[i + 3];
                }
            }

            return tp;
        }
    }
}
