using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Domain.Entities
{
    public class Board
    {
        private readonly IList<Piece> _pieces = new List<Piece>();
        //HACK: mantive para provavel futura validacao de boundaries do movimento
        //private readonly int[] _rows = new[] { 1, 2, 3, 4, 5, 6, 8 };
        //private readonly char[] _columns = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };


        public Board()
        {
            var colour = "white";
            _pieces.Add(new Rook(colour, "A1"));
            _pieces.Add(new Knight(colour, "B1"));
            _pieces.Add(new Bishop(colour, "C1"));
            _pieces.Add(new Queen(colour, "D1"));
            _pieces.Add(new King(colour, "E1"));
            _pieces.Add(new Bishop(colour, "F1"));
            _pieces.Add(new Knight(colour, "G1"));
            _pieces.Add(new Rook(colour, "H1"));
            _pieces.Add(new Pawn(colour, "A2"));
            _pieces.Add(new Pawn(colour, "B2"));
            _pieces.Add(new Pawn(colour, "C2"));
            _pieces.Add(new Pawn(colour, "D2"));
            _pieces.Add(new Pawn(colour, "E2"));
            _pieces.Add(new Pawn(colour, "F2"));
            _pieces.Add(new Pawn(colour, "G2"));
            _pieces.Add(new Pawn(colour, "H2"));

            colour = "black";
            _pieces.Add(new Rook(colour, "A8"));
            _pieces.Add(new Knight(colour, "B8"));
            _pieces.Add(new Bishop(colour, "C8"));
            _pieces.Add(new Queen(colour, "D8"));
            _pieces.Add(new King(colour, "E8"));
            _pieces.Add(new Bishop(colour, "F8"));
            _pieces.Add(new Knight(colour, "G8"));
            _pieces.Add(new Rook(colour, "H8"));
            _pieces.Add(new Pawn(colour, "A7"));
            _pieces.Add(new Pawn(colour, "B7"));
            _pieces.Add(new Pawn(colour, "C7"));
            _pieces.Add(new Pawn(colour, "D7"));
            _pieces.Add(new Pawn(colour, "E7"));
            _pieces.Add(new Pawn(colour, "F7"));
            _pieces.Add(new Pawn(colour, "G7"));
            _pieces.Add(new Pawn(colour, "H7"));

        }

        public Piece GetPiece(string position)
        {
            return _pieces.FirstOrDefault(p => p.Position == position);
        }
    }
}