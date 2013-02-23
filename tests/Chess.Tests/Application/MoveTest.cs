using System;
using Chess.Application.Contracts;
using Chess.Application.Contracts.DTOs;
using EasyArchitecture.Configuration;
using EasyArchitecture.IoC;
using NUnit.Framework;

namespace Chess.Tests.Application
{
    [TestFixture]
    public class MoveTest
    {
        private Guid _matchId;

        [SetUp]
        public void SetUp()
        {
            Configure
                .For<IChessFacade>()
                .Done();

            var facade = Container.Resolve<IChessFacade>();

            var blackPlayer = new PlayerDTO() { Name = "henriquericcio", Id = Guid.NewGuid() };
            var whitePlayer = new PlayerDTO() { Name = "marcelom", Id = Guid.NewGuid() };

            _matchId = facade.SetUpMatch(whitePlayer, blackPlayer);

        }

        [Test]
        public void Can_move_an_inexistent_piece()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string impossibleMove = "e4e8";

            //Assert.That(()=> facade.DoMove(impossibleMove, _matchId)==null,Throws.Exception);
            Assert.Inconclusive("deve receber um comando de retorno dizendo que o movimento é impossível e este deve ser a verificação");
        }

        [Test]
        public void Can_move_to_the_same_position()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string samePosition = "a7a7";
            Assert.That(() => facade.DoMove(samePosition, _matchId), Throws.TypeOf<Exception>());
        }

        [Test]
        public void Can_move_to_an_inexisting_position()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string inexistingPosition = "a7z9";
            Assert.That(()=>facade.DoMove(inexistingPosition, _matchId), Throws.TypeOf<Exception>());
        }

        [Test]
        public void Is_player_turn()
        {
            var facade = Container.Resolve<IChessFacade>();
            const string validMoveForBlackPlayer = "c7c5";
            Assert.That(() => facade.DoMove(validMoveForBlackPlayer, _matchId), Throws.TypeOf<Exception>());
        }
    }
}
