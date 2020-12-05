using System.Net.Http;

namespace BookDeliveryApplication.Services
{
    public class ServiceBookProxy : IServiceProxy
    {
        private readonly HttpClient _httpClient;
        private const string Url = "https://getbooksrestapi.azurewebsites.net/api/books";

        public ServiceBookProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async void GetAndSaveBooks(int bookCount)
        {
            var response = await _httpClient.GetAsync($"{Url}/{bookCount}");
        }
    }
}