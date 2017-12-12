using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace AdventOfCode2017
{
    class Program
    {
        public static string path;

        static void Main(string[] args)
        {
            path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Day day;
            string introduction = File.ReadAllText(path + "/Resources/introduction.txt");
            string input;

            Start:

            Console.Clear();
            Console.Write(introduction);

            AwaitInput:
            input = Console.ReadLine();

            switch (input.ToUpper())
            {
                case "1.1":
                    day = new Day1(1);
                    break;
                case "1.2":
                    day = new Day1(2);
                    break;

                case "END":
                    goto End;

                default:
                    goto Start;
            }

            day.Start();

            goto AwaitInput;

            End:
            Console.Write("");
        }
    }
}
