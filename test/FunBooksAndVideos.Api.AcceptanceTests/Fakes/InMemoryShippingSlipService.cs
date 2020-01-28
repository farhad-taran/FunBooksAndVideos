using System.Collections.Generic;
using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;

namespace FunBooksAndVideos.Api.AcceptanceTests.Fakes
{
    public class InMemoryShippingSlipService : IShippingSlipService
    {
        public List<PurchaseOrder> PurchaseOrders { get; private set; }

        public InMemoryShippingSlipService()
        {
            PurchaseOrders = new List<PurchaseOrder>();
        }

        public Task GenerateShippingSlipAsync(PurchaseOrder purchaseOrder)
        {
            PurchaseOrders.Add(purchaseOrder);

            return Task.CompletedTask;
        }

        public void CleanUp()
        {
            PurchaseOrders = new List<PurchaseOrder>();
        }
    }
}
