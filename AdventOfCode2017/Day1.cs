using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day1 : Day
    {
        private bool isPartTwo;
        private string captcha;

        public Day1(int part)
        {
            isPartTwo = part != 1;
            captcha = File.ReadAllText(Program.path + "/Resources/day1.txt");
        }

        public override void Start()
        {
            isRunning = true;

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

            isRunning = false;
        }
    }
}
