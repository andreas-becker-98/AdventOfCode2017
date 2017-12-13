using System;
using System.IO;

namespace AdventOfCode2017
{
    class Day1
    {
        public static void Run(string[] args)
        {
            bool isPartTwo = args[0] == "2";
            string captcha = File.ReadAllText(Program.path + "/Resources/day1.txt");
            int length = captcha.Length;
            int step = isPartTwo ? length / 2 : 1;
            int sum = 0;

            for (int i = 0; i < length; ++i)
            {
                if (captcha[i] == captcha[(i + step) % length])
                {
                    sum += captcha[i] - '0';
                }
            }

            Console.Write(string.Format("Output:\t{0}\n\n", sum));
        }
    }
}
