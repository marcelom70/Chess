using Chess.Domain.Entities;
using NUnit.Framework;

namespace Chess.Tests.Domain
{
    [TestFixture]
    public class BoardTest
    {
        [Test]
        public void Can_default_setup_a_board()
        {
            var board = new Board(null);

            Assert.That(board.GetPiece("A1"), Is.EqualTo(new Rook("white", "A1")));
            Assert.That(board.GetPiece("B1"), Is.EqualTo(new Knight("white", "B1")));
            Assert.That(board.GetPiece("C1"), Is.EqualTo(new Bishop("white", "C1")));
            Assert.That(board.GetPiece("D1"), Is.EqualTo(new Queen("white", "D1")));
            Assert.That(board.GetPiece("E1"), Is.EqualTo(new King("white", "E1")));
            Assert.That(board.GetPiece("F1"), Is.EqualTo(new Bishop("white", "F1")));
            Assert.That(board.GetPiece("G1"), Is.EqualTo(new Knight("white", "G1")));
            Assert.That(board.GetPiece("H1"), Is.EqualTo(new Rook("white", "H1")));
            Assert.That(board.GetPiece("A2"), Is.EqualTo(new Pawn("white", "A2")));
            Assert.That(board.GetPiece("B2"), Is.EqualTo(new Pawn("white", "B2")));
            Assert.That(board.GetPiece("C2"), Is.EqualTo(new Pawn("white", "C2")));
            Assert.That(board.GetPiece("D2"), Is.EqualTo(new Pawn("white", "D2")));
            Assert.That(board.GetPiece("E2"), Is.EqualTo(new Pawn("white", "E2")));
            Assert.That(board.GetPiece("F2"), Is.EqualTo(new Pawn("white", "F2")));
            Assert.That(board.GetPiece("G2"), Is.EqualTo(new Pawn("white", "G2")));
            Assert.That(board.GetPiece("H2"), Is.EqualTo(new Pawn("white", "H2")));
            Assert.That(board.GetPiece("A3"), Is.Null);
            Assert.That(board.GetPiece("B3"), Is.Null);
            Assert.That(board.GetPiece("C3"), Is.Null);
            Assert.That(board.GetPiece("D3"), Is.Null);
            Assert.That(board.GetPiece("E3"), Is.Null);
            Assert.That(board.GetPiece("F3"), Is.Null);
            Assert.That(board.GetPiece("G3"), Is.Null);
            Assert.That(board.GetPiece("H3"), Is.Null);
            Assert.That(board.GetPiece("A4"), Is.Null);
            Assert.That(board.GetPiece("B4"), Is.Null);
            Assert.That(board.GetPiece("C4"), Is.Null);
            Assert.That(board.GetPiece("D4"), Is.Null);
            Assert.That(board.GetPiece("E4"), Is.Null);
            Assert.That(board.GetPiece("F4"), Is.Null);
            Assert.That(board.GetPiece("G4"), Is.Null);
            Assert.That(board.GetPiece("H4"), Is.Null);
            Assert.That(board.GetPiece("A5"), Is.Null);
            Assert.That(board.GetPiece("B5"), Is.Null);
            Assert.That(board.GetPiece("C5"), Is.Null);
            Assert.That(board.GetPiece("D5"), Is.Null);
            Assert.That(board.GetPiece("E5"), Is.Null);
            Assert.That(board.GetPiece("F5"), Is.Null);
            Assert.That(board.GetPiece("G5"), Is.Null);
            Assert.That(board.GetPiece("H5"), Is.Null);
            Assert.That(board.GetPiece("A6"), Is.Null);
            Assert.That(board.GetPiece("B6"), Is.Null);
            Assert.That(board.GetPiece("C6"), Is.Null);
            Assert.That(board.GetPiece("D6"), Is.Null);
            Assert.That(board.GetPiece("E6"), Is.Null);
            Assert.That(board.GetPiece("F6"), Is.Null);
            Assert.That(board.GetPiece("G6"), Is.Null);
            Assert.That(board.GetPiece("H6"), Is.Null);
            Assert.That(board.GetPiece("A7"), Is.EqualTo(new Pawn("black", "A7")));
            Assert.That(board.GetPiece("B7"), Is.EqualTo(new Pawn("black", "B7")));
            Assert.That(board.GetPiece("C7"), Is.EqualTo(new Pawn("black", "C7")));
            Assert.That(board.GetPiece("D7"), Is.EqualTo(new Pawn("black", "D7")));
            Assert.That(board.GetPiece("E7"), Is.EqualTo(new Pawn("black", "E7")));
            Assert.That(board.GetPiece("F7"), Is.EqualTo(new Pawn("black", "F7")));
            Assert.That(board.GetPiece("G7"), Is.EqualTo(new Pawn("black", "G7")));
            Assert.That(board.GetPiece("H7"), Is.EqualTo(new Pawn("black", "H7")));
            Assert.That(board.GetPiece("A8"), Is.EqualTo(new Rook("black", "A8")));
            Assert.That(board.GetPiece("B8"), Is.EqualTo(new Knight("black", "B8")));
            Assert.That(board.GetPiece("C8"), Is.EqualTo(new Bishop("black", "C8")));
            Assert.That(board.GetPiece("D8"), Is.EqualTo(new Queen("black", "D8")));
            Assert.That(board.GetPiece("E8"), Is.EqualTo(new King("black", "E8")));
            Assert.That(board.GetPiece("F8"), Is.EqualTo(new Bishop("black", "F8")));
            Assert.That(board.GetPiece("G8"), Is.EqualTo(new Knight("black", "G8")));
            Assert.That(board.GetPiece("H8"), Is.EqualTo(new Rook("black", "H8")));
        }

        [Test]
        public void Can_customize_board_setup()
        {
            var board = new Board("rnbqkbnr/pppppppp/8/8/P2P3P/8/1PP1PPP1/RNBQKBNR");

            Assert.That(board.GetPiece("A1"), Is.EqualTo(new Rook("white", "A1")));
            Assert.That(board.GetPiece("B1"), Is.EqualTo(new Knight("white", "B1")));
            Assert.That(board.GetPiece("C1"), Is.EqualTo(new Bishop("white", "C1")));
            Assert.That(board.GetPiece("D1"), Is.EqualTo(new Queen("white", "D1")));
            Assert.That(board.GetPiece("E1"), Is.EqualTo(new King("white", "E1")));
            Assert.That(board.GetPiece("F1"), Is.EqualTo(new Bishop("white", "F1")));
            Assert.That(board.GetPiece("G1"), Is.EqualTo(new Knight("white", "G1")));
            Assert.That(board.GetPiece("H1"), Is.EqualTo(new Rook("white", "H1")));
            Assert.That(board.GetPiece("A2"), Is.Null);
            Assert.That(board.GetPiece("B2"), Is.EqualTo(new Pawn("white", "B2")));
            Assert.That(board.GetPiece("C2"), Is.EqualTo(new Pawn("white", "C2")));
            Assert.That(board.GetPiece("D2"), Is.Null);
            Assert.That(board.GetPiece("E2"), Is.EqualTo(new Pawn("white", "E2")));
            Assert.That(board.GetPiece("F2"), Is.EqualTo(new Pawn("white", "F2")));
            Assert.That(board.GetPiece("G2"), Is.EqualTo(new Pawn("white", "G2")));
            Assert.That(board.GetPiece("H2"), Is.Null);
            Assert.That(board.GetPiece("A3"), Is.Null);
            Assert.That(board.GetPiece("B3"), Is.Null);
            Assert.That(board.GetPiece("C3"), Is.Null);
            Assert.That(board.GetPiece("D3"), Is.Null);
            Assert.That(board.GetPiece("E3"), Is.Null);
            Assert.That(board.GetPiece("F3"), Is.Null);
            Assert.That(board.GetPiece("G3"), Is.Null);
            Assert.That(board.GetPiece("H3"), Is.Null);
            Assert.That(board.GetPiece("A4"), Is.EqualTo(new Pawn("white", "A4")));
            Assert.That(board.GetPiece("B4"), Is.Null);
            Assert.That(board.GetPiece("C4"), Is.Null);
            Assert.That(board.GetPiece("D4"), Is.EqualTo(new Pawn("white", "D4")));
            Assert.That(board.GetPiece("E4"), Is.Null);
            Assert.That(board.GetPiece("F4"), Is.Null);
            Assert.That(board.GetPiece("G4"), Is.Null);
            Assert.That(board.GetPiece("H4"), Is.EqualTo(new Pawn("white", "H4")));
            Assert.That(board.GetPiece("A5"), Is.Null);
            Assert.That(board.GetPiece("B5"), Is.Null);
            Assert.That(board.GetPiece("C5"), Is.Null);
            Assert.That(board.GetPiece("D5"), Is.Null);
            Assert.That(board.GetPiece("E5"), Is.Null);
            Assert.That(board.GetPiece("F5"), Is.Null);
            Assert.That(board.GetPiece("G5"), Is.Null);
            Assert.That(board.GetPiece("H5"), Is.Null);
            Assert.That(board.GetPiece("A6"), Is.Null);
            Assert.That(board.GetPiece("B6"), Is.Null);
            Assert.That(board.GetPiece("C6"), Is.Null);
            Assert.That(board.GetPiece("D6"), Is.Null);
            Assert.That(board.GetPiece("E6"), Is.Null);
            Assert.That(board.GetPiece("F6"), Is.Null);
            Assert.That(board.GetPiece("G6"), Is.Null);
            Assert.That(board.GetPiece("H6"), Is.Null);
            Assert.That(board.GetPiece("A7"), Is.EqualTo(new Pawn("black", "A7")));
            Assert.That(board.GetPiece("B7"), Is.EqualTo(new Pawn("black", "B7")));
            Assert.That(board.GetPiece("C7"), Is.EqualTo(new Pawn("black", "C7")));
            Assert.That(board.GetPiece("D7"), Is.EqualTo(new Pawn("black", "D7")));
            Assert.That(board.GetPiece("E7"), Is.EqualTo(new Pawn("black", "E7")));
            Assert.That(board.GetPiece("F7"), Is.EqualTo(new Pawn("black", "F7")));
            Assert.That(board.GetPiece("G7"), Is.EqualTo(new Pawn("black", "G7")));
            Assert.That(board.GetPiece("H7"), Is.EqualTo(new Pawn("black", "H7")));
            Assert.That(board.GetPiece("A8"), Is.EqualTo(new Rook("black", "A8")));
            Assert.That(board.GetPiece("B8"), Is.EqualTo(new Knight("black", "B8")));
            Assert.That(board.GetPiece("C8"), Is.EqualTo(new Bishop("black", "C8")));
            Assert.That(board.GetPiece("D8"), Is.EqualTo(new Queen("black", "D8")));
            Assert.That(board.GetPiece("E8"), Is.EqualTo(new King("black", "E8")));
            Assert.That(board.GetPiece("F8"), Is.EqualTo(new Bishop("black", "F8")));
            Assert.That(board.GetPiece("G8"), Is.EqualTo(new Knight("black", "G8")));
            Assert.That(board.GetPiece("H8"), Is.EqualTo(new Rook("black", "H8")));
        }
    }
}
