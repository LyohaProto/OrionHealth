using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orion
{
    class Program
    {
        static void Main()
        {
            int n;
            string s = Console.ReadLine();

            if (!int.TryParse(s, out n))
                return;

            List<string> solutions = Solve(n);

            solutions.Sort();

            foreach (var str in solutions)
            {
                Console.WriteLine(str);
            }

            Console.ReadLine();
        }

        private static List<string> Solve(int n)
        {
            List<string> rezult = new List<string>();

            List<int> givenDigitsLine = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                givenDigitsLine.Add(i);
            }

            // Maximum value of the array of all possible substitutions ("", "+", "-")
            char[] maxSubstValue = new char[n - 1];
            for (int j = 0; j < n - 1; j++)
            {
                maxSubstValue[j] = '2';
            }

            // Start with array filled with 0
            char[] currentSubstValue = new char[n - 1];
            for (int j = 0; j < n - 1; j++)
            {
                currentSubstValue[j] = '0';
            }

            StringBuilder digitsLineToCheck = new StringBuilder();

            // Iterate through the all substitution variants
            do
            {
                digitsLineToCheck.Clear().Append(givenDigitsLine[0]);
                int pos = 1;

                foreach (var substVariant in currentSubstValue.ToArray())
                {
                    switch (substVariant)
                    {
                        case '1':
                            digitsLineToCheck.Append('+');
                            break;
                        case '2':
                            digitsLineToCheck.Append('-');
                            break;
                    }
                    digitsLineToCheck.Append(givenDigitsLine[pos]);
                    pos++;
                }

                if (CheckIfEqualsTo100(digitsLineToCheck.ToString()))
                {
                    rezult.Add(digitsLineToCheck.ToString());
                    // Uncomment to make waiting for the big numbers like 12+ not so boring
                    // Console.WriteLine(digitsLineToCheck.ToString());
                }

                currentSubstValue = TernaryInc(currentSubstValue, n - 2);
            } while (currentSubstValue != null);

            Console.Clear();

            return rezult;
        }

        // Increment the char[] in ternary way: 0, 1, 2, 10, 11, 12, 20...
        private static char[] TernaryInc(char[] rezult, int i)
        {
            switch (rezult[i])
            {
                case '0':
                    rezult[i] = '1';
                    break;
                case '1':
                    rezult[i] = '2';
                    break;
                case '2':
                    if (rezult[0] == '2')
                        return null;

                    rezult[i] = '0';
                    TernaryInc(rezult, i - 1);
                    break;
            }

            return rezult;
        }

        private static bool CheckIfEqualsTo100(string s)
        {
            long rez = 0;
            StringBuilder currentNum = new StringBuilder();

            foreach (var chr in s)
            {
                switch (chr)
                {
                    case '+':
                        if (currentNum.Length > 0)
                            rez += long.Parse(currentNum.ToString());
                        currentNum.Clear();
                        break;
                    case '-':
                        rez += long.Parse(currentNum.ToString());
                        currentNum.Clear();
                        currentNum.Append('-');
                        break;
                    default:
                        currentNum.Append(chr);
                        break;
                }
            }
            rez += long.Parse(currentNum.ToString());

            if (rez == 100)
            {
                return true;
            }


            return false;
        }
    }
}