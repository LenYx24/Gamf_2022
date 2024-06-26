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
            public List<Step> Steps = new();
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
                string[,] arr = new string[6, 6];
                int[] firstStep = Steps[0].RowColumnArrAsIndices;
                arr[firstStep[0], firstStep[1]] = "F";
                int[] startingPoint = Steps[^1].RowColumnArrAsIndices;
                arr[startingPoint[0], startingPoint[1]] = "S";
                bool isThere = false;
                for (int i = 0; i < HorseSteps.GetLength(0); i++)
                {
                    int[] lastStep = Steps[^1].AddStep(i).RowColumnArrAsIndices;
                    if (lastStep[0] == -1 || lastStep[1] == -1) continue;
                    arr[lastStep[0], lastStep[1]] = "P";
                    if (String.Join("", firstStep) == String.Join("", lastStep))
                    {
                        arr[lastStep[0], lastStep[1]] = "X";
                        isThere = true;
                    }
                }
                //if (isThere) PossibleMoves(arr);
                return isThere;
            }
            public void PossibleMoves(string[,] arr)
            {
                Console.WriteLine("  1|2|3|4|5|6");
                for(int i = 0; i < 6; i++)
                {
                    Console.WriteLine(new String('-',14));
                    string qwe = "";
                    for(int j = 0; j < 6; j++)
                    {
                        if (arr[i,j] == null) qwe += $" |";
                        else qwe += $"{arr[i,j]}|";
                    }
                    Console.WriteLine($"{i + 1}|{qwe}");
                }
                Console.WriteLine(new String('-', 14));
            }
            public bool AreThereStepsThatAreTheSame()
            {
                List<int> stepints = Steps.ConvertAll(x => x.RowColumn).ToList();
                if (stepints.Count != stepints.Distinct().Count())
                {
                    return true;
                }
                else
                {
                    var st = stepints.OrderBy(x => x);
                    string k = "";
                    st.ToList().ForEach(x => k += $"{x} ");
                    //Console.WriteLine(k);
                }
                return false;
            }
            public bool EveryMoveInGoodOrder()
            {
                for (int k = 0; k < Steps.Count-1; k++)
                {
                    bool isLegalMove = false;
                    for (int i = 0; i < HorseSteps.GetLength(0); i++)
                    {
                        int[] currentStep = Steps[k].AddStep(i).RowColumnArrAsIndices;
                        if (currentStep[0] == -1 || currentStep[1] == -1) continue;
                        if (String.Join("", Steps[k+1].RowColumnArrAsIndices) == String.Join("", currentStep))
                        {
                            isLegalMove = true;
                            break;
                        }
                    }
                    if(!isLegalMove)
                        return false;
                }
                return true;
            }
        }
        class Step
        {
            int row;
            int column;
            public Step(string xy)
            {
                row = Convert.ToInt32(xy.Substring(0, 1));
                column = Convert.ToInt32(xy.Substring(1, 1));
            }
            public int Row { get { return row; } }
            public int Column { get { return column; } }
            public int RowColumn { get { return Convert.ToInt32(row.ToString() + column.ToString()); } }
            public int[] RowColumnArr { get { return new int[] { row, column }; } }
            public int[] RowColumnArrAsIndices { get { return new int[] { row-1, column-1 }; } }
            public Step AddStep(int i)
            {
                int r1 = row + HorseSteps[i, 0];
                int c1 = column + HorseSteps[i, 1];
                if (r1 < 1 || r1 > 6 || c1 < 1 || c1 > 6)
                    return new Step("00");
                return new Step($"{r1}{c1}");
            }
        }
        public static void Main2()
        {
            List<Table> tables = new();
            StreamReader r = new("lepesek.txt"); // ../../../lepesek.txt
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
            for (int i = 0; i < tables.Count; i++)
            {
                if(tables[i].StartingFieldPossibleAfterLastStep()) {
                  //Console.WriteLine(i);
                  b++;
                }
            }
            Console.WriteLine($"b) {b} olyan sor van a fájlban, ahol az utolsó lépés után a kezdő mező egy újabb lólépéssel elérhető");

            //c
            string codeRow = "";
            for(int i = 0; i < tables.Count; i++)
            {
                string res = "1";
                if (tables[i].AreThereStepsThatAreTheSame() || !tables[i].EveryMoveInGoodOrder())
                {
                    res = "0";
                }

                codeRow += res;
            }
            Console.WriteLine($"c) Kódsor: {codeRow}");
        }
    }
}
