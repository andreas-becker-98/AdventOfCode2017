using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day12
    {
        static string[] input;
        static List<HashSet<int>> groups;

        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            input = File.ReadAllLines(Program.path + "/Resources/day12.txt");

            groups = new List<HashSet<int>>();

            for (int i = 0; i < input.Length; i++)
            {
                StartGroup(i);
            }

            Console.Write(string.Format("Output:\t{0}\n\n", isPart2 ? groups.Count : groups[0].Count));
        }

        public static void StartGroup(int index)
        {
            HashSet<int> group = groups.Find(x => x.Contains(index));
            if (group == null)
            {
                group = new HashSet<int>();

                BuildGroup(index, ref group);

                groups.Add(group);
            }
            
            // Else we will just do nothing, as this index is already in one of the groups.
        }

        public static void BuildGroup(int index, ref HashSet<int> group)
        {
            string[] data = input[index].Split(' ');
            int connectionValue;

            group.Add(index);

            foreach (string s in data.Skip(2).ToArray())
            {
                connectionValue = int.Parse(s.Replace(',', ' ').Trim());

                if (!group.Contains(connectionValue))
                {
                    BuildGroup(connectionValue, ref group);
                }
            }
        }
    }
}
