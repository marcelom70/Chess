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
            Assert.That(()=> facade.DoMove(impossibleMove, _matchId),Throws.Exception);
        }
    }
}
