using System;

namespace FoodOrdering.Models
{
    public class Order
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
        public DateTime Date { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ChatUserMessage> ChatMessages { get; set; }
    }
}
