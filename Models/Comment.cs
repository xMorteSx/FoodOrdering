using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrdering.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [ForeignKey("OrderProduct")]
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }

        virtual public Product OrderProduct { get; set; }
    }
}
