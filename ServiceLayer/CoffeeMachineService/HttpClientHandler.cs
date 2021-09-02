using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CoffeeMachineService.Interfaces;
using CoffeeMachineService.ViewModels;

namespace CoffeeMachineService
{
    public class HttpClientHandler : IHttpHandler
    {
        private readonly HttpClient _client;
        public HttpClientHandler(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<string> GetAsync(string url)
        {
            var response = await _client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var dataContent = await response.Content.ReadAsStringAsync();
            return dataContent;
        }
    }
}