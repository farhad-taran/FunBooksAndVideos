using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FunBooksAndVideos.Api.Contracts.Requests;
using Newtonsoft.Json;
using Xunit;
using OrderItem = FunBooksAndVideos.Api.Contracts.Common.OrderItem;
using OrderItemType = FunBooksAndVideos.Api.Contracts.Common.OrderItemType;

namespace FunBooksAndVideos.Api.AcceptanceTests
{
    public class PurchaseOrdersControllerTests : IClassFixture<ApiFacade>, IDisposable
    {
        private readonly ApiFacade _apiFacade;

        [Fact]
        public async Task ProcessPurchaseOrder_WhenShippableProductIncluded_MustCreateShippingSlip()
        {
            //Arrange
            var request = new ProcessPurchaseOrderRequest
            {
                Id = 1,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 1,
                        OrderItemType = OrderItemType.Product
                    }
                }
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //Act
            var responseMessage = await _apiFacade.HttpClient.PostAsync("purchase-orders/process", content);

            //Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
           _apiFacade.ShippingSlippedPurchaseOrders.Count.Should().Be(1);
           _apiFacade.ShippingSlippedPurchaseOrders.Single().Should().BeEquivalentTo(request);
           _apiFacade.CustomerAccountPurchaseOrders.Count.Should().Be(0);
        }

        [Fact]
        public async Task ProcessPurchaseOrder_WhenMembershipPurchaseIncluded_MustCreateCustomerAccountMembership()
        {
            //Arrange
            var request = new ProcessPurchaseOrderRequest
            {
                Id = 1,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 1,
                        OrderItemType = OrderItemType.Membership
                    }
                }
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //Act
            var responseMessage = await _apiFacade.HttpClient.PostAsync("purchase-orders/process", content);

            //Assert
            responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
            _apiFacade.CustomerAccountPurchaseOrders.Count.Should().Be(1);
            _apiFacade.CustomerAccountPurchaseOrders.Single().Should().BeEquivalentTo(request);
            _apiFacade.ShippingSlippedPurchaseOrders.Count.Should().Be(0);
        }

        public PurchaseOrdersControllerTests(ApiFacade apiFacade)
        {
            _apiFacade = apiFacade;
        }

        public void Dispose()
        {
            _apiFacade?.CleanUp();
        }
    }
}