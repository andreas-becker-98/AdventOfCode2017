using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day2
    {
        public static void Run(string[] args)
        {
            string[] rows = File.ReadAllLines(Program.path + "/Resources/day2.txt");
            string[] row;
            int checksum = 0;
            int curVal;

            switch (args[0])
            {
                case "1":

                    int minVal;
                    int maxVal;

                    for (int i = 0; i < rows.Length; i++)
                    {
                        row = rows[i].Split('\t');

                        minVal = int.MaxValue;
                        maxVal = int.MinValue;

                        for (int j = 0; j < row.Length; j++)
                        {
                            curVal = int.Parse(row[j]);
                            if (curVal > maxVal)
                            {
                                maxVal = curVal;
                            }

                            if (curVal < minVal)
                            {
                                minVal = curVal;
                            }
                        }

                        checksum += (maxVal - minVal);
                    }

                    break;

                case "2":

                    int otherVal;
                    int checksumDelta;

                    for (int i = 0; i < rows.Length; i++)
                    {
                        row = rows[i].Split('\t');
                        checksumDelta = 0;

                        for (int j = 0; j < row.Length - 1; j++)
                        {
                            if ((j + 1) == row.Length)
                            {
                                break;
                            }

                            curVal = int.Parse(row[j]);

                            for (int k = (j + 1); k < row.Length; k++)
                            {
                                otherVal = int.Parse(row[k]);

                                if ((curVal % otherVal) == 0)
                                {
                                    checksumDelta = curVal / otherVal;
                                    break;
                                }
                                if ((otherVal % curVal) == 0)
                                {
                                    checksumDelta = otherVal / curVal;
                                    break;
                                }
                            }

                            if (checksumDelta > 0)
                            {
                                checksum += checksumDelta;
                                break;
                            }
                        }
                    }
                    break;
            }

            Console.Write(string.Format("Output:\t{0}\n\n", checksum));
        }
    }
}
