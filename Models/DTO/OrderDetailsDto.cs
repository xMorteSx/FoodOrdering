namespace FoodOrdering.Models.DTO
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string RestaurantName { get; set; }
        public decimal MinPrice { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal FreeDeliveryPrice { get; set; }
    }
}
