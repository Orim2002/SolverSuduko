using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    public class Square: ICloneable,IComparable
    {
        public char Number { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public List<char> Possible { get; set; }

        public Square(int col, int row, char number)
        {
            Number = number;
            Col = col;
            Row = row;
            Possible = new List<char>();
        }

        public bool Equals(Square s)
        {
            return Row == s.Row && Col == s.Col;
        }

        public object Clone()
        {
            Square other = (Square)this.MemberwiseClone();
            other.Number = this.Number;
            other.Row = this.Row;
            other.Col = this.Col;
            other.Possible = new List<char>(Possible);
            return other;
        }

        public int CompareTo(object obj)
        {
            Square other = (Square)obj;
            return Possible.Count - other.Possible.Count;
        }
    }
}

