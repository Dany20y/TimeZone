using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Zone.Domain.Entities.Product
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descript { get; set; } 
        public int Price { get; set; }
        public string Duration { get; set; }
        public string Start { get; set; }
        public List<Outline> OutlineObj { get; set; }
    }
    public class Outline
    {
        public string Topic { get; set; }
        public string Time { get; set; }
        public bool Status { get; set; }
    }
}
