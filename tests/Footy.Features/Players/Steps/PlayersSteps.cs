using System.Linq;
using System.Threading.Tasks;
using Footy.Features.General;
using Footy.Features.General.Scenarios;
using Footy.Features.General.Server;
using Footy.Features.General.Steps;
using Footy.Models.Players;
using Footy.Rest.Api.Players;
using Footy.TestUtilities.Players;
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

        [When("I get all players")]
        public async Task WhenIGetAllPlayers()
        {
            var players = await ScenarioContext.Current.GetJsonAsync<PlayerDto[]>("/players");
            ScenarioContext.Current.Players(players);
        }

        [Then("I should see (.*) players")]
        public void ThenIShouldSeePlayers(int number)
        {
            var players = ScenarioContext.Current.Players();
            Assert.Equal(number, players.Length);
        }
    }

    public static class PlayersStepsExtensions
    {
        private const string PlayersKey = "players";
        
        public static PlayerDto[] Players(this ScenarioContext context)
        {
            return context.Get<PlayerDto[]>(PlayersKey);
        }

        public static void Players(this ScenarioContext context, PlayerDto[] playersDto)
        {
            context.Set(PlayersKey, playersDto);
        }
    }
}