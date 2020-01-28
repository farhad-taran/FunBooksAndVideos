using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services.PurchaseOrderRules
{
    public interface IPurchaseOrderRule
    {
        Task ApplyRuleAsync(PurchaseOrder purchaseOrder);
    }
}