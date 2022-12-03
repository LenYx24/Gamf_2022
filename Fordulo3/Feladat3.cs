using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Numerics;

namespace Fordulo3
{
    internal class Feladat3
    {
        public static void Start()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("3. feladat:");
            string fajl1 = File.ReadAllText("szamok_a.txt");
            string fajl2 = File.ReadAllText("szamok_b.txt");
            // a) rész
            List<int> nemSzerepelAFajl1ben = nemSzereploSzamok(fajl1);
            List<int> nemSzerepelAFajl2ben = nemSzereploSzamok(fajl2);
            int f1 = 1;
            string s = string.Join(' ', nemSzerepelAFajl1ben);
            System.Console.WriteLine("fajl1: " + s);
            s = string.Join(' ', nemSzerepelAFajl2ben);
            System.Console.WriteLine("fajl2: " + s);
            List<int> ugyanazAKetFajlban = nemSzerepelAFajl1ben.Intersect(nemSzerepelAFajl2ben).ToList();
            if (ugyanazAKetFajlban.Count == 0) f1 = 0;
            else
            {
                foreach (int k in ugyanazAKetFajlban) f1 = f1 * k;
            }
            Console.WriteLine("a) 1. eset: " + f1);
            f1 = 1;
            foreach (int k in nemSzerepelAFajl1ben) f1 = f1 * k;
            foreach (int k in nemSzerepelAFajl2ben) f1 = f1 * k;
            Console.WriteLine("a) 2. eset: " + f1);
            // b) rész
            string b = bfeladat(fajl1, fajl2);
            Console.WriteLine("b) " + b);
            string tesztb = bfeladat("17259", "85239");
            Console.WriteLine("b) " + tesztb);

            // c) rész
            uint szorzat = cfeladat(fajl1 + fajl2);
            Console.WriteLine("c) " + szorzat.ToString("##,#"));

            // console helyreállítása
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
        }
        static List<int> nemSzereploSzamok(string f)
        {
            List<int> ered = new();
            for (int i = 10; i < 20; i++) ered.Add(i);
            for (int i = 0; i < f.Count() - 1; i++)
            {
                int szam = Convert.ToInt32(f.Substring(i, 2));
                if (10 <= szam && szam < 20 && ered.Contains(szam)) ered.Remove(szam);
            }
            return ered;
        }
        static List<string> osszesKepezhetoSzam(string f)
        {
            List<string> ered = new();
            for (int i = f.Count(); i > 0; i--)
            {
                for (int j = 0; j <= f.Count() - i; j++)
                {
                    string szam = f.Substring(j, i);
                    ered.Add(szam);
                }
            }
            return ered;
        }
        static string bfeladat(string f1, string f2)
        {
            List<string> s1 = osszesKepezhetoSzam(f1);
            List<string> s2 = osszesKepezhetoSzam(f2);
            int k = 1;
            for (int i = 0; i < s1.Count(); i += k-1)
            {
                List<string> s1resze = s1.GetRange(i, k);
                List<string> s2resze = s2.GetRange(i, k);
                if(k < 5)
                {
                    //Console.WriteLine(String.Join(';',s1resze));
                    //Console.WriteLine();
                }
                List<string> ered = s1resze.Intersect(s2resze).ToList();
                if (ered.Count() == 1) return ered[0];
                else if (ered.Count() > 1)
                {
                    // a nagyobb számot visszaadni
                    List<BigInteger> bg = new();
                    foreach(string e in ered)
                    {
                        bg.Add(BigInteger.Parse(e));
                    }
                    //Console.WriteLine(string.Join(' ',ered));
                    return bg.Max().ToString();
                }
                k++;
            }
            return "";
        }
        static uint cfeladat(string f)
        {
            uint max = 1;
            for(int i = 0; i < f.Count() - 9; i++)
            {
                string k = f.Substring(i, 10);
                int ind = k.IndexOf('0');
                if(ind != -1)
                {
                    i += ind;
                    continue;
                }
                uint szorzat = 1;
                foreach(char szam in k)
                {
                    szorzat *= Convert.ToUInt32(szam.ToString());
                }
                //Console.WriteLine(szorzat);
                if (max < szorzat) max = szorzat;
            }
            return max;
        }
    }
}