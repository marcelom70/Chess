using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Domain
{
    public class Match
    {
        public Board Board {get;set;}
        public Player Player{ get; set; } 
        public IList<Move> Moves { get; set; }
    }
    public class Board
    {
        public IList<Square> Squares { get; set; }
        public IList<Piece> Pieces { get; set; }
    }
    public class Player
    {
    }
    public class Move
    {
    }
    public class Square
    {
    }
    public abstract class Piece
    {
    }
    public class King : Piece
    {
    }
    public class Queen : Piece
    {
    }
    public class Rook:Piece
    {
    }
    public class Bishop: Piece
    {
    }
    public class Knight : Piece
    {
    }
    public class Pawn:Piece
    {
    }
}
