﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Fordulo2
{
    internal class Feladat2
    {
        static int[,] HorseSteps = {
            { 2, 1 },
            { 2, -1 },
            { 1, 2 },
            { 1, -2 },
            { -2, 1 },
            { -2, -1 },
            { -1, 2 },
            { -1, -2 }
        };
        class Table
        {
            List<Step> Steps = new();
            public Table(string[] steps)
            {
                foreach(string step in steps)
                {
                    Steps.Add(new(step));
                }
            }
            public bool FirstTenStepsInTheMiddleFour()
            {
                for (int i = 0; i < 10; i++)
                {
                    int row = Steps[i].Row;
                    int column = Steps[i].Column;
                    if ((Steps[i].Row == 3 || Steps[i].Row == 4) && (column == 3 || column == 4))
                    {
                        //Console.WriteLine($"Row: {Row(i)} --- Column: {Column(i)}");
                        return true;
                    }
                }
                return false;
            }
            public bool FirstTenStepsInTheMiddleFour2() // második módszer
            {
                for (int i = 0; i < 10; i++)
                {
                    if (Steps[i].RowColumn == 34 || Steps[i].RowColumn == 44 || Steps[i].RowColumn == 43 || Steps[i].RowColumn == 33)
                    {
                        //Console.WriteLine($"Row: {Row(i)} --- Column: {Column(i)}");
                        return true;
                    }
                }
                return false;
            }
            public bool StartingFieldPossibleAfterLastStep()
            {
                int[] firstStep = Steps[0].RowColumnArr;
                for (int i = 0; i < HorseSteps.GetLength(0); i++)
                {
                    int[] lastStep = Steps[i].RowColumnArr;
                    lastStep[0] += HorseSteps[i, 0];
                    lastStep[1] += HorseSteps[i, 1];
                    if (String.Join("", firstStep) == String.Join("", lastStep)) return true;
                }
                return false;
            }
        }
        class Step
        {
            int row;
            int column;
            int rowcolumn;
            public Step(string xy)
            {
                row = Convert.ToInt32(xy.Substring(0, 1));
                column = Convert.ToInt32(xy.Substring(1, 1));
            }
            public int Row { get { return row; } }
            public int Column { get { return column; } }
            public int RowColumn { get { return Convert.ToInt32(row.ToString() + column.ToString()); } }
            public int[] RowColumnArr { get { return new int[] { row, column }; } }
        }
        public static void Main2()
        {
            List<Table> tables = new();
            StreamReader r = new("../../../lepesek.txt");
            while (!r.EndOfStream)
            {
                tables.Add(new(r.ReadLine().Split(" ")));
            }
            r.Close();

            //a
            int a = 0;
            foreach(Table t in tables)
            {
                if (t.FirstTenStepsInTheMiddleFour()) a++;
            }
            Console.WriteLine($"a) {a} kódsor szerepel a fájlban, ahol az első 10 lépés közül valamelyik a középső négy mezőben van!");

            //b
            int b = 0;
            foreach(Table t in tables)
            {
                if(t.StartingFieldPossibleAfterLastStep()) b++;
            }
            Console.WriteLine($"b) {b} olyan sor van a fájlban, ahol az utolsó lépés után a kezdő mező egy újabb lólépéssel elérhető");
        }
    }
}