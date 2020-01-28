using System.Collections.Generic;
using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using FunBooksAndVideos.Domain.Services.PurchaseOrderRules;
using Moq;
using Xunit;

namespace FunBooksAndVideos.Domain.UnitTests.Services
{
    public class PurchaseOrderServiceTests
    {
        private PurchaseOrderService _sut;
        private Mock<IPurchaseOrderRule> _customerMembershipRuleMock;
        private Mock<IPurchaseOrderRule> _shippingSlipRuleMock;

        public PurchaseOrderServiceTests()
        {
            _customerMembershipRuleMock = new Mock<IPurchaseOrderRule>();
            _shippingSlipRuleMock = new Mock<IPurchaseOrderRule>();
            _sut = new PurchaseOrderService(new[] { _customerMembershipRuleMock.Object, _shippingSlipRuleMock.Object });
        }

        [Fact]
        public async Task ProcessPurchaseOrderAsync_AppliesAllRules()
        {
            var purchaseOrder = new PurchaseOrder
            {
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 2,
                        OrderItemType = OrderItemType.Membership,
                    },
                    new OrderItem
                    {
                        Id = 3,
                        OrderItemType = OrderItemType.Product,
                    }
                }
            };

            await _sut.ProcessPurchaseOrderAsync(purchaseOrder);

            _customerMembershipRuleMock.Verify(x => x.ApplyRuleAsync(purchaseOrder), Times.Once);
            _shippingSlipRuleMock.Verify(x => x.ApplyRuleAsync(purchaseOrder), Times.Once);
        }
    }
}
