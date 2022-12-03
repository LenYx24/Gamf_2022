using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Reflection;

namespace Fordulo3
{
    class Koordinata
    {
        public int X;
        public int Y;
        public int Z;

        public Koordinata(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public void BeallítasokXYZ(Koordinata Irany)
        {
            this.X += Irany.X;
            this.Y += Irany.Y;
            this.Z += Irany.Z;
        }
        public bool Egyenlo(Koordinata a)
        {
            if (a.X == this.X && a.Y == this.Y && a.Z == this.Z)
            {
                return true;
            }
            return false;
        }
    }

    class AzUt
    {
        public int Hossz;
        public Koordinata Kezdo_csucs;
        public Koordinata[] Irany;

        public AzUt(string Sor)
        {
            List<string> Pont = new() { "A", "B", "C", "D", "E", "F", "G", "H" };

            Koordinata[] Csucs = {
                new(0, 0, 0), // A
                new(1, 0, 0), // B
                new(1, 1, 0), // C
                new(0, 1, 0), // D
                new(0, 0, 1), // E
                new(1, 0, 1), // F
                new(1, 1, 1), // G
                new(0, 1, 1)  // H
            };
            Koordinata[] Irany = {
                new(0, 0, 0),  // Marad
                new(1, 0, 0),  // Jobbra
                new(0, 1, 0),  // Előre
                new(0, 0, 1),  // Fel
                new(-1, 0, 0), // Balra
                new(0, -1, 0), // Hátra
                new(0, 0, -1)  // Le
            };

            string[] Egyeni = Sor.Split(" ");

            Hossz = int.Parse(Egyeni[0]);

            int index = Pont.IndexOf(Egyeni[1]);
            Kezdo_csucs = new(Csucs[index].X, Csucs[index].Y, Csucs[index].Z);

            this.Irany = new Koordinata[Hossz];
            for (int i = 0; i < Hossz; i++)
                this.Irany[i] = Irany[int.Parse(Egyeni[2 + i])];
        }
    }

    internal class Feladat2_magyar
    {
        public static List<AzUt> MindenUt = new();

        public static void Start()
        {
            Console.WriteLine("2. feladat:");

            FajlOlvasas();
            Munka_A();
            Munka_B();
            Munka_C();
        }

        public static void FajlOlvasas()
        {
            StreamReader Fajl = new("utak.txt");

            while (!Fajl.EndOfStream)
                MindenUt.Add(new(Fajl.ReadLine()));

            Fajl.Close();
        }

        public static bool Megvalosithato(AzUt item)
        {
            Koordinata Jelenlegi_pont = new(item.Kezdo_csucs.X, item.Kezdo_csucs.Y, item.Kezdo_csucs.Z);
            foreach(Koordinata irany in item.Irany)
            {
                Jelenlegi_pont.BeallítasokXYZ(irany);

                if (Jelenlegi_pont.X < 0 || Jelenlegi_pont.X > 1 || Jelenlegi_pont.Y < 0 || Jelenlegi_pont.Y > 1 || Jelenlegi_pont.Z < 0 || Jelenlegi_pont.Z > 1)
                    return false;
            }
            return true;
        }

        // Jó megoldások keresése
        public static void Munka_A()
        {
            int darab = 0;
            foreach (AzUt item in MindenUt)
            {
                if (Megvalosithato(item)) darab++;
            }

            Console.WriteLine($"a) {darab}");
        }

        // Összes csúcson járt
        public static void Munka_B()
        {
            int darab = 0;

            foreach (AzUt Sor in MindenUt)
            {
                List<Koordinata> Csucsok = new() {
                    new(0, 0, 0), // A
                    new(1, 0, 0), // B
                    new(1, 1, 0), // C
                    new(0, 1, 0), // D
                    new(0, 0, 1), // E
                    new(1, 0, 1), // F
                    new(1, 1, 1), // G
                    new(0, 1, 1)  // H
                };
                Koordinata c = new(Sor.Kezdo_csucs.X, Sor.Kezdo_csucs.Y, Sor.Kezdo_csucs.Z);
                foreach(Koordinata irany in Sor.Irany)
                {
                    Koordinata cs1 = null;
                    foreach(Koordinata k in Csucsok)
                    {
                        if (k.Egyenlo(c)) cs1 = k;
                    }
                    if (cs1 != null)
                        Csucsok.Remove(cs1);

                    c.BeallítasokXYZ(irany);
                }
                Koordinata cs = null;
                foreach (Koordinata k in Csucsok)
                {
                    if (k.Egyenlo(c)) cs = k;
                }
                if (cs != null)
                    Csucsok.Remove(cs);

                if (Megvalosithato(Sor) && Csucsok.Count == 0)
                    darab++;
                
            }

            Console.WriteLine($"b) {darab}");
        }

        // Összes élen járt
        public static void Munka_C()
        {
            int darab = 0;

            foreach (AzUt item in MindenUt)
            {
                List<Koordinata> Csucs1 = new() {
                    new(0, 0, 0),
                    new(1, 0, 0),
                    new(1, 1, 0),
                    new(0, 1, 0),
                    new(0, 0, 1),
                    new(1, 0, 1),
                    new(1, 1, 1),
                    new(0, 1, 1),
                    new(0, 0, 0),
                    new(1, 0, 0),
                    new(1, 1, 0),
                    new(0, 1, 0)
                };
                List<Koordinata> Csucs2 = new() {
                    new(1, 0, 0),
                    new(1, 1, 0),
                    new(0, 1, 0),
                    new(0, 0, 0),
                    new(1, 0, 1),
                    new(1, 1, 1),
                    new(0, 1, 1),
                    new(0, 0, 1),
                    new(0, 0, 1),
                    new(1, 0, 1),
                    new(1, 1, 1),
                    new(0, 1, 1)
                };
                Koordinata c = new(item.Kezdo_csucs.X, item.Kezdo_csucs.Y, item.Kezdo_csucs.Z);
                Koordinata p = new(0, 0, 0);

                foreach(Koordinata irany in item.Irany)
                {
                    p = new(c.X, c.Y, c.Z);
                    c.BeallítasokXYZ(irany);
                    int index = -1;
                    for (int j = 0; j < Csucs1.Count; j++)
                    {
                        if (Csucs1[j].Egyenlo(c)
                            && Csucs2[j].Egyenlo(p)
                            ||
                            Csucs1[j].Egyenlo(p)
                            && Csucs2[j].Egyenlo(c)
                            )
                        {
                            index = j;
                            break;
                        }
                    }
                    if (index != -1)
                    {
                        Csucs1.RemoveAt(index);
                        Csucs2.RemoveAt(index);
                    }
                }

                if (Megvalosithato(item) && Csucs1.Count == 0)
                    darab++;
            }

            Console.WriteLine($"c) {darab}");
        }
    }
}