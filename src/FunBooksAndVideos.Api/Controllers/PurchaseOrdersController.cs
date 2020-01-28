using System.Linq;
using System.Threading.Tasks;
using FunBooksAndVideos.Api.Contracts;
using FunBooksAndVideos.Api.Contracts.Requests;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace FunBooksAndVideos.Api.Controllers
{
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly IPurchaseOrdersService _purchaseOrdersService;

        public PurchaseOrdersController(IPurchaseOrdersService purchaseOrdersService)
        {
            _purchaseOrdersService = purchaseOrdersService;
        }
        
        [HttpPost(ApiRoutes.ProcessPurchaseOrder)]
        public async Task<IActionResult> ProcessPurchaseOrder([FromBody] ProcessPurchaseOrderRequest request)
        {
            //we need to maintain a separation between the domain model and the api contract
            //this way we can avoid introducing breaking changes to the consumers of the api
            //and can enhance our domain model over time to be richer, 
            //also the api contract will probably have dependencies on third party or technology specific
            //libraries as we start to add validation attributes or other metadata and this third party
            //dependencies should not be introduced into the domain model.
            //this is why we are mapping from a api contract to a domain model here

            var purchaseOrder = new PurchaseOrder
            {
                Id = request.Id,
                CustomerId = request.CustomerId,
                Total = request.Total,
                OrderItems = request.OrderItems.Select(orderItem => new OrderItem
                {
                    Id = orderItem.Id,
                    Name = orderItem.Name,
                    OrderItemType = (OrderItemType)orderItem.OrderItemType
                }).ToList()
            };

            await _purchaseOrdersService.ProcessPurchaseOrderAsync(purchaseOrder);

            return Ok();
        }
    }
}