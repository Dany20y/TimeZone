using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Time_Zone.Domain.Entities.Product
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        // Listă pentru a ține evidența liniilor de produse în coș
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            var line = lineCollection
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity, Price = product.Price, UserId = UserId });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveItem(int productId)
        {
            lineCollection.RemoveAll(l => l.Product.Id == productId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        // Colectie virtuala pentru Entity Framework
        public virtual ICollection<CartLine> Lines
        {
            get => lineCollection;
            set => lineCollection = value?.ToList() ?? new List<CartLine>();
        }
    }
}
