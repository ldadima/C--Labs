using System.Net.Http;

namespace BookDeliveryApplication.Services
{
    public class ServiceProxy : IServiceProxy
    {
        private readonly HttpClient _httpClient;
        private const string Url = "https://getbooksrestapi.azurewebsites.net/api/books";

        public ServiceProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void GetAndSaveBooks()
        {
            var response = await _httpClient.GetAsync($"{Url}/10");
        }
    }
}