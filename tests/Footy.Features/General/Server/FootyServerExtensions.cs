using System.Net.Http;
using System.Threading.Tasks;
using Footy.Features.General.Scenarios;

namespace Footy.Features.General.Server
{
    public static class FootyServerExtensions
    {
        private const string FootyServerKey = "footy-server";
        
        public static FootyServer FootyServer(this ScenarioContext context)
        {
            return context.GetOrAdd(FootyServerKey, () => new FootyServer());
        }

        public static HttpClient CreateClient(this ScenarioContext context)
        {
            return context.FootyServer().CreateClient();
        }

        public static async Task<T> GetJsonAsync<T>(this ScenarioContext context, string url)
        {
            using (var client = context.CreateClient())
                return await client.GetJsonAsync<T>(url);
        }
    }
}