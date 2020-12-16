using System.Collections.Generic;
using System.Net.Http;
using BookDeliveryApplication.Producer;
using ContractRabbit;
using Newtonsoft.Json;

namespace BookDeliveryApplication.Services
{
    public class ServiceBookProxy : IServiceProxy
    {
        private readonly HttpClient _httpClient;
        private readonly BooksReceiveProducer _booksReceiveProducer;
        private const string Url = "https://getbooksrestapi.azurewebsites.net/api/books";

        public ServiceBookProxy(HttpClient httpClient, BooksReceiveProducer booksReceiveProducer)
        {
            _httpClient = httpClient;
            _booksReceiveProducer = booksReceiveProducer;
        }

        public async void GetAndSaveBooks(int bookCount)
        {
            var response = await _httpClient.GetAsync($"{Url}/{bookCount}");
            var json = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<IBookResponse.Book>>(json);
            await _booksReceiveProducer.SentBooksResponse(books);
        }
    }
}