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
                    Day1.Run(new string[] { "1" });
                    goto AwaitInput;
                case "1.2":
                    Day1.Run(new string[] { "2" });
                    goto AwaitInput;
                case "2.1":
                    Day2.Run(new string[] { "1" });
                    goto AwaitInput;
                case "2.2":
                    Day2.Run(new string[] { "2" });
                    goto AwaitInput;


                case "END":
                    goto End;

                default:
                    goto Start;
            }

        End:
            Console.Write("");
        }
    }
}
