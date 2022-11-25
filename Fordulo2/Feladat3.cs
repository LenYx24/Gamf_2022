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

      public static void Start() {
        ReadIn();
        Divisible();
        EndingDevision();
        Repeating();
      }

      public static void Divisible() {
        int db = 0;
        foreach(string num in nums) {
          int n = int.Parse(num);
          if (n % 317 == 0) db++;
        }
        Console.WriteLine($"a) {db} szam osztható 317-tel.");
      }
      public static void EndingDevision() {
        int db = 0;

        foreach (string num in nums) {
          double n = double.Parse(num);
          if (n % 612 == 0) continue;

          double tizedes = n / (double)612;

          if (tizedes.ToString().Length < 15) {
            db++;
          }
        }

        Console.WriteLine($"b) {db} olyan véges tizedes van ami osztható 612-vel.");
      }

      public static void Repeating() {
        int n = int.Parse(nums[0]);
        double rem = n % 317 < 317 ? ((n % 317)*10) : n % 317;
        string str = "";

        for (int i = 0; i < 1000; i++) {
          str += Math.Floor(rem / 317);
          rem = rem % 317 < 317 ? ((rem % 317)*10) : rem % 317;
        }
        string[] strs = str.Split(str.Substring(0, 4));

        Console.WriteLine($"c) {str.Substring(0, 4)}{strs[1]} a tizedes utáni szakasz.");
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
