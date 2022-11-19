using System;
using System.IO;
using System.Collections.Generic;

namespace feladat1
{
    internal class Program
    {
        static string nums = "";
        static void Main(string[] args)
        {
            ReadIn();
            Zeros();
            FourDigitNums();
            PrimeNums();
        }

        static void ReadIn() {
          try {
            StreamReader r = new("szamok.txt");

            nums = r.ReadLine();
            r.Close();
          } catch (IOException e) {
            Console.WriteLine(e.Message);
          }
        }

        //Counting all the zeros in the string
        static void Zeros() {
          int db = 0;
          foreach(char c in nums) {
            if (c == '0') db++;
          }
          Console.WriteLine($"a. feladat) {db} db 0 szám van a fájlban.");
        }

        //Counting all the different four digit numbers in the string and saving it in a list for further operations
        static List<int> diff_nums = new();
        static void FourDigitNums() {
          for (int i = 0; i < nums.Length-3; i++) {
            int num = int.Parse(nums.Substring(i, 4));
            if (!diff_nums.Contains(num) && num.ToString().Length == 4) diff_nums.Add(num);
          }
          Console.WriteLine($"b. feladat) {diff_nums.Count} db különböző négyjegyű szám van a fájlban.");
        }

        //Checking which numbers present in the list is a prime number
        static void PrimeNums() {
          int primek = 0;
          foreach (int num in diff_nums) {
            if (isPrime(num)) primek++;
          }
          Console.WriteLine($"c. feladat) {primek} db négyjegyű prím szám van a fájlban.");
        }

        //This is a method for simplifying the code
        static bool isPrime(int num) {
          for (int i = 2; i < num / 2; i++) {
            if (num % i == 0) return false;
          }
          return true;
        }
    }
}
