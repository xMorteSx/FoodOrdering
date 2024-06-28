namespace FoodOrdering.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
