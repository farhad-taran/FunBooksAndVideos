using System.Threading.Tasks;
using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services
{
    public class CustomerAccountService : ICustomerAccountService
    {
        public Task ActivateMembershipsAsync(PurchaseOrder purchaseOrder)
        {
            return Task.CompletedTask;
        }
    }
}