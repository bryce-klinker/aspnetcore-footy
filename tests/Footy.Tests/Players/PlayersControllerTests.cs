using System.Threading.Tasks;
using Footy.Models.Players;
using Footy.Rest.Api.General;
using Footy.Rest.Api.Players;
using Footy.TestUtilities.General;
using Footy.TestUtilities.Players;
using Microsoft.AspNetCore.Mvc;
using Footy.TestUtilities;
using Xunit;

namespace Footy.Tests.Players
{
    public class PlayersControllerTests
    {
        private readonly FootyContext _context;
        private readonly PlayersController _controller;

        public PlayersControllerTests()
        {
            _context = ContextFactory.InMemory();
            _controller = new PlayersController(_context);
        }

        [Fact]
        public async Task ShouldReturnAllPlayers()
        {
            _context.Add(PlayerFactory.Create());
            _context.Add(PlayerFactory.Create());
            _context.Add(PlayerFactory.Create());
            _context.SaveChanges();

            var result = (OkObjectResult) await _controller.GetAll();
            Assert.Equal(3, result.Value<PlayerDto[]>().Length);
        }
    }
}