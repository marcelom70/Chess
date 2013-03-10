using System;
using Chess.Application.Contracts;
using Chess.Application.Contracts.DTOs;
using EasyArchitecture.Configuration;
using EasyArchitecture.Mechanisms.IoC;
using NUnit.Framework;

namespace Chess.Tests.Application
{
    [TestFixture]
    public class MoveTest
    {
        private Guid _matchId1;
        private Guid _matchId2;

        [SetUp]
        public void SetUp()
        {
            Configure
                .For<IChessFacade>()
                .Done();

            var facade = Container.Resolve<IChessFacade>();

            var blackPlayer = new PlayerDTO() { Name = "henriquericcio", Id = Guid.NewGuid() };
            var whitePlayer = new PlayerDTO() { Name = "marcelom", Id = Guid.NewGuid() };

            _matchId1 = facade.SetUpMatch(whitePlayer, blackPlayer);
            _matchId2 = facade.SetUpMatch(whitePlayer, blackPlayer, "8/8/8/2RNBQ2/2KBNR2/8/8/8");

        }

        [Test]
        public void Cannot_move_an_inexistent_piece()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string impossibleMove = "e4e8";

            Assert.That(()=> facade.DoMove(impossibleMove, _matchId1),Throws.Exception);
        }

        [Test]
        public void Cannot_move_to_the_same_position()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string samePosition = "a7a7";
            Assert.That(() => facade.DoMove(samePosition, _matchId1), Throws.Exception);
        }

        [Test]
        public void Cannot_move_to_an_inexisting_position()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string inexistingPosition = "a7z9";
            Assert.That(()=>facade.DoMove(inexistingPosition, _matchId1), Throws.Exception);
        }

        [Test]
        public void Should_not_move_on_other_player_turn()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string validMoveForBlackPlayer = "c7c5";
            Assert.That(() => facade.DoMove(validMoveForBlackPlayer, _matchId1), Throws.Exception);
        }

        [Test]
        public void Cannot_move_king_more_than_one_square()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string validMoveForBlackPlayer = "c4a2";
            Assert.That(() => facade.DoMove(validMoveForBlackPlayer, _matchId2), Throws.Exception);
        }
    }
}
