using System.Collections.Generic;
using System.Net.Http;
using FunBooksAndVideos.Api.AcceptanceTests.Fakes;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace FunBooksAndVideos.Api.AcceptanceTests
{
    public class ApiFacade : WebApplicationFactory<Startup>
    {
        public HttpClient HttpClient { get; }
        public InMemoryShippingSlipService InMemoryShippingSlipService { get; }
        public InMemoryCustomerAccountService InMemoryCustomerAccountService { get; }

        public List<PurchaseOrder> ShippingSlippedPurchaseOrders =>
            InMemoryShippingSlipService.PurchaseOrders;

        public List<PurchaseOrder> CustomerAccountPurchaseOrders =>
            InMemoryCustomerAccountService.PurchaseOrders;

        public ApiFacade()
        {
            InMemoryShippingSlipService = new InMemoryShippingSlipService();
            InMemoryCustomerAccountService = new InMemoryCustomerAccountService();

            var appFactory = WithWebHostBuilder(config =>
            {
                config
                    .ConfigureTestServices(services =>
                    {
                        services.AddTransient<IShippingSlipService>(s => InMemoryShippingSlipService);
                        services.AddTransient<ICustomerAccountService>(s => InMemoryCustomerAccountService);
                    });
            });

            HttpClient = appFactory.CreateClient();
        }

        public void CleanUp()
        {
            InMemoryShippingSlipService.CleanUp();
            InMemoryCustomerAccountService.CleanUp();
        }
    }
}
