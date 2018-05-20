using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Footy.Features
{
    public static class HttpClientExtensions
    {
        public static async Task<T> GetJsonAsync<T>(this HttpClient client, string url)
        {
            var json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}