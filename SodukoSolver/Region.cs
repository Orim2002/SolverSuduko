using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodukoSolver
{
    class Region : IEnumerable<Square>,ICloneable
    {
        private readonly Square[] _cells;
        public Square this[int index] => _cells[index];
        public Region(Square[] cells)
        {
            _cells = (Square[])cells.Clone();
        }

        public IEnumerable<Square> GetCellsWithCandidate(char candidate)
        {
            return _cells.Where(c => c.Possible.Contains(candidate));
        }

        public IEnumerator<Square> GetEnumerator()
        {
            return ((IEnumerable<Square>)_cells).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        public object Clone()
        {
            Region other = (Region)this.MemberwiseClone();
            for(int i = 0;i<_cells.Length;i++)
            {
                other._cells[i] = (Square)_cells[i].Clone();
            }
            return other;
        }
    }
}
