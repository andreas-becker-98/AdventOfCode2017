using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day8
    {
        static Dictionary<string, int> variables;
        static int highestValueHeld;

        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            string[] input = File.ReadAllLines(Program.path + "/Resources/day8.txt");
            variables = new Dictionary<string, int>();
            highestValueHeld = int.MinValue;
            Instruction[] instructions = new Instruction[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                instructions[i] = Instruction.Parse(input[i]);
            }

            for (int i = 0; i < input.Length; i++)
            {
                instructions[i].Run();
            }

            List<int> varValues = variables.Values.ToList();
            int max = int.MinValue;
            int index = -1;

            for (int i = 0; i < varValues.Count; i++)
            {
                if (varValues[i] > max)
                {
                    index = i;
                    max = varValues[i];
                }
            }

            Console.Write(string.Format("Output:\t{0}\n\n", !isPart2 ? max : highestValueHeld));
        }

        public class Instruction
        {
            public string variableName;
            public bool isIncrementing;
            public int offsetAmount;
            public delegate bool Condition();
            public Condition condition;

            public static Instruction Parse(string text)
            {
                Instruction output = new Instruction();
                string[] info = text.Split(' ');

                // njb dec 964 if mr >= -2

                output.variableName = info[0];
                output.isIncrementing = info[1] == "inc";
                output.offsetAmount = int.Parse(info[2]);

                int intResult;

                switch (info[5])
                {
                    case "==":

                        output.condition = () =>
                            (variables[info[4]] == (int.TryParse(info[6], out intResult) ? intResult : variables[info[6]]));

                        break;
                    case "!=":

                        output.condition = () =>
                            (variables[info[4]] != (int.TryParse(info[6], out intResult) ? intResult : variables[info[6]]));

                        break;
                    case "<=":

                        output.condition = () =>
                            (variables[info[4]] <= (int.TryParse(info[6], out intResult) ? intResult : variables[info[6]]));

                        break;
                    case "<":

                        output.condition = () =>
                            (variables[info[4]] < (int.TryParse(info[6], out intResult) ? intResult : variables[info[6]]));

                        break;
                    case ">=":

                        output.condition = () =>
                            (variables[info[4]] >= (int.TryParse(info[6], out intResult) ? intResult : variables[info[6]]));

                        break;
                    case ">":

                        output.condition = () =>
                            (variables[info[4]] > (int.TryParse(info[6], out intResult) ? intResult : variables[info[6]]));

                        break;
                }

                if (!variables.ContainsKey(output.variableName))
                {
                    variables.Add(output.variableName, 0);
                }

                return output;
            }

            public void Run()
            {
                if (condition.Invoke())
                {
                    variables[variableName] += isIncrementing ? offsetAmount : -offsetAmount;
                    if (highestValueHeld < variables[variableName])
                        highestValueHeld = variables[variableName];
                }
            }
        }
    }
}
