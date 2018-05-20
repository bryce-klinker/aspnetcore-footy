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
            using (var context = GetService<FootyContext>())
            {
                foreach (var entity in entities)
                    context.Service.Add(entity);
                await context.Service.SaveChangesAsync();
            }
        }

        public ScopedService<T> GetService<T>()
        {
            var scope = GetScope();
            return new ScopedService<T>(scope);
        }

        public void Dispose()
        {
            using (var context = GetService<FootyContext>())
                context.Service.Database.EnsureDeleted();
            
            _server.Dispose();
        }

        private IServiceScope GetScope()
        {
            return _server.Host.Services.CreateScope();
        }
    }
}