using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services
{
    public interface IShippingSlipService
    {
        Task GenerateShippingSlipAsync(PurchaseOrder purchaseOrder);
    }
}