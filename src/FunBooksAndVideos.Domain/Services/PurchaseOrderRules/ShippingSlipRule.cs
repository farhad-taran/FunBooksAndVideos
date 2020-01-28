using System.Linq;
using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services.PurchaseOrderRules
{
    public class ShippingSlipRule : IPurchaseOrderRule
    {
        private readonly IShippingSlipService _shippingSlipService;

        public ShippingSlipRule(IShippingSlipService shippingSlipService)
        {
            _shippingSlipService = shippingSlipService;
        }

        public async Task ApplyRuleAsync(PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder.OrderItems.Any(orderItem => orderItem.OrderItemType == OrderItemType.Product))
                await _shippingSlipService.GenerateShippingSlipAsync(purchaseOrder);
        }
    }
}
