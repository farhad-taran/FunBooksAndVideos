using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services.PurchaseOrderRules;

namespace FunBooksAndVideos.Domain.Services
{
    public class PurchaseOrderService : IPurchaseOrdersService
    {
        private readonly IEnumerable<IPurchaseOrderRule> _purchaseOrderRules;

        public PurchaseOrderService(IEnumerable<IPurchaseOrderRule> purchaseOrderRules)
        {
            _purchaseOrderRules = purchaseOrderRules;
        }

        public async Task ProcessPurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            var ruleApplicationTasks = _purchaseOrderRules.Select(purchaseOrderRule => purchaseOrderRule.ApplyRuleAsync(purchaseOrder));

            await Task.WhenAll(ruleApplicationTasks);
        }
    }
}
