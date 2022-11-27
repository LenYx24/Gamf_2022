using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fordulo2
{
    internal class Feladat1
    {
      public static void Start() {
            Console.WriteLine("a) "+ BalPrime());
            Console.WriteLine("b) "+ bfeladat());
            Console.WriteLine("c) "+ cfeladat());
        }

        static int BalPrime() {
            int db = 0;
        for (int i = 11; i <= 100; i++) {
          int num = i;
          while (isPrime(num) && num > 9) {
            num = int.Parse(num.ToString().Substring(1, num.ToString().Length-1));
          }
                if (num < 10 && isPrime(num))
                {
                    db++;
                    //Console.WriteLine(i); debug
                }
        }
            return db;
      }
        static int bfeladat()
        {
            for (int i = 300000; i > 100000; i--)
            {
                int num = i;
                while (isPrime(num) && num > 9)
                {
                    num = int.Parse(num.ToString().Substring(1, num.ToString().Length - 1));
                }
                if (num < 10 && isPrime(num))
                {
                    return i;
                }
            }
            return 0;
        }
        static int cfeladat()
        {
            int db = 0;
            for (int i = 10000; i <= 99999; i++)
            {
                if (isPrime(i))
                {
                    string num = i.ToString();
                    for (int k = 0; k < num.Length; k++)
                    {
                        if (isOneDigitPrime(int.Parse(num.Substring(k, 1)))) db++;
                    }
                }
                
            }
            return db;
        }

        static bool isPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
        static bool isOneDigitPrime(int num)
        {
            if (num == 2 || num == 3 || num == 5 || num == 7) return true;
            return false;
        }
    }
}
