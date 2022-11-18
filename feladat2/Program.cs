using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace feladat2
{
    class Sor {
        public string Rendszám;
        public TimeSpan Behajtás_Időpont;
        public TimeSpan Kihajtás_Időpont;
        public int ÚtAzonosító;

        public Sor(string sor) {
            string[] S = sor.Split("\t");

            Rendszám = S[0];
            Behajtás_Időpont = new(int.Parse(S[1]), int.Parse(S[2]), 0);
            Kihajtás_Időpont = new(int.Parse(S[3]), int.Parse(S[4]), 0);
            ÚtAzonosító = int.Parse(S[5]);
        }
    }

    class Autó {
        public string Rendszám;
        public List<TimeSpan> Behajtás_Időpontok;
        public List<TimeSpan> Kihajtás_Időpontok;
        public List<int> ÚtAzonosítók;

        public Autó(string Rendszám) {
            this.Rendszám = Rendszám;
            Behajtás_Időpontok = new();
            Kihajtás_Időpontok = new();
            ÚtAzonosítók = new();
        }
    }

    internal class Program
    {
        public static List<Sor> Sorok = new();
        public static readonly int ÚtHossz = 10;

        static void Main(string[] args)
        {
            int darab;
            string fájlnév = "forgalom.txt";
            StreamReader fájlbeolvasóváltozó = new(fájlnév);

            while (!fájlbeolvasóváltozó.EndOfStream)
                Sorok.Add(new(fájlbeolvasóváltozó.ReadLine()));

            fájlbeolvasóváltozó.Close();

            // a) feladat
            darab = 0;
            TimeSpan dél = new(12, 0, 0);
            foreach (Sor elem in Sorok)
                if (elem.Behajtás_Időpont <= dél && elem.Kihajtás_Időpont >= dél)
                    darab++;
            Console.WriteLine($"a) {darab}");

            // b) feladat
            darab = 0;
            foreach (Sor elem in Sorok) {
                TimeSpan ÚtIdő;

                if (elem.Kihajtás_Időpont > elem.Behajtás_Időpont)
                    ÚtIdő = elem.Kihajtás_Időpont - elem.Behajtás_Időpont;
                else
                    ÚtIdő = elem.Kihajtás_Időpont + (new TimeSpan(24, 0, 0) - elem.Behajtás_Időpont);

                if (ÚtHossz/ÚtIdő.TotalHours >= 100)
                    darab++;
            }
            Console.WriteLine($"b) {darab}");

            // c) feladat
            List<Autó> Autók = new();
            foreach (Sor elem in Sorok) {
                int jelenlegiGépjárműIndex = Autók.FindIndex(x => x.Rendszám == elem.Rendszám);

                if (jelenlegiGépjárműIndex == -1) {
                    jelenlegiGépjárműIndex = Autók.Count;
                    Autók.Add(new(elem.Rendszám));
                }
                
                Autók[jelenlegiGépjárműIndex].Behajtás_Időpontok.Add(elem.Behajtás_Időpont);
                Autók[jelenlegiGépjárműIndex].Kihajtás_Időpontok.Add(elem.Kihajtás_Időpont);
                Autók[jelenlegiGépjárműIndex].ÚtAzonosítók.Add(elem.ÚtAzonosító);
            }

            darab = 0;
            Console.WriteLine("C feladat tesztek:\n------------------");
            List<Autó> hibasAutok = new();
            foreach (Autó Gépjármű in Autók)
            {
                for (int i = 0; i < Gépjármű.ÚtAzonosítók.Count - 1; i++)
                {
                    if (Gépjármű.ÚtAzonosítók[i] != Gépjármű.ÚtAzonosítók[i + 1] && Gépjármű.Kihajtás_Időpontok[i] > Gépjármű.Behajtás_Időpontok[i + 1])
                    {
                        hibasAutok.Add(Gépjármű);
                        Console.WriteLine(new String('-',10));
                        Console.WriteLine($"\tRendszám: {Gépjármű.Rendszám}");
                        for(int j = 0; j < Gépjármű.Behajtás_Időpontok.Count; j++)
                        {
                            string s = j == i || j == i+1 ? "  <--" : "";
                            Console.WriteLine($"{Gépjármű.Behajtás_Időpontok[j]} : {Gépjármű.Kihajtás_Időpontok[j]}" + s);
                        }
                        Console.WriteLine(new String('-', 10));
                        //Gépjármű.Behajtás_Időpontok.ForEach(x=>Console.WriteLine($"Behajtási: {x}"));
                        //Gépjármű.Kihajtás_Időpontok.ForEach(x=>Console.WriteLine($"Kihajtási: {x}"));
                        //Console.WriteLine($"\tBe_idő: {Gépjármű.Behajtás_Időpontok[i]} Ki_idő: {Gépjármű.Kihajtás_Időpontok[i]}");
                        //Console.WriteLine($"\tBe_idő: {Gépjármű.Behajtás_Időpontok[i+1]} Ki_idő: {Gépjármű.Kihajtás_Időpontok[i+1]}");
                        //Console.WriteLine($"\tÚtszakasz_ID1: {Gépjármű.ÚtAzonosítók[i]}");
                        //Console.WriteLine($"\tÚtszakasz_ID2: {Gépjármű.ÚtAzonosítók[i+1]}");
                        darab++;
                    }
                }
            }
            Console.WriteLine($"c) {darab}");
            //int k = megoldas2(Autók,hibasAutok);
            //Console.WriteLine($"c (második megoldási módszer): {k}");
            // másik megoldás BUG:
            // A megoldás kettő megszámolja hibásan azt is, hogyha éfjél után hajt ki és éfjél előtt hajt be, mert be > ki
        }
        //class IdőBeírás
        //{
        //    public TimeSpan Ido;
        //    public bool Tipus; // false behajtási, true kihajtási
        //    public IdőBeírás(TimeSpan ido, bool tipus)
        //    {
        //        Ido = ido;
        //        Tipus = tipus;
        //    }
        //}
        //static int megoldas2(List<Autó> Autók)
        //{
        //    int s = 0;
        //    foreach(Autó a in Autók)
        //    {
        //        List<IdőBeírás> idobeirasok = new();
        //        a.Behajtás_Időpontok.ForEach(x => idobeirasok.Add(new(x, false)));
        //        a.Kihajtás_Időpontok.ForEach(x => idobeirasok.Add(new(x, true)));
        //        idobeirasok = idobeirasok.OrderBy(x => x.Ido).ToList();
        //        bool vane = false;
        //        for(int i = 0; i < idobeirasok.Count; i++)
        //        {
        //            if(i%2 == 0 && idobeirasok[i].Tipus == true || i % 2 == 1 && idobeirasok[i].Tipus == false)
        //            {
        //                vane = true;
        //            }
                    
        //        }
        //        if (vane)
        //        {
        //            s++;
        //            Console.WriteLine(new String('-', 10));
        //            Console.WriteLine($"\tRendszám: {a.Rendszám}");
        //            for (int j = 0; j < a.Behajtás_Időpontok.Count; j++)
        //            {
        //                Console.WriteLine($"{a.Behajtás_Időpontok[j]} : {a.Kihajtás_Időpontok[j]}");
        //            }
        //            Console.WriteLine(new String('-', 10));
        //        }
        //    }
        //    return s;
        //}
    }
}
