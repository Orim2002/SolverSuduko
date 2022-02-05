using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace SodukoSolver
{
    class Program
    {
        public static string GetSolution(SudukoBoard SudukoBoard)
        {
            Stopwatch stopWatch = new Stopwatch();
            Solver solver = new Solver(SudukoBoard);
            stopWatch.Start();
            SudukoBoard.Solved = solver.Solve();
            stopWatch.Stop();
            TimeSpan time = stopWatch.Elapsed;
            return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
        }

        public static string RetPath(SudukoBoard SudukoBoard)
        {
            string retpath = ((FileSudukoBoard)SudukoBoard)._path;
            int index = Utils.FindDot(retpath);
            if (index != -1)
                retpath = retpath.Insert(index, "Ans");
            return retpath;
        }

        [STAThread]
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Utils.Command command = Utils.RetCommand();
            SudukoBoard SudukoBoard;
            string elapsedTime;
            while (command != Utils.Command.Exit)
            {
                switch (command)
                {
                    case Utils.Command.Console:
                        Console.WriteLine("Please enter your Soduko puzzle:");
                        SudukoBoard = new ConsoleSudukoBoard(Utils.RL());
                        SudukoBoard.Valid = SudukoBoard.IsValid();
                        if (!SudukoBoard.Valid)
                        {
                            Console.WriteLine("Board is not valid");
                            break;
                        }
                        elapsedTime = GetSolution(SudukoBoard);
                        if(SudukoBoard.Solved)
                        {
                            Utils.Print(SudukoBoard);
                            Console.WriteLine("Runtime: " + elapsedTime);
                        }
                        else
                            Console.WriteLine("No Solution");
                        break;
                    case Utils.Command.File:
                        SudukoBoard = new FileSudukoBoard();
                        SudukoBoard.Valid = SudukoBoard.IsValid();
                        if (!SudukoBoard.Valid)
                        {
                            Console.WriteLine("Board is not valid");
                            break;
                        }
                        string retpath = RetPath(SudukoBoard);
                        elapsedTime = GetSolution(SudukoBoard);
                        TextWriter tw = new StreamWriter(retpath);
                        if (SudukoBoard.Solved)
                        {
                            Utils.PrintToFile(tw, SudukoBoard);
                            tw.WriteLine("Runtime: " + elapsedTime);
                            Console.WriteLine("Solved Suduko Board Has Been Written To File!");
                        }
                        else
                            Console.WriteLine("No Solution");
                        tw.Close();
                        break;
                    default:
                        break;
                }
                command = Utils.RetCommand();
            }
            Console.WriteLine("Thank you for using this Soduko puzzle solver:)");
        }
    }
}
