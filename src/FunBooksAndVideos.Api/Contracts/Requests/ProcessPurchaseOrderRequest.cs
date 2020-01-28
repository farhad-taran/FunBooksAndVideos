using System.Collections.Generic;
using FunBooksAndVideos.Api.Contracts.Common;

namespace FunBooksAndVideos.Api.Contracts.Requests
{
    public class ProcessPurchaseOrderRequest
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public int CustomerId { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
