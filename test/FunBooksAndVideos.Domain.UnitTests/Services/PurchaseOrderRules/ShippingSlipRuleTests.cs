using System.Collections.Generic;
using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using FunBooksAndVideos.Domain.Services.PurchaseOrderRules;
using Moq;
using Xunit;

namespace FunBooksAndVideos.Domain.UnitTests.Services.PurchaseOrderRules
{
    public class ShippingSlipRuleTests
    {
        private readonly ShippingSlipRule _sut;
        private readonly Mock<IShippingSlipService> _shippingSlipServiceMock;

        public ShippingSlipRuleTests()
        {
            _shippingSlipServiceMock = new Mock<IShippingSlipService>();
            _sut = new ShippingSlipRule(_shippingSlipServiceMock.Object);
        }

        [Fact]
        public async Task ApplyRuleAsync_WhenPurchaseOrderContainsProduct_MustGenerateShippingSlip()
        {
            var purchaseOrder = new PurchaseOrder
            {
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 1,
                        OrderItemType = OrderItemType.Product,
                    }
                }
            };
            
            await _sut.ApplyRuleAsync(purchaseOrder);

            _shippingSlipServiceMock.Verify(x => x.GenerateShippingSlipAsync(purchaseOrder), Times.Once);
        }

        [Fact]
        public async Task ApplyRuleAsync_WhenPurchaseOrderContainsNoProducts_MustNotGenerateShippingSlip()
        {
            var purchaseOrder = new PurchaseOrder
            {
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 2,
                        OrderItemType = OrderItemType.Membership,
                    }
                }
            };

            await _sut.ApplyRuleAsync(purchaseOrder);

            _shippingSlipServiceMock.Verify(x => x.GenerateShippingSlipAsync(It.IsAny<PurchaseOrder>()), Times.Never);
        }
    }
}
