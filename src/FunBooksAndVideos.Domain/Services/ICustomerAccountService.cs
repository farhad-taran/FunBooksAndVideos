using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services
{
    public interface ICustomerAccountService
    {
        Task ActivateMembershipsAsync(PurchaseOrder purchaseOrder);
    }
}