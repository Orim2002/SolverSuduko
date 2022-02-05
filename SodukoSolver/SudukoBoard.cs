using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    class SudukoBoard: ICloneable
    {
        public string PuzzleString { get; set; }
        public int Size { get; set; }
        public bool Valid { get; set; }
        public bool Solved { get; set; }
        public Square[][] Board { get; set; }
        public Collection<Collection<Region>> Regions;

        public bool IsValid()
        {
            bool valid = Valid;
            for (int i = 0; i < Size; i++)
            {
                foreach (Collection<Region> region in Regions)
                {
                    Square[] s = region[i].ToArray();
                    char ch = (char)(s.Length + 48);
                    for(int j =0;j<s.Length;j++)
                    {
                        if (s[j].Number < '0' || s[j].Number > ch)
                            valid = false;
                    }
                    if (Utils.CheckDup(s))
                        valid = false;
                }
            }
            return valid;
        }

        public Square GetLeastOptions()
        {
            int minOp = 16;
            Square min=null;
            for(int i = 0;i<Size;i++)
            {
                for(int j = 0;j<Size;j++)
                {
                    if (minOp > Board[i][j].Possible.Count&&Board[i][j].Number=='0')
                    {
                        min = Board[i][j];
                        minOp = min.Possible.Count;
                    }
                        
                }
            }
            return min;
        }

        public object Clone()
        {
            SudukoBoard other = (SudukoBoard)this.MemberwiseClone();
            other.PuzzleString = string.Copy(PuzzleString);
            other.Size = Size;
            other.Valid = Valid;
            other.Solved = Solved;
            for(int i=0;i<Size;i++)
            {
                for(int j=0;j<Size;j++)
                {
                    other.Board[i][j] = (Square)Board[i][j].Clone();
                }
            }
            other.Regions = new Collection<Collection<Region>>(Regions);
            return other;
        }

        public List<Square> GetEmpty()
        {
            List<Square> empty = new List<Square>();
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    if (Board[i][j].Number == '0')
                        empty.Add(Board[i][j]);
            empty.Sort();
            return empty;
        }
    }
}
