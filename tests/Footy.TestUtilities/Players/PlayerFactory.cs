using System;
using Footy.Rest.Api.Players;

namespace Footy.TestUtilities.Players
{
    public static class PlayerFactory
    {
        public static Player Create(string firstName = null, string surname = null)
        {
            return new Player
            {
                FirstName = firstName ?? Guid.NewGuid().ToString(),
                Surname = surname ?? Guid.NewGuid().ToString()
            };
        }
    }
}