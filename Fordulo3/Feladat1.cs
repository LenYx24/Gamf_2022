using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Fordulo3
{
  internal class Feladat1 {
    public static void Start() {
      Console.WriteLine("1. feladat:");
      Read();
      DifferentWords();
      ValVel();
      WordLengthStat();
    }

    static List<string> lines = new();
    static void Read() {
      try {
        StreamReader r = new("szoveg.txt");

        while (!r.EndOfStream) {
          lines.Add(r.ReadLine().Trim());
        }
        
        r.Close();
      } catch (IOException e) {
        Console.WriteLine(e.Message);
      }
    }

    static List<string> diffwords = new();
    static void DifferentWords() {
      foreach (string line in lines) {
        string[] words = line.Split(' ');
        foreach (string word in words) {
          if (!diffwords.Contains(word)) diffwords.Add(word);
        }
      }
      Console.WriteLine($"a) {diffwords.Count} különböző szó van a szövegben");
            if (diffwords.Contains(" ")) Console.WriteLine("VAN BENNE ÜRES");
    }

    static void ValVel() {
      int db = 0;
            foreach (string word in diffwords)
            {
                if (word.EndsWith("VAL") || word.EndsWith("VEL") && word.Length > 3)
                {
                    db++;
                }
            }
            Console.WriteLine($"b) {db} db szó végződik a val/vel raggal.");
    }

    static Dictionary<int, int> wordlen = new();
    static void WordLengthStat() {
      foreach (string line in lines) {
        string[] words = line.Split(' ');
        foreach (string word in words) {
          if (!wordlen.ContainsKey(word.Length)) wordlen.Add(word.Length, 1);
          else wordlen[word.Length] += 1;
        }
      }
      Dictionary<int, int> ordered = wordlen.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
      Console.WriteLine($"c) {ordered.First().Key};{ordered.First().Value} ({ordered.First().Key} betű hosszúságú szavakból van a legtöbb [{ordered.First().Value}])");
    }
  }
}