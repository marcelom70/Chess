using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Chess;

namespace WpfChess
{
    class PieceImage : Image
    {
        private Piece _piece;

        public Piece piece
        {
            get { return _piece; }
            set { _piece = value; }
        }
    }
}
