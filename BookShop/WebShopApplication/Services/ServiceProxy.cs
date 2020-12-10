using System.Collections.Generic;
using System.Net.Http;
using ApplicationServices;
using BookShop;
using Newtonsoft.Json;

namespace WebShopApplication.Services
{
    public class ServiceProxy : IServiceProxy
    {
        private readonly MarketSystem _marketSystem;
        private readonly HttpClient _httpClient;
        private const string Url = "https://getbooksrestapi.azurewebsites.net/api/books";

#warning тут у тебя в конструктор инжектятся HttpClient и MarketSystem. Они оба у тебя зареганы в DI, окей
#warning а вот в конструктор MarketSystem инжектится ShopLibrary, которого нигде в DI нет
#warning ты запускал приложение?
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
#warning данный метод вызывается у тебя только в джобе
#warning нужно проверять, надо ли выполнить заказ книг, и в случае необходимости отправлять сообщение во вторую систему
#warning а вот вторая система уже получает книги из external api и отправляет сюда
            var response = await _httpClient.GetAsync($"{Url}/10");
            var books = JsonConvert.DeserializeObject<List<Book>>(response.Content.ToString());
            _marketSystem.BookReception(books);
        }
    }
}