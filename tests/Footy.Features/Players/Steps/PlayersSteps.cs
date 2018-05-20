using System.Linq;
using System.Threading.Tasks;
using Footy.Features.General;
using Footy.Features.General.Scenarios;
using Footy.Features.General.Server;
using Footy.Features.General.Steps;
using Footy.Models.Players;
using Footy.Rest.Api.General;
using Footy.Rest.Api.Players;
using Footy.TestUtilities.Players;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace Footy.Features.Players.Steps
{
    public class PlayersSteps
    {
        public ITestOutputHelper Output { get; }

        public PlayersSteps(ITestOutputHelper output)
        {
            Output = output;
        }

        [Given("(.*) players are available")]
        public async Task GivenImportedPlayers(int number)
        {
            var entities = Enumerable.Range(0, number).Select(i => PlayerFactory.Create()).ToArray();
            var server = ScenarioContext.Current.FootyServer();
            await server.AddAsync(entities);
        }

        [Given("player with name (.*) (.*)")]
        public async Task GivenPlayerNamed(string firstname, string surname)
        {
            var player = PlayerFactory.Create(firstname, surname);
            var server = ScenarioContext.Current.FootyServer();
            await server.AddAsync(player);
        }

        [When("I get all players")]
        public async Task WhenIGetAllPlayers()
        {
            var players = await ScenarioContext.Current.GetJsonAsync<PlayerDto[]>("/players");
            ScenarioContext.Current.Players(players);
        }

        [When("I get player with name (.*) (.*)")]
        public async Task WhenIGetPlayerWithName(string firstname, string surname)
        {
            var server = ScenarioContext.Current.FootyServer();
            using (var service = server.GetService<FootyContext>())
            {
                var context = service.Service;
                var player = await context.Players
                    .Where(p => p.FirstName == firstname)
                    .SingleAsync(p => p.Surname == surname);
                var dto = await ScenarioContext.Current.GetJsonAsync<PlayerDto>($"/players/{player.Id}");
                ScenarioContext.Current.Player(dto);
            }
        }

        [Then("I should see (.*) players")]
        public void ThenIShouldSeePlayers(int number)
        {
            var players = ScenarioContext.Current.Players();
            Assert.Equal(number, players.Length);
        }

        [Then("I should see player with name (.*) (.*)")]
        public void ThenIShouldSeePlayerWithName(string firstname, string surname)
        {
            var player = ScenarioContext.Current.Player();
            Assert.Equal(firstname, player.FirstName);
            Assert.Equal(surname, player.Surname);
        }
    }
}