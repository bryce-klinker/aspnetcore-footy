using Footy.Features.General.Steps;
using Xunit.Abstractions;

namespace Footy.Features.Players.Steps
{
    public class PlayersSteps
    {
        public PlayersSteps(ITestOutputHelper output)
        {
            Output = output;
        }

        public ITestOutputHelper Output { get; }

        [Given("(.*) players have been imported")]
        public void GivenImportedPlayers(int number)
        {
            Output.WriteLine("Executing players have been imported...");
        }

        [When("I get all players")]
        public void WhenIGetAllPlayers()
        {
            Output.WriteLine("Executing get all players...");
        }

        [Then("I should see (.*) players")]
        public void ThenIShouldSeePlayers(int number)
        {
            Output.WriteLine("Executing see all players...");
        }
    }
}