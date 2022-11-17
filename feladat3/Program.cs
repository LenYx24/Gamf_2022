using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace feladat3
{
    internal class Program
    {
        class Row
        {
            public List<int> Nums;
            public Row(List<int> s)
            {
                Nums = s;
            }
        }
        class NumberPyramid
        {
            List<Row> rows;
            public NumberPyramid()
            {
                rows = new();
            }
            public void SetFirstRowFromFile()
            {
                string[] text = File.ReadAllLines("../../../szamok7.txt");
                List<int> nums = text.ToList().ConvertAll(x => Convert.ToInt32(x));
                Row firstRow = new(nums);
                rows.Add(firstRow);
            }
            public void calculateRows()
            {
                if (rows.Count == 0) return;
                while (rows[^1].Nums.Count >= 2)
                {
                    List<int> newNums = new();
                    for (int i = 1; i < rows[^1].Nums.Count; i++)
                    {
                        newNums.Add(rows[^1].Nums[i] + rows[^1].Nums[i - 1]);
                    }
                    rows.Add(new(newNums));
                }
            }
            public void PrintRowsInReverse()
            {
                for (int i = rows.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{i + 1}: {String.Join(" ", rows[i].Nums)}");
                }
            }
            public void PrintRows()
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {String.Join(" ", rows[i].Nums)}");
                }
            }
            public int GetSumOfRowValues(int index)
            {
                return rows[index].Nums.Sum();
            }
            public List<Row> Rows { get { return rows; } }
            public int LastRowsNumber { get { return rows[^1].Nums[0]; } } // Legfelső (utolsó) sor száma
        }
        static void Main(string[] args)
        {
            // A piramis elkészítése
            NumberPyramid numberPyramid = new NumberPyramid();
            numberPyramid.SetFirstRowFromFile();
            numberPyramid.calculateRows();
            numberPyramid.PrintRows();

            // a rész
            Row row6 = numberPyramid.Rows[5];
            int not3digitNums = 0;
            foreach (int x in row6.Nums)
            {
                if (x < 100 || x > 999) not3digitNums++;
            }
            //LINQ:
            int n = row6.Nums.Where(x => x < 100 || x > 999).Count();
            Console.WriteLine($"a) A számpiramos 6. sorában {not3digitNums} nem háromjegyű szám található");

            // b rész
            string b = numberPyramid.LastRowsNumber.ToString();
            Console.WriteLine($"b) A számpiramos legfelső sorában a {b} szám szerepel");

            // c rész
            numberPyramid.PrintRowsInReverse();
            List<double> quotients = new();
            for (int i = 1; i < numberPyramid.Rows.Count; i++)
            {
                double rowSums = Convert.ToDouble(numberPyramid.GetSumOfRowValues(i));
                double aboveRowSums = numberPyramid.GetSumOfRowValues(i-1);
                double quotient = rowSums / aboveRowSums;
                quotients.Add(quotient);
            }
            
            int c = quotients.Where(x => x > 1.5).Count();// Megszámolja hogy hány olyan hányados van amely értéke nagyobb 1,5nél
            Console.WriteLine($"c) {c} sor esetén lesz a hányados nagyobb 1,5-nél");
            // TESZT
            // Hányadosok kiírása
            //Console.WriteLine("HÁNYADOSOK\n--------------");
            //quotients.ForEach(x => Console.WriteLine(x));
        }
    }
}
