using System.Collections.Generic;
using System.Net.Http;
using BookShop;
using Newtonsoft.Json;

namespace WebShopApplication.Services
{
    public class ServiceProxy : IServiceProxy
    {
        private readonly MarketSystem _marketSystem;
        private readonly HttpClient _httpClient;
        private const string Url = "https://getbooksrestapi.azurewebsites.net/api/books";

        public ServiceProxy(HttpClient httpClient, MarketSystem marketSystem)
        {
            _httpClient = httpClient;
            _marketSystem = marketSystem;
        }

        public async void GetAndSaveBooks()
        {
            if (!_marketSystem.IsNeedSomeBooks())
            {
                return;
            }
            var response = await _httpClient.GetAsync($"{Url}/10");
            var books = JsonConvert.DeserializeObject<List<Book>>(response.Content.ToString());
            _marketSystem.BookReception(books);
        }
    }
}