using System.Collections.Generic;

namespace Chess.Domain
{
    public class Board
    {
        public IList<Square> Squares { get; set; }
        public IList<Piece> Pieces { get; set; }
    }
}