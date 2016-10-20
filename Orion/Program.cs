using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            Console.WriteLine("Done!");
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

            List<char[]> variantsList = GetTernaryVariants(n - 1);
            StringBuilder stringToCheck = new StringBuilder();

            foreach (var variantArray in variantsList)
            {
                stringToCheck.Clear().Append(givenDigitsLine[0]);
                int pos = 1;

                foreach (var variant in variantArray)
                {
                    switch (variant)
                    {
                        case '1':
                            stringToCheck.Append('+');
                            break;
                        case '2':
                            stringToCheck.Append('-');
                            break;
                    }
                    stringToCheck.Append(givenDigitsLine[pos]);
                    pos++;
                }

                if (CheckIfEqualsTo100(stringToCheck.ToString()))
                {
                    Console.WriteLine(stringToCheck.ToString());
                }

            }

            return rezult;
        }

        private static List<char[]> GetTernaryVariants(int i)
        {
            List<char[]> resultList = new List<char[]>();

            char[] maxValue = new char[i];
            for (int j = 0; j < i; j++)
            {
                maxValue[j] = '2';
            }

            char[] rezult = new char[i];
            for (int j = 0; j < i; j++)
            {
                rezult[j] = '0';
            }
            resultList.Add(rezult.ToArray());

            do
            {
                resultList.Add(rezult.ToArray());
                rezult = TernaryInc(rezult, i-1);
            } while (rezult != null);


            return resultList;
        }

        private static char[] TernaryInc(char[] rezult, int i)
        {
            if (i >= rezult.Length)
            {
                i = rezult.Length - 1;
            }

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
                    {
                        return null;
                    }

                    rezult[i] = '0';
                    TernaryInc(rezult, i - 1);
                    break;
            }

            return rezult;
        }

        private static bool CheckIfEqualsTo100(string s)
        {
            double rez = 0;
            StringBuilder currentNum = new StringBuilder();

            foreach (var chr in s)
            {
                switch (chr)
                {
                    case '+':
                        if (currentNum.Length > 0)
                            rez += double.Parse(currentNum.ToString());
                        currentNum.Clear();
                        break;
                    case '-':
                        rez += double.Parse(currentNum.ToString());
                        currentNum.Clear();
                        currentNum.Append('-');
                        break;
                    default:
                        currentNum.Append(chr);
                        break;
                }
            }
            rez += double.Parse(currentNum.ToString());

            if (rez == 100)
            {
                Console.WriteLine(s);
                return true;
            }


            return false;
        }
    }
}