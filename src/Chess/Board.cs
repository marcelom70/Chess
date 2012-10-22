using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{

    /// <summary>
    /// This class is a composition of an eight-by-eight squares table.
    /// This class is responsible to create the instances of the squares and two sets of
    /// pieces. Besides, it is also responsible to place all those pieces created on its
    /// starting point position. 
    /// When the game starts, it also manages the following items:
    /// The movement of the pieces, by calling the Movement method of the specific piece instance 
    /// and then watching for interaction among the pieces.
    /// </summary>
    public class Board
    {
        public const string STR_Y = "ABCDEFGH";

        /// <summary>
        /// Initializes an empty list of squares
        /// </summary>
        //public List<Square> squareList = new List<Square>();
        //TODO: Padrao de nomenclatura c# -> public fields CamelCase
        public List<Square> SquareList = new List<Square>();

        /// <summary>
        /// Initilizes two sets of pieces
        /// </summary>
        public List<Piece> WhiteSet = new List<Piece>();
        public List<Piece> BlackSet = new List<Piece>();

        /// <summary>
        /// 
        /// </summary>
        //private Piece.EColor turn = Piece.EColor.White;
        //TODO: padrao de nomenclatura c# -> private fields _xxxxxx
        private Piece.EColor _turn = Piece.EColor.White;

        private bool[] _setMoved = new bool[2];

        private bool _check = false;
        private bool _checkmate = false;

        private Square _enPassantSquare = null; 

        #region Constructors
        /// <summary>
        /// Default constructor that creates an eight-by-eight square table and places two sets
        /// of pieces.
        /// </summary>
        public Board()
        {
            //TODO: setup board
            for (int x = 1; x != 9; x++)
            {
                for (int y = 1; y != 9; y++)
                {
                    Piece piece = null;
                    Square newSquare = new Square(x, y);
                    switch (x)
                    {
                        case 1:
                            switch (y)
                            {
                                case 1:
                                case 8:
                                    piece = new Rook(Piece.EColor.White, y);
                                    break;
                                case 3:
                                case 6:
                                    piece = new Bishop(Piece.EColor.White, y);
                                    break;
                                case 2:
                                case 7:
                                    piece = new Knight(Piece.EColor.White, y);
                                    break;
                                case 4:
                                    piece = new Queen(Piece.EColor.White);
                                    break;
                                case 5:
                                    piece = new King(Piece.EColor.White);
                                    break;
                            }
                            WhiteSet.Add(piece);
                            break;
                        case 2:
                            piece = new Pawn(Piece.EColor.White, y);
                            WhiteSet.Add(piece);
                            break;
                        case 7:
                            piece = new Pawn(Piece.EColor.Black, y);
                            BlackSet.Add(piece);
                            break;
                        case 8:
                            switch (y)
                            {
                                case 1:
                                case 8:
                                    piece = new Rook(Piece.EColor.Black, y);
                                    break;
                                case 3:
                                case 6:
                                    piece = new Bishop(Piece.EColor.Black, y);
                                    break;
                                case 2:
                                case 7:
                                    piece = new Knight(Piece.EColor.Black, y);
                                    break;
                                case 4:
                                    piece = new Queen(Piece.EColor.Black);
                                    break;
                                case 5:
                                    piece = new King(Piece.EColor.Black);
                                    break;
                            }
                            BlackSet.Add(piece);
                            break;
                        default:
                            piece = null;
                            break;
                    }

                    // Adds a piece to the square and the square to the board
                    newSquare.Piece = piece;
                    if (piece != null)
                    {
                        piece.square = newSquare;
                    }
                    SquareList.Add(newSquare);
                }
            }

        }
        #endregion

        #region Public methods
        /// <summary>
        /// Performs the castling movement, if possible
        /// </summary>
        /// <param name="king"></param>
        /// <param name="rook"></param>
        /// <returns>True if the castling was performed</returns>
        public bool Castling(King king, Rook rook)
        {

            // Both pieces must be in the game and not moved
            if ((_turn == king.Color & _turn == rook.Color) &
                (!king.Moved & !rook.Moved))
            {

                // Horizontal movement
                int progress = 1;
                int increment = 0;
                if (king.square.Y > rook.square.Y)
                {
                    progress = -1;
                    increment = 1;
                }

                for (int i = king.square.Y + (1 * progress); i != rook.square.Y; i = i + (1 * progress))
                {
                    if ((SquareList.Find(o => o.ToString() ==
                        (STR_Y.Substring(i - 1, 1) + king.square.X.ToString()))).Piece != null)
                    {
                        return false;
                    }
                }

                // Places the pieces on the squares
                Square kingSquare = SquareList.Find(o => o.ToString() ==
                        (STR_Y.Substring((king.square.Y + ((2 + increment) * progress)) - 1, 1)
                        + king.square.X.ToString()));

                // checks if the opponent pieces can reach the destination square
                List<Piece> list;
                if (king.Color == Piece.EColor.Black)
                {
                    list = WhiteSet;
                }
                else
                {
                    list = BlackSet;
                }

                foreach (Piece p in list)
                {
                    if (p.square != null)
                    {
                        _check = VerifyCheck(p, kingSquare);
                        if(_check)
                        {
                            break;
                        }
                    }
                }

                Square rookSquare = SquareList.Find(o => o.ToString() ==
                        (STR_Y.Substring((king.square.Y + ((1 + increment) * progress)) - 1, 1)
                        + king.square.X.ToString()));

                king.square.Piece = null;
                kingSquare.Piece = king;
                king.square = kingSquare;

                rook.square.Piece = null;
                rookSquare.Piece = rook;
                rook.square = rookSquare;

                if (king.Color == Piece.EColor.Black)
                {
                    _turn = Piece.EColor.White;
                }
                else
                {
                    _turn = Piece.EColor.Black;
                }

                return true;


            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Validates a movement of a piece to the square.
        /// </summary>
        /// <param name="piece">The instance of the piece being moved</param>
        /// <param name="square">The instance of the destination square</param>
        public bool Movement(Piece piece, Square square)
        {
            if (_turn == piece.Color && piece.Movement(square))
            {
                if (!_setMoved[(int)piece.Color] && piece.GetType() != typeof(Pawn))
                    return false;

                // The knight is an exception, due to its particular movement, already
                // verified in its Movement method
                if (piece.GetType() == typeof(Knight))
                    return true;

                /// Here, there are three possibilities:
                /// a movement can be done in horizontal, vertical or diagonal way.
                /// Horizontal: the Xs are equal and Ys are different
                /// Vertical: the Xs are different and Ys are equal
                /// Diagonal: Both Xs and Ys are different
                /// If both Xs and Ys are equal, it means that the square is the same, so
                /// there´s no movement to perform.
                if (piece.square.X == square.X)
                {
                    // Horizontal movement
                    int progress = 1;
                    if (piece.square.Y > square.Y)
                    {
                        progress = -1;
                    }

                    for (int i = piece.square.Y + (1 * progress); i != square.Y; i = i + (1 * progress))
                    {
                        if ((SquareList.Find(o => o.ToString() ==
                            (STR_Y.Substring(i - 1, 1) + square.X.ToString()))).Piece != null)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    if (piece.square.Y == square.Y)
                    {
                        // Vertical movement
                        int progress = 1;
                        if (piece.square.X > square.X)
                        {
                            progress = -1;
                        }
                        for (int i = piece.square.X + (1 * progress); i != square.X; i = i + (1 * progress))
                        {
                            if ((SquareList.Find(o => o.ToString() ==
                                (STR_Y.Substring(square.Y - 1, 1) + i.ToString()))).Piece != null)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        // Diagonal movement
                        int progressY = 1;
                        int progressX = 1;

                        if (piece.square.Y > square.Y)
                        {
                            progressY = -1;
                        }

                        if (piece.square.X > square.X)
                        {
                            progressX = -1;
                        }

                        // Counter for Y must be initialized with the current Y square
                        int YCounter = piece.square.Y;

                        for (int i = piece.square.X + (1 * progressX); i != square.X; i = i + (1 * progressX))
                        {
                            YCounter = YCounter + (1 * progressY);
                            Piece p = (SquareList.Find(o => o.ToString() == (STR_Y.Substring(YCounter - 1, 1) + i.ToString()))).Piece;
                            if ((SquareList.Find(o => o.ToString() == (STR_Y.Substring(YCounter - 1, 1) + i.ToString()))).Piece != null)
                            {
                                return false;
                            }
                        }

                        return true;

                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Performs the movement of a piece to the square.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        public void Move(Piece piece, Square square)
        {
            if (!Movement(piece, square))
            {
                throw new Exception("The specified movement can't be performed!");
            }

            // frees the square being left
            piece.square.Piece = null;

            _setMoved[(int)piece.Color] = true;

            // If there is an opponent piece in the destination square,
            // this piece is out
            if (square.Piece != null)
            {
                square.Piece.square = null;
            }

            // Rule: En passant. This is one of the most complex movement to validate:
            // It´s related only with two opponent pawns and in a specific situation.
            // It can occur immediately after a player moves a pawn two squares forward 
            // from its starting position, and an enemy pawn could have captured it had 
            // it moved only one square forward. The opponent piece captures the just-moved pawn.
            if (piece.GetType() == typeof(Pawn) && !piece.Moved && Math.Abs(piece.square.X - square.X) == 2)
            {
                // enPassantSquare must be released after any movement different 
                // from double squared movement of a not moved pawn.
                if (_enPassantSquare != null)
                {
                    _enPassantSquare.Piece = null;
                    _enPassantSquare = null;
                }

                // enPassantSquare is just a pointer to a square. 
                _enPassantSquare = SquareList.Find(s => s.ToString() == (STR_Y.Substring(piece.square.Y - 1, 1) + ((piece.square.X + square.X) / 2).ToString()));
                _enPassantSquare.Piece = piece;
            }
            else
            {
                if (piece.GetType() == typeof(Pawn) && square == _enPassantSquare)
                {
                    // Frees all the squares which refers to a piece in the "en passant" square
                    foreach(Square squ in SquareList)
                    {
                        if (squ.Piece != null && squ.Piece == _enPassantSquare.Piece)
                        {
                            squ.Piece.square = null;
                            squ.Piece = null;
                        }
                    }
                }
                // enPassantSquare must be released after any movement different 
                // from double squared movement of a not moved pawn.
                if (_enPassantSquare != null)
                {
                    _enPassantSquare.Piece = null;
                    _enPassantSquare = null;
                }
            }

            // Places the piece on the square
            square.Piece = piece;
            piece.square = square;

            // This method can be called only after the piece square setting is done
            _check = VerifyCheck(piece);

            if (piece.Color == Piece.EColor.Black)
            {
                _turn = Piece.EColor.White;
            }
            else
            {
                _turn = Piece.EColor.Black;
            }

            Piece pieceToPromote = CheckPromotion(piece, square);

            // Replaces the pieces, if they be different from one another
            if (pieceToPromote != piece)
            {
                // Frees the pawn
                piece.square = null;

                // Places the promoted piece in the square
                square.Piece = pieceToPromote;
                pieceToPromote.square = square;
            }
        }

        /// <summary>
        /// Method that returns a list of squares that can be reach
        /// by a specific piece.
        /// </summary>
        /// <param name="piece">The piece in focus</param>
        /// <returns></returns>
        public List<Square> CanReach(Piece piece)
        {
            try
            {
                List<Square> list = new List<Square>();

                foreach (Square square in SquareList)
                {
                    if (Movement(piece, square))
                    {
                        list.Add(square);
                    }                    
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        /// <returns></returns>
        private Piece CheckPromotion(Piece piece, Square square)
        {
            Piece pieceToPromote = piece;

            // Rule: pawn promotion. If this is a regular promotion, then
            // an algorithm can solve. But if it´s an underpromotion, 
            // this situation needs a decision. This will cause
            // an interruption in the process and will become more complex.
            if (piece.GetType() == typeof(Pawn) && ((piece.Color == Piece.EColor.White &
                square.X == 8) | (piece.Color == Piece.EColor.Black &
                square.X == 1)))
            {
                List<Piece> set = null;
                if (piece.Color == Piece.EColor.White)
                {
                    set = WhiteSet;
                }
                else
                {
                    set = BlackSet;
                }

                foreach (Piece p in set)
                {
                    if (p.square == null)
                    {
                        if (pieceToPromote.MaterialValue < p.MaterialValue)
                        {
                            pieceToPromote = p;
                        }
                    }
                }

            }
            return pieceToPromote;
        }

        /// <summary>
        /// Returns a string representing the movement
        /// </summary>
        /// <param name="piece">Piece being moved</param>
        /// <param name="square">The destination square</param>
        /// <returns>A string in the following format:
        /// Piece being moved (for example, a bishop "B") + the original square (lower Y & X) + "-" + 
        /// the destination square (lower Y & X) [+ promotion]</returns>
        /// <remarks>This is a more simple format and is used as 
        /// in order to avoid applying some algoriths to determine whether there were ambiguity.
        /// Besides, it also informs possible promotions of pawns.
        /// Due to same reason as PGN format, it´s not possible to inform check and checkmate signs</remarks>
        private string getMovetext(Piece piece, Square square)
        {
            string promotion = "";

            /// Pawn promotions are notated by appending an "=" to the destination square, 
            /// followed by the piece the pawn is promoted to. For example: "e8=Q". 
            Piece pieceToPromote = CheckPromotion(piece, square);

            if (pieceToPromote != piece)
            {
                promotion = "=" + pieceToPromote.ToString();
            }

            return piece.ToString() + piece.square.ToString().ToLower() + "-" + square.ToString().ToLower() + promotion;
        }


        /// <summary>
        /// Portable Game Notation (PGN) was adopted to express the movements. Movetext details are
        /// explained above:
        /// The movetext describes the actual moves of the game. This includes move number indicators 
        /// (numbers followed by either one or three periods; one if the next move is White's move, 
        /// three if the next move is Black's move) and movetext Standard Algebraic Notation (SAN).
        /// The algebraic name of any square is as per usual Algebraic chess notation; from white's perspective, 
        /// the leftmost square closest to white is a1, the rightmost square closest to the white is h1, 
        /// and the rightmost (from white's perspective) square closest to black side is h8.
        /// An annotator who wishes to suggest alternative moves to those actually played in the game may 
        /// insert variations enclosed in parentheses. 
        /// He may also comment on the game by inserting Numeric Annotation Glyphs (NAGs) into the movetext. 
        /// Each NAG reflects a subjective impression of the move preceding the NAG or of the resultant position.
        /// If the game result is anything other than "*", the result is repeated at the end of the movetext.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        /// <returns>A PGN format string</returns>
        private string getMovetextPGN(Piece piece, Square square)
        {
            string strPiece = "";
            string strSquare;
            string disambiguation = "";
            string promotion = "";
            string capture = "";
            string sufixCheck = "";

            /// For most moves the SAN consists of the letter abbreviation for the piece
            /// The letter abbreviations are "K" (king), "Q" (queen), "R" (rook), "B" (bishop), and "N" (knight). 
            /// The pawn is given an empty abbreviation in SAN movetext, but in other contexts the abbreviation "P" is used. 
            if (piece.GetType() != typeof(Pawn))
            {
                strPiece = piece.ToString();
            }            
            
            /// ... an "x" if there is a capture
            if (square.Piece != null)
            {
                capture = "x";
            }

            /// ... and the two-character algebraic name of the final square the piece moved to. 
            strSquare = square.ToString().ToLower();

            /// Pawn promotions are notated by appending an "=" to the destination square, 
            /// followed by the piece the pawn is promoted to. For example: "e8=Q". 
            Piece pieceToPromote = CheckPromotion(piece, square);

            if (pieceToPromote != piece)
            {
                promotion = "=" + pieceToPromote.ToString();
            }

            /// Check and checkmate variants can be verified in this context
            /// because they only are changed when the movement is committed.
            ///// If the move is a checking move, the plus sign "+" is also appended; 
            //if (_check)
            //{
            //    sufixCheck = "+";
            //}

            ///// if the move is a checkmating move, the number sign "#" is appended instead. For example: "e8=Q#".
            //if (_checkmate)
            //{
            //    sufixCheck = "#";
            //}

            //Disambiguation
            /// In a few cases a more detailed representation is needed to resolve ambiguity; 
            /// if so, the piece's file letter, numerical rank, or the exact square is inserted 
            /// after the moving piece's name (in that order of preference). 
            /// Thus, "Nge2" specifies that the knight originally on the g-file moves to e2.
            if (piece.GetType() != typeof(King) & piece.GetType() != typeof(Queen))
            {
                List<Piece> set = null;
                if (piece.Color == Piece.EColor.White)
                {
                    set = WhiteSet;
                }
                else
                {
                    set = BlackSet;
                }

                foreach (Piece p in set)
                {
                    if (piece.GetType() == p.GetType() && piece != p)
                    {
                        if (p.square != null && Movement(p, square))
                        { 
                            if(piece.square.Y != p.square.Y)
                            {
                                disambiguation = piece.square.X.ToString().ToLower();
                            }else
                            {
                                if (piece.square.X != p.square.X)
                                {
                                    disambiguation = piece.square.X.ToString();
                                }                                    
                            }                            
                        }
                    }
                }                
            }

            // Returns all the information in the correct sequence
            return strPiece + disambiguation + capture + strSquare + promotion + sufixCheck;
        }


        /// <summary>
        /// SAN kingside castling is indicated by the sequence "O-O"; queenside castling is indicated 
        /// by the sequence "O-O-O" (note that these are capital letter "O"s, not numeral "0"s). 
        /// </summary>
        /// <param name="king">The king submitted to the castling</param>
        /// <param name="rook">The rook submitted to the castling</param>
        /// <returns></returns>
        private string getMovetextPGN(King king, Rook rook)
        {
            if (GetColumnDistance(king.square, rook.square) == 2)
            {
                // Kingside castling
                return "O-O";
            }
            else
            {
                // Queenside castling
                return "O-O-O";
            }            
        }

        /// <summary>
        /// Method responsible for making an actual move for the specified piece, 
        /// keeping a bookmark for the former position. Every call for this
        /// method must be followed by a corresponding call for FinishSimulation method.
        /// Besides, it must be guaranteed by a transaction context, assuring that the
        /// movement can be rolled back, in order to avoid damage to the game integrity.
        /// It is recommended that all treatment between the calls should be inside an exception 
        /// context.
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        /// <returns></returns>
        public Piece StartSimulation(Piece piece, Square square)
        {
            Piece returnPiece = square.Piece;

            return returnPiece;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        public void FinishSimulation(Piece piece, Square square)
        { 

        }

        /// <summary>
        /// This method can be called after the commit of the movement
        /// </summary>
        /// <param name="piece">The piece just moved</param>
        /// <returns></returns>
        private bool VerifyCheck(Piece piece)
        {
            List<Piece> list;
            if (piece.Color == Piece.EColor.Black)
            {
                list = WhiteSet;
            }
            else
            {
                list = BlackSet;
            }

            // The king can never have a null square reference, in that case, it´s game over
            Square square = list.Find(o => o.ToString() == "K").square;

            return VerifyCheck(piece, square);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="piece">The piece just moved or the piece candidate for
        /// the movement</param>
        /// <param name="square">The square can be informed in case being not
        /// the real position of the piece</param>
        /// <returns></returns>
        private bool VerifyCheck(Piece piece, Square square)
        {

            return Movement(piece, square);

        }

        /// <summary>
        /// This method can be called after any method of check verification
        /// has returned true
        /// </summary>
        /// <param name="king"></param>
        /// <param name="piece"></param>
        /// <returns></returns>
        private bool VerifyCheckMate(King king, Piece piece)
        {
            // There are basically three kinds of verifications to be made 
            // to achieve a complete checkmate´s verification:
            // 1. Can any piece (including the king) capture the opponent?
            if (Movement(king, piece.square))
            {
                // 1.1 If the king is the only piece able to capture the opponent, 
                //     is it free of being caught in the destination square?
                //     In this situation, there must be necessary to make a simulation.
                if (true)
                {
                    return false;
                }
            }
            else
            {
                List<Piece> set = null;
                if (king.Color == Piece.EColor.White)
                {
                    set = WhiteSet;
                }
                else
                {
                    set = BlackSet;
                }

                foreach (Piece p in set)
                {
                    if (p.square != null)
                    {
                        if (Movement(p, piece.square))
                        {
                            // 1.2 Is the king free of being caught if this piece is moved?
                            //     In this situation, there must be necessary to make a simulation.
                            if (true)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            // 2. Can the king escape the menace (from any opponent pieces)
            for (int x = king.square.X - 1; x <= king.square.X + 1; x++)
            {
                for (int y = king.square.Y - 1; y <= king.square.Y + 1; y++)
                {
                    if ((x >= 1 & x <= 8) & (y >= 1 & y <= 8))
                    {
                        Square square = SquareList.Find(o => o.ToString() == (STR_Y.Substring(y - 1, 1) + x.ToString()));
                        if (Movement(king, square))
                        {
                            // 2.1 Is the king free of being caught in the destination square?
                            //     In this situation, there must be necessary to make a simulation.
                            if (true)
                            {
                                return false;
                            }
                        }
                    }
                }
            }


            // 3. Can any other piece be placed between the king and the opponent piece, without 
            //    causing another check*?
            // * this verification can be done by simulate the position of the defensive piece and
            //   recursive calls to this method.
            return true;
        }

        #endregion

        #region Public attributes
        /// <summary>
        /// This attribute will be set true when the king is under a simple check.
        /// </summary>
        public bool Check
        {
            get
            {
                return _check;
            }
        }

        /// <summary>
        /// Read only property that shows which player, returning the piece color, 
        /// is the next player
        /// </summary>
        public Piece.EColor Turn
        {
            get
            {
                return _turn;
            }
        }
        #endregion


        public static bool IsSameColumn(Square square1, Square square2)
        {
            return square1.Y == square2.Y ;
        }

        public static bool IsSameRow(Square square1, Square square2)
        {
            return square1.X == square2.X;
        }

        public static bool IsNextColumn(Square square1, Square square2)
        {
            return Math.Abs(square1.Y-square2.Y)==1;
        }

        public static bool IsNextRow(Square square1, Square square2)
        {
            return Math.Abs(square1.X - square2.X)==1;
        }

        public static bool IsNextColumn(Square square1, Square square2, int distance)
        {
            return Math.Abs(square1.Y - square2.Y) == distance;
        }

        public static bool IsNextRow(Square square1, Square square2, int distance)
        {
            return Math.Abs(square1.X - square2.X) == distance;
        }

        internal static int GetColumnDistance(Square square1, Square square2)
        {
            return Math.Abs(square1.Y - square2.Y);
        }

        internal static int GetRowDistance(Square square1, Square square2)
        {
            return Math.Abs(square1.X - square2.X);
        }

    }
}
