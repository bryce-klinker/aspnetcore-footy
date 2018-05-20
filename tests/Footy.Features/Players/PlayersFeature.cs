using System.Threading.Tasks;
using Footy.Features.General.Features;
using Footy.Features.General.Scenarios;
using Footy.Features.General.Steps;
using Xunit;
using Xunit.Abstractions;

namespace Footy.Features.Players
{
    public class PlayersFeature : Feature
    {
        public PlayersFeature(ITestOutputHelper output)
            : base(output)
        {
            
        }

        [Fact]
        [Scenario("Get All Players")]
        public async Task GetAllPlayers()
        {
            await Given("5 players are available");
            await When("I get all players");
            await Then("I should see 5 players");
        }
    }
}