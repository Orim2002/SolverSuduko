using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    class ConsoleSudukoBoard : SudukoBoard
    {

        public ConsoleSudukoBoard(string puzzle)
        {
            double root;
            int iroot;
            PuzzleString = puzzle;
            Solved = false;
            Valid = true;
            if (PuzzleString == "")
                Valid = false;
            root = Math.Sqrt(PuzzleString.Length);
            root = Math.Sqrt(root);
            iroot = (int)root;
            if (root - iroot != 0)
                Valid = false;
            else
                Size = (int)(root * root);
            if (Valid)
            {

                Board = new Square[Size][];
                for (int i = 0; i < Size; i++)
                    Board[i] = new Square[Size];
                for (int i = 0; i < Size; i++)
                    for (int j = 0; j < Size; j++)
                        Board[i][j] = new Square(i, j, PuzzleString[i * Size + j]);
                Region[] rows = new Region[Size],
                columns = new Region[Size],
                blocks = new Region[Size];
                for (int i = 0; i < Size; i++)
                {
                    Square[] cells;
                    int c;

                    cells = new Square[Size];
                    for (c = 0; c < Size; c++)
                    {
                        cells[c] = Board[c][i];
                    }
                    rows[i] = new Region(cells);

                    for (c = 0; c < Size; c++)
                    {
                        cells[c] = Board[i][c];
                    }
                    columns[i] = new Region(cells);

                    c = 0;
                    int ix = i % iroot * iroot,
                        iy = i / iroot * iroot;
                    for (int x = ix; x < ix + iroot; x++)
                    {
                        for (int y = iy; y < iy + iroot; y++)
                        {
                            cells[c++] = Board[x][y];
                        }
                    }
                    blocks[i] = new Region(cells);
                }
                Regions = new Collection<Collection<Region>>(new Collection<Region>[]
                {
                new Collection<Region>(rows),
                new Collection<Region>(columns),
                new Collection<Region>(blocks)
                });
            }
        }
    }
}
