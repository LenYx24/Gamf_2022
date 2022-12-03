using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Fordulo3
{
    class 座標 {
        public int X;
        public int Y;
        public int Z;

        public 座標(int x, int y, int z) {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public void 設定XYZ(座標 方向) {
            this.X += 方向.X;
            this.Y += 方向.Y;
            this.Z += 方向.Z;
        }
    }

    class 道路 {
        public int 長さ;
        public 座標 開始ピーク;
        public 座標[] 進行方向;

        public 道路(string 行) {
            List<string> ポイント = new() { "A", "B", "C", "D", "E", "F", "G", "H" };

            座標[] ピーク = {
                new(0, 0, 0), // A
                new(1, 0, 0), // B
                new(1, 1, 0), // C
                new(0, 1, 0), // D
                new(0, 0, 1), // E
                new(1, 0, 1), // F
                new(1, 1, 1), // G
                new(0, 1, 1)  // H
            };
            座標[] 方向 = {
                new(0, 0, 0),  // Marad
                new(1, 0, 0),  // Jobbra
                new(0, 1, 0),  // Előre
                new(0, 0, 1),  // Fel
                new(-1, 0, 0), // Balra
                new(0, -1, 0), // Hátra
                new(0, 0, -1)  // Le
            };

            string[] 個 = 行.Split(" ");

            this.長さ = int.Parse(個[0]);

            int 索引 = ポイント.IndexOf(個[1]);
            this.開始ピーク = new(ピーク[索引].X, ピーク[索引].Y, ピーク[索引].Z);

            this.進行方向 = new 座標[this.長さ];
            for (int i = 0; i < this.長さ; i++)
                進行方向[i] = 方向[int.Parse(個[2 + i])];
        }
    }

    internal class Feladat2 {
        public static List<道路> すべての道路 = new();

        public static void Start() {
            Console.WriteLine("2. feladat:");

            ファイルスキャン();
            仕事_A();
            仕事_B();
            仕事_C();
        }

        public static void ファイルスキャン() {
            StreamReader ファイル = new("utak.txt");

            while (!ファイル.EndOfStream)
                すべての道路.Add(new(ファイル.ReadLine()));

            ファイル.Close();
        }

        public static bool 実現可能です(道路 item) {
            座標 現在のポイント = item.開始ピーク;
            for (int i = 0; i < item.進行方向.Length; i++) {
                現在のポイント.設定XYZ(item.進行方向[i]);

                if (現在のポイント.X < 0 || 現在のポイント.X > 1 || 現在のポイント.Y < 0 || 現在のポイント.Y > 1 || 現在のポイント.Z < 0 || 現在のポイント.Z > 1)
                    return false;
            }
            return true;
        }

        // Jó megoldások keresése
        public static void 仕事_A() {
            int ピース = 0;
            foreach (道路 item in すべての道路) {
                if (実現可能です(item)) ピース++;
            }
            
            Console.WriteLine($"a) {ピース}");
        }

        // Összes csúcson járt
        public static void 仕事_B() {
            int ピース = 0;

            foreach (道路 item in すべての道路) {
                List<座標> ピーク = new() {
                    new(0, 0, 0), // A
                    new(1, 0, 0), // B
                    new(1, 1, 0), // C
                    new(0, 1, 0), // D
                    new(0, 0, 1), // E
                    new(1, 0, 1), // F
                    new(1, 1, 1), // G
                    new(0, 1, 1)  // H
                };
                座標 c = new(item.開始ピーク.X, item.開始ピーク.Y, item.開始ピーク.Z);

                for (int i = 0; i < item.進行方向.Length; i++) {
                    int 索引 = -1;
                    for (int j = 0; j < ピーク.Count; j++) {
                        if (ピーク[j].X == c.X && ピーク[j].Y == c.Y && ピーク[j].Z == c.Z) {
                            索引 = j;
                            break;
                        }
                    }
                    if (索引 != -1)
                        ピーク.RemoveAt(索引);

                    c.設定XYZ(item.進行方向[i]);
                }

                if (実現可能です(item) && ピーク.Count == 0)
                    ピース++;
            }

            Console.WriteLine($"b) {ピース}");
        }

        // Összes élen járt
        public static void 仕事_C() {
            int ピース = 0;

            foreach (道路 item in すべての道路) {
                List<座標> ピーク1 = new() {
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
                List<座標> ピーク2 = new() {
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
                座標 c = new(item.開始ピーク.X, item.開始ピーク.Y, item.開始ピーク.Z);
                座標 p = new(0, 0, 0);

                for (int i = 0; i < item.進行方向.Length; i++) {
                    p = new(c.X, c.Y, c.Z);
                    c.設定XYZ(item.進行方向[i]);

                    int 索引 = -1;
                    for (int j = 0; j < ピーク1.Count; j++) {
                        if (((ピーク1[j].X == c.X && ピーク1[j].Y == c.Y && ピーク1[j].Z == c.Z) && (ピーク2[j].X == p.X && ピーク2[j].Y == p.Y && ピーク2[j].Z == p.Z)) || ((ピーク2[j].X == c.X && ピーク2[j].Y == c.Y && ピーク2[j].Z == c.Z) && (ピーク1[j].X == p.X && ピーク1[j].Y == p.Y && ピーク1[j].Z == p.Z))) {
                            索引 = j;
                            break;
                        }
                    }
                    if (索引 != -1) {
                        ピーク1.RemoveAt(索引);
                        ピーク2.RemoveAt(索引);
                    }
                }

                if (実現可能です(item) && ピーク1.Count == 0)
                    ピース++;
            }

            Console.WriteLine($"c) {ピース}");
        }
    }
}