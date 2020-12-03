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
        
        #warning можно превратить в константу, как подсказывает решарпер
        private readonly string _url = "https://getbooksrestapi.azurewebsites.net/api/books";

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
            #warning _url + 10 и получится https://getbooksrestapi.azurewebsites.net/api/books10. 
            #warning во-первых, это неверный url, по такому запросу ничего не придёт, ты проверял что этот код работает? 
            #warning во-вторых, лучше использовать интерполяцию
            var response = await _httpClient.GetAsync(_url + 10);
            var books = JsonConvert.DeserializeObject<List<Book>>(response.Content.ToString());
            _marketSystem.BookReception(books);
        }
    }
}