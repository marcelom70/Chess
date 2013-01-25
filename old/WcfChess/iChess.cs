using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Chess;

namespace WcfChess
{
    [ServiceContract]
    public interface iChess
    {

        [OperationContract]
        bool InitializeBoard();

        bool MovePiece(Chess.Piece pieceToMove, Chess.Square squareToMove);

    }

}
