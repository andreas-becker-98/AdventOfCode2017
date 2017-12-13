using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day5
    {
        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            string[] input = File.ReadAllLines(Program.path + "/Resources/day5.txt");
            int[] instructions = new int[input.Length];
            int steps = 0;
            int offset;

            int index = 0;

            for (int i = 0; i < input.Length; i++)
            {
                instructions[i] = int.Parse(input[i]);
            }

            while (index >= 0 && index < instructions.Length)
            {
                offset = instructions[index];
                instructions[index] += (isPart2 && (offset >= 3)) ? (-1) : 1;

                index += offset;
                steps++;
            }

            Console.Write(string.Format("Output:\t{0}\n\n", steps));
        }
    }
}
