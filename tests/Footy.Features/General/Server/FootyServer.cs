using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Footy.Rest.Api;
using Footy.Rest.Api.General;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Polly;

namespace Footy.Features.General.Server
{
    public class FootyServer : IDisposable
    {
        private readonly TestServer _server;

        public FootyServer()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>());
        }

        public HttpClient CreateClient()
        {
            return _server.CreateClient();
        }

        public async Task AddAsync<T>(params T[] entities)
            where T : class
        {
            using (var scope = GetScope())
            using (var context = GetService<FootyContext>(scope))
            {
                Console.WriteLine($"Entity Type: {typeof(T)}");
                await context.Players.ToArrayAsync();
                foreach (var entity in entities)
                    context.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            using (var scope = GetScope())
            using (var context = GetService<FootyContext>(scope))
                context.Database.EnsureDeleted();
            
            _server.Dispose();
        }

        private IServiceScope GetScope()
        {
            return _server.Host.Services.CreateScope();
        }

        private static T GetService<T>(IServiceScope scope)
        {
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}