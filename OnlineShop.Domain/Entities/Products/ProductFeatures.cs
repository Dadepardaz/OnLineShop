using OnlineShop.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities.Products
{
    public class ProductFeatures : BaseEntity
    {
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
    }
}
