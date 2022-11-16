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
        static void Main(string[] args)
        {
            List<Row> rows = new();
            rows.Add(new(File.ReadAllLines("szamok7.txt").ToList().ConvertAll(x => Convert.ToInt32(x))));
            while (rows[^1].Nums.Count >= 2)
            {
                List<int> newNums = new();
                for (int i = 1; i < rows[^1].Nums.Count; i++)
                {
                    newNums.Add(rows[^1].Nums[i] + rows[^1].Nums[i - 1]);
                }
                rows.Add(new(newNums));
            }
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                Console.WriteLine($"{i + 1}: {String.Join(" ", rows[i].Nums)}");
            }
            Row row6 = rows[5];
            int not3digitNums = 0;
            foreach (int x in row6.Nums)
            {
                if (x < 100 || x > 999) not3digitNums++;
            }
            Console.WriteLine($"a) A számpiramos 6. sorában {not3digitNums} nem háromjegyű szám található");
            Console.WriteLine($"b) A számpiramos legfelső sorában a {rows[^1].Nums[0]} szám szerepel");

            List<double> quotients = new();
            for (int i = 0; i < rows.Count; i++)
            {
                Console.WriteLine($"{String.Join(" ", rows[i].Nums)}");
            }
            int counter = 0;
            for (int i = 1; i < rows.Count; i++)
            {
                double num = Convert.ToDouble(rows[i].Nums.Sum()) / rows[i - 1].Nums.Sum();
                quotients.Add(num);
                if (num > 1.5) counter++;
            }
            Console.WriteLine("HÁNYADOSOK\n--------------");
            quotients.ForEach(x => Console.WriteLine(x));
            Console.WriteLine($"c) {counter} sor esetén lesz a hányados nagyobb 1,5-nél");
        }
    }
}
