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
                    Console.WriteLine(i);
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
                        if (isPrime(int.Parse(num.Substring(k, 1)))) db++;
                    }
                }
                
            }
            return db;
        }

        static bool isPrime(int num) {
          if (num <= 1) return false;
          for (int i = 2; i < num / 2; i++) {
            if (num % i == 0) return false;
          }
          return true;
        }
    }
}
