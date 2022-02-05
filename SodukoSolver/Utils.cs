using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    abstract class Utils
    {
        public enum Command
        {
            File,
            Console,
            Exit
        }

        public static Command RetCommand()
        {
            Console.WriteLine("Please choose a command:");
            Console.WriteLine("1.File- To open file source");
            Console.WriteLine("2.Console- To write soduko SudukoBoard in the console");
            Console.WriteLine("3.Exit- To Exit the program");
            string command = Console.ReadLine();
            switch (command)
            {
                case "File":
                    return Command.File;
                case "1":
                    return Command.File;
                case "Console":
                    return Command.Console;
                case "2":
                    return Command.Console;
                case "Exit":
                    return Command.Exit;
                case "3":
                    return Command.Exit;
                default:
                    Console.WriteLine("Illegal Command");
                    return RetCommand();
            }
        }

        public static string RL()
        {
            System.IO.Stream inputStream = Console.OpenStandardInput(1024);
            Console.SetIn(new System.IO.StreamReader(inputStream));
            return Console.ReadLine();
        }

        public static int FindDot(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.')
                    return i;
            }
            return -1;
        }

        public static void Print(SudukoBoard SudukoBoard)
        {
            for (int i = 0; i < SudukoBoard.Size; i++)
            {
                for (int j = 0; j < SudukoBoard.Size; j++)
                {
                    int num = (int)(SudukoBoard.Board[i][j].Number - 48);
                    if (num > 9)
                        Console.Write(" {0}", num);
                    else
                        Console.Write(" {0} ", num);

                }
                Console.WriteLine();
            }

        }

        public static void PrintToFile(TextWriter tw, SudukoBoard SudukoBoard)
        {
            for (int i = 0; i < SudukoBoard.Size; i++)
            {
                for (int j = 0; j < SudukoBoard.Size; j++)
                {
                    int num = (int)(SudukoBoard.Board[i][j].Number - 48);
                    byte[] bytes = BitConverter.GetBytes(num);
                    if (BitConverter.IsLittleEndian)
                        Array.Reverse(bytes);
                    if (num > 9)
                        tw.Write("{0} ", num);
                    else
                        tw.Write(" {0} ", num);
                }
                tw.WriteLine();
            }
        }

        public static bool CheckDup(Square[] squares)
        {
            bool same = false;
            for(int i=0;i<squares.Length;i++)
            {
                for (int j = i+1; j < squares.Length; j++)
                {
                    if (squares[i].Number == squares[j].Number && squares[i].Number != '0')
                        same = true;
                }
            }
            return same;
        }

        public static string SudukoBoardToPuzzle(SudukoBoard Board)
        {
            string puzzle = "";
            int n = Board.Size;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    puzzle += Board.Board[i][j].Number;
                }
            }
            return puzzle;
        }
    }
}
