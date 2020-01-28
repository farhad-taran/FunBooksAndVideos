using System.Collections.Generic;

namespace FunBooksAndVideos.Domain.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public int CustomerId { get; set; }

        public IList<OrderItem> OrderItems { get; set; }
    }
}
