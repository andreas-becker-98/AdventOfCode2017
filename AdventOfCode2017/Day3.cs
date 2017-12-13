using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2017
{
    class Day3
    {
        public static void Run(string[] args)
        {
            int input = int.Parse(File.ReadAllText(Program.path + "/Resources/day3.txt"));

            switch (args[0])
            {
                case "1":
                    Part1(input);
                    break;

                case "2":
                    Part2(input);
                    break;
            }
        }

        public static void Part1(int input)
        {
            // DISCLAIMER:
            // IT MAY SPIT OUT THE RIGHT ANSWER BUT THE MATH IS WRONG

            Vector pos = new Vector(1, -1);
            int temp = 0;
            int ring = -1;
            int comparison = 0;

            while (comparison < input)
            {
                ring++;
                comparison = (int)Math.Pow((2 * ring - 1), 2);

                //Console.Write(ring + "  " + comparison + "\n");
            }

            int quarter = 2 * ring - 1;
            int temp2 = quarter;
            temp = comparison;
            pos.Mult(ring);
            Vector dir = new Vector(-1, 0);

            while (temp != input)
            {
                pos += dir;

                temp2--;
                temp--;
                if (temp2 == 0)
                {

                    dir.Rotate90DegreesClockwise();
                    temp2 = quarter;
                }
            }

            Console.Write(string.Format("Output:\t{0}\n\n", pos.Manhattan()));
        }

        public static void Part2(int input)
        {
            Vector dir = new Vector(1, 0);
            Vector pos = new Vector(0, 0);

            int curStep = 0, maxStep = 1, dirChange = 0;
            int curValue = 0, mostRecentValue = 0;

            int graphValue = 0;

            Dictionary<Vector, int> pointGraph = new Dictionary<Vector, int>
            {
                { pos, 1 }
            };

            while (mostRecentValue < input)
            {
                curValue = 0;
                pos += dir;
                curStep++;

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }

                        if (pointGraph.TryGetValue(pos + new Vector(x, y), out graphValue))
                        {
                            curValue += graphValue;
                        }
                    }
                }

                mostRecentValue = curValue;
                pointGraph.Add(pos, curValue);

                if (curStep >= maxStep)
                {
                    dir.Rotate90DegreesAnticlockwise();
                    curStep = 0;
                    dirChange++;
                }
                if (dirChange >= 2)
                {
                    dirChange = 0;
                    maxStep++;
                }
            }

            Console.Write(string.Format("Output:\t{0}\n\n", mostRecentValue));
        }

        class Vector
        {
            public int x, y;

            public static Vector operator+ (Vector v1, Vector v2)
            {
                Vector v = new Vector(v1.x + v2.x, v1.y + v2.y);
                return v;
            }

            public Vector(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public void Mult(int multiplier)
            {
                x *= multiplier;
                y *= multiplier;
            }

            public void Rotate90DegreesAnticlockwise()
            {
                int tempX = x, tempY = y;
                x = tempX * (int)Math.Cos(Math.PI / 2) - tempY * (int)Math.Sin(Math.PI / 2);
                y = tempX * (int)Math.Sin(Math.PI / 2) + tempY * (int)Math.Cos(Math.PI / 2);
            }
            public void Rotate90DegreesClockwise()
            {
                int tempX = x, tempY = y;
                x = tempX * (int)Math.Cos(Math.PI / 2) + tempY * (int)Math.Sin(Math.PI / 2);
                y = - tempX * (int)Math.Sin(Math.PI / 2) + tempY * (int)Math.Cos(Math.PI / 2);
            }

            public int Manhattan()
            {
                return Math.Abs(x) + Math.Abs(y);
            }

            public override string ToString()
            {
                return "(" + x + ", " + y + ")";
            }

            public override bool Equals(object obj)
            {
                // pos + new Vector(x, y)
                return ((Vector)obj).x == x && ((Vector)obj).y == y;
            }

            public override int GetHashCode()
            {
                var hashCode = 1502939027;
                hashCode = hashCode * -1521134295 + x.GetHashCode();
                hashCode = hashCode * -1521134295 + y.GetHashCode();
                return hashCode;
            }
        }
    }
}
