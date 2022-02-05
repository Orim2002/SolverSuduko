using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SodukoSolver
{
    class Solver
    {
        private SudukoBoard Board;
        public Solver(SudukoBoard SudukoBoard)
        {
            Board = SudukoBoard;
        }

        public void Check(SudukoBoard grid)
        {
            for (int n = 0; n < grid.Size; n++)
            {
                char number = (char)(n + 49);
                for (int i = 0; i < grid.Size; i++)
                {
                    foreach (Collection<Region> region in grid.Regions)
                    {
                        Square[] s = region[i].ToArray();
                        for (int j = 0; j < grid.Size; j++)
                        {
                            if (s[j].Number == number)
                            {
                                for (int k = 0; k < grid.Size; k++)
                                {
                                    s[k].Possible.Remove(number);
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool IsSolved(SudukoBoard grid)
        {
            bool solved = true;
            for (int x = 0; x < grid.Size; x++)
            {
                for (int y = 0; y < grid.Size; y++)
                {
                    Square cell = grid.Board[x][y];
                    if (cell.Number == '0')
                        solved = false;
                }
            }
            return solved;
        }
        public bool isSafe(SudukoBoard grid, int row, int col, char n)
        {
            // Check if we find the same num
            // in the similar row , we
            // return false
            for (int x = 0; x <= grid.Size-1; x++)
                if (grid.Board[x][col].Number == n)
                    return false;

            // Check if we find the same num
            // in the similar column ,
            // we return false
            for (int x = 0; x <= grid.Size - 1; x++)
                if (grid.Board[row][x].Number == n)
                    return false;

            // Check if we find the same num
            // in the particular 3*3
            // matrix, we return false
            int root = (int)Math.Sqrt(grid.Size);
            int startRow = row - row % root, startCol
              = col - col % root;
            for (int i = 0; i < root; i++)
                for (int j = 0; j < root; j++)
                    if (grid.Board[i + startRow][j + startCol].Number == n)
                        return false;

            return true;
        }

        public SudukoBoard SolveSudoku(SudukoBoard grid)
        {
            Reduce(grid);
            if (IsSolved(grid))
                return grid;
            List<Square> empty=grid.GetEmpty();
            Square square = empty.ElementAt(0);
            foreach (char option in square.Possible)
            {
                SudukoBoard cpy = (SudukoBoard)grid.Clone();
                Square copyCell = cpy.Board[square.Row][square.Col];
                copyCell.Number = option;
                copyCell.Possible = new List<char>(option);
                Check(cpy);
                SudukoBoard result = SolveSudoku(cpy);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        public void NakedSingle(SudukoBoard grid)
        {
            for (int x = 0; x < grid.Size; x++)
            {
                for (int y = 0; y < grid.Size; y++)
                {
                    Square cell = grid.Board[x][y];
                    if (cell.Number == '0')
                    {
                        if(cell.Possible.Count==1)
                        {
                            cell.Number = cell.Possible.ElementAt(0);
                            char[] puzzle = grid.PuzzleString.ToArray();
                            puzzle[grid.Size * x + y] = cell.Number;
                            grid.PuzzleString = new string(puzzle);
                        }
                    }
                }
            }
        }

        private void HiddenSingle(SudukoBoard grid)
        {
            for (int i = 0; i < grid.Size; i++)
            {
                foreach (Collection<Region> region in grid.Regions)
                {
                    char c = (char)(grid.Size + 48);
                    for (char candidate = '1'; candidate <= c; candidate++)
                    {
                        Square[] s = region[i].GetCellsWithCandidate(candidate).ToArray();
                        if (s.Length == 1)
                        {
                            s[0].Number = candidate;
                        }
                    }
                }
            }
        }

        public void NakedPair(SudukoBoard grid)
        {
            for (int i = 0; i < grid.Size; i++)
            {
                foreach (Collection<Region> region in grid.Regions)
                {
                    for (int j = 0; j < grid.Size; j++)
                    {
                        Square c = region[i][j];
                        if (c.Possible.Count == 2)
                        {
                            for (int k = 0; k < grid.Size; k++)
                            {
                                if(k!=j&& region[i][k].Possible.Equals(c.Possible))
                                {
                                    for (int r = 0; r < grid.Size; r++)
                                    {
                                        if (r != k)
                                        {
                                            region[i][r].Possible.Remove(c.Possible.ElementAt(0));
                                            region[i][r].Possible.Remove(c.Possible.ElementAt(1));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void HiddenPair(SudukoBoard grid)
        {
            for (int i = 0; i < grid.Size; i++)
            {
                foreach (Collection<Region> region in grid.Regions)
                {
                    char ch = (char)(grid.Size + 48);
                    for (char candidate = '1'; candidate <= ch; candidate++)
                    {
                        for (char candidate2 = candidate; candidate2 <= ch; candidate2++)
                        {
                            Square[] first = region[i].GetCellsWithCandidate(candidate).ToArray();
                            Square[] second = region[i].GetCellsWithCandidate(candidate2).ToArray();
                            var q = from a in first
                                    join b in second on a equals b
                                    select a;

                            bool equals = first.Length == second.Length && q.Count() == first.Length;
                            if (equals&&first.Length == 2 && candidate != candidate2)
                            {
                                List<char> list = new List<char>();
                                list.Add(candidate);
                                list.Add(candidate2);
                                List<char> list2 = new List<char>(list);
                                first[0].Possible = list;
                                first[1].Possible = list2;
                            }
                        }
                    }
                }
            }
        }


        public void Reduce(SudukoBoard grid)
        {
            NakedSingle(grid);
            HiddenSingle(grid);
            //NakedPair(grid);
            //HiddenPair(grid);
        }

        public bool Solve()
        {
            if (!Board.Valid)
            {
                Console.WriteLine("Soduko SudukoBoard is not valid!");
                return false;
            }
            for (int i = 0; i < Board.Size; i++)
            {
                for (int j = 0; j < Board.Size; j++)
                {
                    for (int k = 0; k < Board.Size; k++)
                    {
                        if (Board.Board[i][j].Number != '0')
                            break;
                        char number = (char)(k + 49);
                        Board.Board[i][j].Possible.Add(number);
                    }
                }
            }
            Check(Board);
            Board.PuzzleString = Utils.SudukoBoardToPuzzle(Board);
            Board.Solved = IsSolved(Board);
            Reduce(Board);
            Board = SolveSudoku(Board);
            if (Board.Solved==false&& Board!=null)
            {
                Board.PuzzleString = Utils.SudukoBoardToPuzzle(Board);
                return true;
            }
            Board.Solved = IsSolved(Board);
            if (Board.Solved)
                return true;
            return false;
        }
    }
}
