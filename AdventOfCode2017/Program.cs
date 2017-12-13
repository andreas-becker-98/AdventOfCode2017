using System;
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
            string[] input;

        Start:

            Console.Clear();
            Console.Write(introduction);

        AwaitInput:
            try
            {
                input = Console.ReadLine().Split('.');
            }
            catch
            {
                goto Start;
            }

            switch (input[0].ToUpper())
            {
                case "1":
                    Day1.Run(new string[] { input[1] });
                    goto AwaitInput;
                case "2":
                    Day2.Run(new string[] { input[1] });
                    goto AwaitInput;
                case "3":
                    Day3.Run(new string[] { input[1] });
                    goto AwaitInput;
                case "4":
                    Day4.Run(new string[] { input[1] });
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
