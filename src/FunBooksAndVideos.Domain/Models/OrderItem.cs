namespace FunBooksAndVideos.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OrderItemType OrderItemType { get; set; }
    }
}