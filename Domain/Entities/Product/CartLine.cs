using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_Zone.Domain.Entities.Product
{
    public class CartLine
    {
        [Key]
        public int CartLineId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public string UserId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual Product Product { get; set; }
    }
}
