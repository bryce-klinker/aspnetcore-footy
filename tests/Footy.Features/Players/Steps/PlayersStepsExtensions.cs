using Footy.Features.General.Scenarios;
using Footy.Models.Players;

namespace Footy.Features.Players.Steps
{
    public static class PlayersStepsExtensions
    {
        private const string PlayersKey = "players";
        private const string PlayerKey = "player";
        
        public static PlayerDto[] Players(this ScenarioContext context)
        {
            return context.Get<PlayerDto[]>(PlayersKey);
        }

        public static void Players(this ScenarioContext context, PlayerDto[] playersDto)
        {
            context.Set(PlayersKey, playersDto);
        }

        public static PlayerDto Player(this ScenarioContext context)
        {
            return context.Get<PlayerDto>(PlayerKey);
        }

        public static void Player(this ScenarioContext context, PlayerDto playerDto)
        {
            context.Set(PlayerKey, playerDto);
        }
    }
}