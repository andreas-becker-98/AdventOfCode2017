using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day6
    {
        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            string[] input = File.ReadAllText(Program.path + "/Resources/day6.txt").Split('\t');
            int[] memoryBanks = new int[input.Length];
            int reallocations = 0;

            for (int i = 0; i < input.Length; i++)
            {
                memoryBanks[i] = int.Parse(input[i]);
            }

            List<string> previousConfigs = new List<string>();
            int blocksToReallocate = 0;
            int reallocationStart = 0;
            int index;
            int loopSize = 0;
            string arrayVal;

            while (true)
            {
                reallocationStart = 0;

                for (int i = 0; i < memoryBanks.Length; i++)
                {
                    if (memoryBanks[i] > blocksToReallocate)
                    {
                        blocksToReallocate = memoryBanks[i];
                        reallocationStart = i;
                    }
                }

                memoryBanks[reallocationStart] = 0;
                index = (reallocationStart + 1) % memoryBanks.Length;

                while (blocksToReallocate > 0)
                {
                    blocksToReallocate--;
                    memoryBanks[index]++;
                    index++;
                    index %= memoryBanks.Length;
                }

                reallocations++;
                blocksToReallocate = 0;

                arrayVal = "";
                foreach (int i in memoryBanks)
                {
                    arrayVal += i.ToString() + "\t";
                }

                if (previousConfigs.Contains(arrayVal.Trim()))
                {
                    loopSize = reallocations - previousConfigs.IndexOf(arrayVal.Trim());
                    loopSize--;
                    break;
                }

                previousConfigs.Add(arrayVal.Trim());
            }

            Console.Write(string.Format("Output:\t{0}\n\n", isPart2 ? loopSize : reallocations));
        }
    }
}
