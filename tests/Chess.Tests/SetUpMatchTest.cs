using System;
using Chess.Application.Contracts;
using Chess.Application.Contracts.DTOs;
using EasyArchitecture.Configuration;
using EasyArchitecture.IoC;
using NUnit.Framework;

namespace Chess.Tests
{
    [TestFixture]
    public class SetUpMatchTest
    {
        [SetUp]
        public void SetUp()
        {
            Configure
                .For<IChessFacade>()
                .Done();
        }

        [Test]
        public void Can_create_match()
        {
            var facade = Container.Resolve<IChessFacade>();

            var blackPlayer = new PlayerDTO() {Name = "henriquericcio", Id = Guid.NewGuid()};
            var whitePlayer = new PlayerDTO() { Name = "marcelom", Id = Guid.NewGuid() };

            var matchId = facade.SetUpMatch(whitePlayer,blackPlayer);

            Assert.That(matchId,Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void Should_not_create_match_with_nameless_player()
        {
            var facade = Container.Resolve<IChessFacade>();

            var blackPlayer = new PlayerDTO() {  Id = Guid.NewGuid() };
            var whitePlayer = new PlayerDTO() { Name = "marcelom", Id = Guid.NewGuid() };

            Assert.That(() => facade.SetUpMatch(whitePlayer, blackPlayer), Throws.Exception);
            //Assert.That(()=>facade.SetUpMatch(whitePlayer, blackPlayer), Throws.TypeOf<InvalidEntityException>());
        }

        [Test]
        public void Should_not_create_match_with_unknow_player()
        {
            var facade = Container.Resolve<IChessFacade>();

            var blackPlayer = new PlayerDTO() { Name = "henriquericcio", Id = Guid.NewGuid() };
            var whitePlayer = new PlayerDTO() { Name = "marcelom"};

            Assert.That(() => facade.SetUpMatch(whitePlayer, blackPlayer), Throws.Exception);
            //Assert.That(() => facade.SetUpMatch(whitePlayer, blackPlayer), Throws.TypeOf<InvalidEntityException>());
        }

        [Test]
        public void Can_create_match_with_a_custom_setup_board()
        {
            var facade = Container.Resolve<IChessFacade>();

            var blackPlayer = new PlayerDTO() { Name = "henriquericcio", Id = Guid.NewGuid() };
            var whitePlayer = new PlayerDTO() { Name = "marcelom", Id = Guid.NewGuid() };

            //peao branco em e4 (equivalente ao primeiro lance 1e4)
            //const string boardSetup = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR";
            const string boardSetup = "rnbqkbnr/pppppppp/8/8/P2P3P/8/1PP1PPP1/RNBQKBNR";

            var matchId = facade.SetUpMatch(whitePlayer, blackPlayer,boardSetup);

            //encontrar uma forma de verificar status

            Assert.That(matchId, Is.Not.EqualTo(Guid.Empty));
        }

    }
}
