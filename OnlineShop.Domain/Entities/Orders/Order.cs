using OnlineShop.Domain.Entities.Common;
using OnlineShop.Domain.Entities.Finances;
using OnlineShop.Domain.Entities.Products;
using OnlineShop.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public long UserId { get; set; }
        public virtual User User { get; set; }
        public long PayId { get; set; }
        public virtual Pay Pay { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string Address { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public class OrderDetail : BaseEntity
    {
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }

    public enum OrderStatus
    {
       
        Processing = 0,
        Canceled = 1,
        Delivered = 2
    }
}
