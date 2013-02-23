using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain.Entities
{
    public class Board
    {
        private readonly IList<Piece> _pieces = new List<Piece>();
        private readonly int[] _rowPositions = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private readonly char[] _columnPositions = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

        public Board(string boardConfiguration)
        {
            if (string.IsNullOrEmpty(boardConfiguration))
                boardConfiguration = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

            var rowCount = 7;
            var columnCount = 0;

            foreach (var row in boardConfiguration.Split('/'))
            {
                foreach (var pieceChar in row.ToCharArray())
                {
                    if(char.IsNumber(pieceChar))
                    {
                        columnCount += int.Parse(pieceChar.ToString());
                        continue;
                    }

                    var colour = char.IsLower(pieceChar) ? "black" : "white";

                    var ajustedPieceChar = char.ToUpperInvariant(pieceChar);

                    var position = _columnPositions[columnCount].ToString() + _rowPositions[rowCount].ToString();

                    Piece piece = null;
                    switch (ajustedPieceChar )
                    {
                        case 'R':
                            piece = new Rook(colour, position);
                            break;
                        case 'N':
                            piece = new Knight(colour, position);
                            break;
                        case 'B':
                            piece = new Bishop(colour, position);
                            break;
                        case 'Q':
                            piece = new Queen(colour, position);
                            break;
                        case 'K':
                            piece = new King(colour, position);
                            break;
                        case 'P':
                            piece = new Pawn(colour, position);
                            break;
                    }
                    _pieces.Add(piece);
                    columnCount ++;
                }
                columnCount = 0;
                rowCount--;
            }
        }

        public bool AcceptPosition(string position)
        {
            var charArray = position.ToUpper().ToCharArray();
            var column = charArray[0];
            var row = charArray[1];

            if(!_rowPositions.Contains(row))
                throw new Exception("Invalid position");

            if (!_rowPositions.Contains(column))
                throw new Exception("Invalid position");

            return true;
        }

        public Piece GetPiece(string position)
        {
            return _pieces.FirstOrDefault(p => p.Position == position);
        }
    }
}