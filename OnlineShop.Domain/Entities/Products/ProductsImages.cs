using OnlineShop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities.Products
{
    public class ProductsImages : BaseEntity
    {
        public string Src { get; set; }
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
    }
}
