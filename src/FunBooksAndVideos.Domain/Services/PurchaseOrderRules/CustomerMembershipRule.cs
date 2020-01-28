using System.Linq;
using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services.PurchaseOrderRules
{
    public class CustomerMembershipRule : IPurchaseOrderRule
    {
        private readonly ICustomerAccountService _customerAccountService;

        public CustomerMembershipRule(ICustomerAccountService customerAccountService)
        {
            _customerAccountService = customerAccountService;
        }

        public async Task ApplyRuleAsync(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder.OrderItems.Any(orderItem => orderItem.OrderItemType == OrderItemType.Membership))
                await _customerAccountService.ActivateMembershipsAsync(purchaseOrder);
        }
    }
}