using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain.Entities
{
    class Position
    {
        private const string StrColumns = "ABCDEFGH";
        public int Row { get; private set; }
        public int Column{ get; private set; }

        public void SetPosition(string position)
        {
            var charArray = position.ToCharArray();
            Column = StrColumns.IndexOf(Char.ToUpper(charArray[0])) + 1; 
            Row = int.Parse(charArray[1].ToString());
        }

        public Position(string position)
        {
            SetPosition(position);
        }

        public override string ToString()
        {
            return StrColumns[Column].ToString() + Row.ToString();
        }
    }
}
