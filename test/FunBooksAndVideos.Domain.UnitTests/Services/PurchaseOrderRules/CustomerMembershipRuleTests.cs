using System.Collections.Generic;
using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using FunBooksAndVideos.Domain.Services.PurchaseOrderRules;
using Moq;
using Xunit;

namespace FunBooksAndVideos.Domain.UnitTests.Services.PurchaseOrderRules
{
    public class CustomerMembershipRuleTests
    {
        private readonly CustomerMembershipRule _sut;
        private readonly Mock<ICustomerAccountService> _customerAccountServiceMock;

        public CustomerMembershipRuleTests()
        {
            _customerAccountServiceMock = new Mock<ICustomerAccountService>();
            _sut = new CustomerMembershipRule(_customerAccountServiceMock.Object);
        }

        [Fact]
        public async Task ApplyRuleAsync_WhenPurchaseOrderContainsMembership_MustActivateMembership()
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

            _customerAccountServiceMock.Verify(x => x.ActivateMembershipsAsync(purchaseOrder), Times.Once);
        }

        [Fact]
        public async Task ApplyRuleAsync_WhenPurchaseOrderContainsNoMemberships_MustNotActivateMembership()
        {
            var purchaseOrder = new PurchaseOrder
            {
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 2,
                        OrderItemType = OrderItemType.Product,
                    }
                }
            };

            await _sut.ApplyRuleAsync(purchaseOrder);

            _customerAccountServiceMock.Verify(x => x.ActivateMembershipsAsync(It.IsAny<PurchaseOrder>()), Times.Never);
        }
    }
}