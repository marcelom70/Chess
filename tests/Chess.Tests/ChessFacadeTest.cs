using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Application.Contracts;
using Chess.Application.Contracts.DTOs;
using EasyArchitecture.Configuration;
using EasyArchitecture.IoC;
using NUnit.Framework;

namespace Chess.Tests
{
    [TestFixture]
    public class ChessFacadeTest
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
            //prepare
            var facade = Container.Resolve<IChessFacade>();

            var blackPlayer = new PlayerDTO() {Name = "henriquericcio", Id = Guid.NewGuid()};
            var whitePlayer = new PlayerDTO() { Name = "marcelom", Id = Guid.NewGuid() };

            //execute
            var matchId = facade.SetUpMatch(whitePlayer,blackPlayer);

            //eval
            Assert.That(matchId,Is.Not.EqualTo(Guid.Empty));
        }

    }
}
