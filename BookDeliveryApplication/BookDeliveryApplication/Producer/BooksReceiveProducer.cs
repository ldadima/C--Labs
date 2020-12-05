using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContractRabbit;
using MassTransit;
using Microsoft.Extensions.Configuration;

namespace BookDeliveryApplication.Producer
{
    public class BooksReceiveProducer
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IConfiguration _configuration;

        public BooksReceiveProducer(ISendEndpointProvider sendEndpointProvider, IConfiguration configuration)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _configuration = configuration;
        }

        public async Task SentPaymentReceivedEvent(List<IBookResponse.Book> books)
        {
            var message = new BooksResponse();
            
            var hostConfig = new MassTransitConfiguration();
            _configuration.GetSection("MassTransit").Bind(hostConfig);
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(hostConfig.GetQueueAddress("books-delivery-queue"));
            await endpoint.Send(message);
        }
    }
}