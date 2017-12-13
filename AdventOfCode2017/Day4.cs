using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AdventOfCode2017
{
    class Day4
    {
        public static void Run(string[] args)
        {
            bool isPart2 = args[0] == "2";

            string[] input = File.ReadAllLines(Program.path + "/Resources/day4.txt");
            int numberOfValidPassphrases = input.Length;

            string[] passphrase;
            HashSet<string> checkedPassphrases;

            char[] currentPassword;

            for (int i = 0; i < input.Length; i++)
            {
                checkedPassphrases = new HashSet<string>();
                passphrase = input[i].Split(' ');

                for (int j = 0; j < passphrase.Length; j++)
                {
                    if (isPart2)
                    {
                        //Sort passphrases before use
                        currentPassword = passphrase[j].ToArray();

                        Quicksort(ref currentPassword, 0, currentPassword.Length - 1);

                        passphrase[j] = string.Concat(currentPassword);
                    }

                    if (!checkedPassphrases.Add(passphrase[j]))
                    {
                        numberOfValidPassphrases--;
                        break;
                    }
                }
            }

            Console.Write(string.Format("Output:\t{0}\n\n", numberOfValidPassphrases));
        }

        public static void Quicksort(ref char[] A, int lo, int hi)
        {
            if (lo < hi)
            {
                int p = Partition(ref A, lo, hi);
                Quicksort(ref A, lo, p - 1);
                Quicksort(ref A, p + 1, hi);
            }
        }

        public static int Partition(ref char[] A, int lo, int hi)
        {
            char pivot = A[hi];
            int i = lo - 1;
            char temp;

            for (int j = lo; j <= hi - 1; j++)
            {
                if (A[j] < pivot)
                {
                    i += 1;
                    temp = A[j];
                    A[j] = A[i];
                    A[i] = temp;
                }
            }

            if (A[hi] < A[i + 1])
            {
                temp = A[hi];
                A[hi] = A[i + 1];
                A[i + 1] = temp;
            }

            return i + 1;
        }
    }
}
