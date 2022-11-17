using System;
using System.IO;
using System.Collections.Generic;

namespace feladat1
{
    internal class Program
    {
        static string szamok = "";
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

            szamok = r.ReadLine();
            r.Close();
          } catch (IOException e) {
            Console.WriteLine(e.Message);
          }
        }

        static void Zeros() {
          int db = 0;
          foreach(char c in szamok) {
            if (c == '0') db++;
          }
          Console.WriteLine($"a. feladat) {db} db 0 szám van a fájlban.");
        }

        static List<int> diff_nums = new();
        static void FourDigitNums() {
          for (int i = 0; i < szamok.Length-3; i++) {
            int szam = int.Parse(szamok.Substring(i, 4));
            if (!diff_nums.Contains(szam) && szam.ToString().Length == 4) diff_nums.Add(szam);
          }
          Console.WriteLine($"b. feladat) {diff_nums.Count} db különböző négyjegyű szám van a fájlban.");
        }

        static void PrimeNums() {
          int primek = 0;
          foreach (int szam in diff_nums) {
            if (isPrime(szam)) primek++;
          }
          Console.WriteLine($"c. feladat) {primek} db négyjegyű prím szám van a fájlban.");
        }

        static bool isPrime(int num) {
          for (int i = 2; i < num; i++) {
            if (num % i == 0) return false;
          }
          return true;
        }
    }
}
