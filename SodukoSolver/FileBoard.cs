using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;

namespace SodukoSolver
{
    public class FileSudukoBoard : SudukoBoard
    {
        private OpenFileDialog openFileDialog;
        public string _path { get; set; }

        public FileSudukoBoard()
        {
            double root;
            int iroot;
            PuzzleString =OpenFile();
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

        public string OpenFile()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\o2002\Desktop\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _path = openFileDialog.FileName;
                FileStream file = File.Open(_path, (FileMode)FileAccess.ReadWrite);
                if (file != null)
                {
                    byte[] puzzle = new byte[file.Length]; ;
                    file.Read(puzzle, 0, (int)file.Length);
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string str = temp.GetString(puzzle, 0, (int)file.Length);
                    file.Close();
                    return str;
                }
                else
                {
                    PuzzleString = "";
                    MessageBox.Show("File Not Found");
                }
            }
            else
            {
                PuzzleString = "";
                MessageBox.Show("File Not Chosen");
            }
            return "";
        }
    }
}
