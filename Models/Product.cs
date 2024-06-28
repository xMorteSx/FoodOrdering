using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrdering.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public string UserName { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Kwota jest wymagana")]
        [Range(0, double.MaxValue, ErrorMessage = "Kwota nie może być mniejsza niż 0")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        virtual public ICollection<Comment> Comments { get; set; }
    }
}
