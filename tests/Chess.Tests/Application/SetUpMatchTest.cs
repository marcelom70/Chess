using System;
using Chess.Application.Contracts;
using Chess.Application.Contracts.DTOs;
using EasyArchitecture.Configuration;
using EasyArchitecture.IoC;
using EasyArchitecture.Validation.Instance;
using NUnit.Framework;

namespace Chess.Tests.Application
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

            Assert.That(()=>facade.SetUpMatch(whitePlayer, blackPlayer), Throws.TypeOf<InvalidEntityException>());
        }

        [Test]
        public void Should_not_create_match_with_unknow_player()
        {
            var facade = Container.Resolve<IChessFacade>();

            var blackPlayer = new PlayerDTO() { Name = "henriquericcio", Id = Guid.NewGuid() };
            var whitePlayer = new PlayerDTO() { Name = "marcelom"};

            Assert.That(() => facade.SetUpMatch(whitePlayer, blackPlayer), Throws.TypeOf<InvalidEntityException>());
        }
    }
}
