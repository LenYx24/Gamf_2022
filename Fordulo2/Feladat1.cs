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

      }

      public void BalPrime() {
        for (int i = 11; i <= 100; i++) {
          int num = i;
          while (isPrime(num) && num > 10) {
            num = int.Parse(num.ToString().Substring(1, num.ToString().Length));
          }
        }
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
