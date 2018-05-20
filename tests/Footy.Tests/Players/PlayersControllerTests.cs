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

        [Fact]
        public async Task ShouldPopulatePlayer()
        {
            var player = PlayerFactory.Create();
            _context.Add(player);
            _context.SaveChanges();

            var result = (OkObjectResult) await _controller.GetAll();
            AssertPlayer(player, result.Value<PlayerDto[]>()[0]);
        }

        [Fact]
        public async Task ShouldGetPlayer()
        {
            var player = PlayerFactory.Create();
            _context.Add(player);
            _context.SaveChanges();

            var result = (OkObjectResult) await _controller.GetById(player.Id);
            AssertPlayer(player, result.Value<PlayerDto>());
        }

        [Fact]
        public async Task ShouldReturn404IfPlayerIsNotFound()
        {
            var result = await _controller.GetById(54);
            Assert.IsType<NotFoundResult>(result);
        }

        private static void AssertPlayer(Player player, PlayerDto dto)
        {
            Assert.Equal(player.FirstName, dto.FirstName);
            Assert.Equal(player.Surname, dto.Surname);
            Assert.Equal(player.Id, dto.Id);
        }
    }
}