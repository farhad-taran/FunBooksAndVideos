namespace FunBooksAndVideos.Api.Contracts.Common
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OrderItemType OrderItemType { get; set; }
    }
}