using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services
{
    public class ShippingSlipService : IShippingSlipService
    {
        public Task GenerateShippingSlipAsync(PurchaseOrder purchaseOrder)
        {
            return Task.CompletedTask;
        }
    }
}