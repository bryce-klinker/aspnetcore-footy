using System;
using Footy.Rest.Api.General;
using Microsoft.EntityFrameworkCore;

namespace Footy.TestUtilities.General
{
    public static class ContextFactory
    {
        public static FootyContext InMemory()
        {
            var options = new DbContextOptionsBuilder<FootyContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new FootyContext(options);
        }
    }
}