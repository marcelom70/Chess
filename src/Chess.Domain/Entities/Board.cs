using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain.Entities
{
    public class Board
    {
        private readonly IList<Square> _squares = new List<Square>();
        private readonly IList<Piece> _pieces = new List<Piece>();

        public Board()
        {
            CreateBoard();
            CreatePieces();
            SetupBoard();

        }

        private void CreateBoard()
        {
            var rows = new[] { 1, 2, 3, 4, 5, 6, 8 };
            var columns = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };

            foreach (var column in columns)
            {
                foreach (var row in rows)
                {
                    _squares.Add(new Square(row, column));
                }
            }
        }

        private void CreatePieces()
        {
            var colours = new[] { "white", "black" };

            foreach (var colour in colours)
            {
                _pieces.Add(new King(colour));
                _pieces.Add(new Queen(colour));
                _pieces.Add(new Rook(colour));
                _pieces.Add(new Rook(colour));
                _pieces.Add(new Bishop(colour));
                _pieces.Add(new Bishop(colour));
                _pieces.Add(new Knight(colour));
                _pieces.Add(new Knight(colour));

                for (var i = 0; i < 8; i++)
                {
                    _pieces.Add(new Pawn(colour));
                }
            }

        }

        private void SetupBoard()
        {
            var colour = "white";
            GetFreePiece(typeof(Rook), colour).AssignSquare(GetSquare('A', 1));
            GetFreePiece(typeof(Knight), colour).AssignSquare(GetSquare('B', 1));
            GetFreePiece(typeof(Bishop), colour).AssignSquare(GetSquare('C', 1));
            GetFreePiece(typeof(Queen), colour).AssignSquare(GetSquare('D', 1));
            GetFreePiece(typeof(King), colour).AssignSquare(GetSquare('E', 1));
            GetFreePiece(typeof(Bishop), colour).AssignSquare(GetSquare('F', 1));
            GetFreePiece(typeof(Knight), colour).AssignSquare(GetSquare('G', 1));
            GetFreePiece(typeof(Rook), colour).AssignSquare(GetSquare('H', 1));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('A', 2));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('B', 2));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('C', 2));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('D', 2));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('E', 2));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('F', 2));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('G', 2));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('H', 2));

            colour = "black";
            GetFreePiece(typeof(Rook), colour).AssignSquare(GetSquare('A', 8));
            GetFreePiece(typeof(Knight), colour).AssignSquare(GetSquare('B', 8));
            GetFreePiece(typeof(Bishop), colour).AssignSquare(GetSquare('C', 8));
            GetFreePiece(typeof(Queen), colour).AssignSquare(GetSquare('D', 8));
            GetFreePiece(typeof(King), colour).AssignSquare(GetSquare('E', 8));
            GetFreePiece(typeof(Bishop), colour).AssignSquare(GetSquare('F', 8));
            GetFreePiece(typeof(Knight), colour).AssignSquare(GetSquare('G', 8));
            GetFreePiece(typeof(Rook), colour).AssignSquare(GetSquare('H', 8));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('A', 7));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('B', 7));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('C', 7));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('D', 7));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('E', 7));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('F', 7));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('G', 7));
            GetFreePiece(typeof(Pawn), colour).AssignSquare(GetSquare('H', 7));
        }

        private Piece GetFreePiece(Type type, string colour)
        {
            return _pieces.FirstOrDefault(p => p.Colour == colour && p.GetType() == type && p.Square == null);
        }

        public Square GetSquare(char column, int row)
        {
            return _squares.FirstOrDefault(s => s.Column == column && s.Row == row);
        }

        public Piece GetPiece(Square square)
        {
            return _pieces.FirstOrDefault(p => p.Square == square);
        }
    }
}