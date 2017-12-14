using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day10
    {
        // --- Part Two ---

        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            string input = File.ReadAllText(Program.path + "/Resources/day10.txt");

            if (!isPart2)
            {
                Part1(input);
            }
            else
            {
                Part2(input);
            }
        }

        public static void Part1(string input)
        {
            string[] data = input.Split(',');
            int[] lengths = new int[data.Length];
            int arraySize = 256;
            int[] array = new int[arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                array[i] = i;
            }
            for (int i = 0; i < data.Length; i++)
            {
                lengths[i] = int.Parse(data[i]);
            }

            int index = 0;
            int length;
            int skipSize = 0;

            int[] subArray;

            for (int i = 0; i < lengths.Length; i++)
            {
                length = lengths[i];
                subArray = new int[length];

                for (int j = 0; j < length; j++)
                {
                    subArray[j] = array[(index + j) % arraySize];
                }
                Reverse(ref subArray);
                for (int j = 0; j < length; j++)
                {
                    array[(index + j) % arraySize] = subArray[j];
                }

                index += length + skipSize;
                skipSize++;
                index %= arraySize;
            }

            int product = array[0] * array[1];
            Console.Write(string.Format("Output:\t{0}\n\n", product));
        }

        public static void Part2(string input)
        {
            string input2 = "17,31,73,47,23";
            int[] bonusLengths = new int[input2.Length];
            List<byte> lengths = new List<byte>();

            int arraySize = 256;
            int[] array = new int[arraySize];

            lengths.AddRange(Encoding.ASCII.GetBytes(input));
            lengths.AddRange(new byte[] { 17, 31, 73, 47, 23 });

            for (int i = 0; i < arraySize; i++)
            {
                array[i] = i;
            }

            int index = 0;
            int length;
            int skipSize = 0;

            int[] subArray;

            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < lengths.Count; j++)
                {
                    length = lengths[j];
                    subArray = new int[length];

                    for (int k = 0; k < length; k++)
                    {
                        subArray[k] = array[(index + k) % arraySize];
                    }
                    Reverse(ref subArray);
                    for (int k = 0; k < length; k++)
                    {
                        array[(index + k) % arraySize] = subArray[k];
                    }

                    index += length + skipSize;
                    skipSize++;
                    index %= arraySize;
                }
            }

            string hexString = "";
            int[] denseHash = new int[16];

            for (int i = 0; i < 16; i++)
            {
                denseHash[i] = array[i * 16];

                for (int j = 1; j < 16; j++)
                {
                    denseHash[i] ^= array[(i * 16) + j];
                }

                hexString += denseHash[i].ToString("X2");
            }

            Console.Write(string.Format("Output:\t{0}\n\n", hexString));
            File.WriteAllText(Program.path + "/Resources/day10_outputP2.txt", hexString.ToLower());
        }

        public static void Reverse(ref int[] array)
        {
            if (array.Length == 0 || array.Length == 1)
                return;

            int end = array.Length - 1;
            int i1;
            int i2;

            for (int i = 0; i <= end / 2; i++)
            {
                i1 = array[i];
                i2 = array[end - i];
                array[i] = i2;
                array[end - i] = i1;
            }
        }
    }
}
