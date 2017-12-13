using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day9
    {
        static List<Group> groups;

        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";
            string input = File.ReadAllText(Program.path + "/Resources/day9.txt");
            groups = new List<Group>();
            int garbageChars = 0;

            int index = 0;
            char c;
            Group scope = Group.Null;
            bool currentlyGarbage = false;

            while (index < input.Length)
            {
                c = input[index];

                if (currentlyGarbage)
                {
                    if (c == '>')
                    {
                        currentlyGarbage = false;
                    }
                    else if (c == '!')
                    {
                        index++;
                    }
                    else
                    {
                        garbageChars++;
                    }
                }
                else
                {
                    switch (c)
                    {
                        case '{':
                            scope = new Group(scope);
                            break;
                        case '}':
                            scope = scope.parentGroup;
                            break;
                        case '<':
                            currentlyGarbage = true;
                            break;
                        case '!':
                            index++;
                            break;
                        default:
                            break;
                    }
                }

                index++;
            }

            int sumAllScores = Group.ReturnAllScore();
            Console.Write(string.Format("Output:\t{0}\n\n", isPart2 ? garbageChars : sumAllScores));
        }

        public class Group
        {
            public Group parentGroup;
            public int score;

            public static Group Null
            {
                get { return new Group() { score = 0 }; }
            }

            public Group()
            {
                groups.Add(this);
            }

            public Group(Group parent)
            {
                parentGroup = parent;
                if (parent == Null)
                {
                    score = 1;
                }
                else
                {
                    score = parent.score + 1;
                }

                groups.Add(this);
            }

            public static int ReturnAllScore()
            {
                int returnValue = 0;

                foreach (Group g in groups)
                {
                    returnValue += g.score;
                }

                return returnValue;
            }

            public override bool Equals(object obj)
            {
                var group = obj as Group;
                return group != null &&
                       EqualityComparer<Group>.Default.Equals(parentGroup, group.parentGroup) &&
                       score == group.score;
            }

            public override int GetHashCode()
            {
                var hashCode = -436242503;
                hashCode = hashCode * -1521134295 + EqualityComparer<Group>.Default.GetHashCode(parentGroup);
                hashCode = hashCode * -1521134295 + score.GetHashCode();
                return hashCode;
            }
        }
    }
}
