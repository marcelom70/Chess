using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Chess;

namespace WcfChess
{
    public class WcfChess : iChess
    {
        Board board; //initialize a board (no new yet)
        
        public bool InitializeBoard()
        {
            try
            {
                board = new Chess.Board(); //Initializing totally the board
                return true;
            }
            catch (Exception ex)
            {
                return false; //fail to initialize the board
            }
        }

        public bool MovePiece(Chess.Piece pieceToMove, Chess.Square squareToMove)
        {
            return board.Movement(pieceToMove, squareToMove);
        }

    }
    
}
