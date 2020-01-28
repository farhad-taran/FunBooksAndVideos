using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services
{
    public interface IPurchaseOrdersService
    {
        Task ProcessPurchaseOrderAsync(PurchaseOrder purchaseOrder);
    }
}
