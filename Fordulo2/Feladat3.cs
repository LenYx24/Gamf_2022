using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fordulo2
{
    internal class Feladat3
    {
      static List<string> nums = new();
      static List<double> debug = new();

      public static void Start() {
        ReadIn();
        Devidable();
        EndingDevision();
        Repeating();
      }

      public static void Devidable() {
        int db = 0;
        foreach(string num in nums) {
          int n = int.Parse(num);
          if (n % 317 == 0) db++;
        }
        Console.WriteLine($"1) {db} szam osztható 317-tel.");
      }
      public static void EndingDevision() {
        int db = 0;

      }

      public static void Repeating() {
        decimal n = int.Parse(nums[0]);

        int rem = (int)n % 317 < 317 ? (((int)n % 317)*10) : (int)n % 317;
        Console.WriteLine($"{rem}");
      }

      public static void ReadIn() {
        try {
          StreamReader r = new("szamok2.txt");

          while (!r.EndOfStream) {
            nums.Add(r.ReadLine());
          }
          r.Close();
        } catch (IOException e) {
          Console.WriteLine(e.Message);
        }
      }
    }
}
