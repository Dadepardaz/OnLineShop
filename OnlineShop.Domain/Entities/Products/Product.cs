using OnlineShop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public int ViewCount { get; set; }
        public bool IsDisplayed { get; set; }
        public string Discription { get; set; }
        public virtual Category Category { get; set; }
        public long CategoryId { get; set; }
        public virtual ICollection<ProductFeatures> ProductFeatures { get; set; }
        public virtual ICollection<ProductsImages> ProductsImages { get; set; }
    }
}
