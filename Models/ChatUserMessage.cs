using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrdering.Models
{
    public class ChatUserMessage
    {
        public int Id { get; set; }
        
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}
