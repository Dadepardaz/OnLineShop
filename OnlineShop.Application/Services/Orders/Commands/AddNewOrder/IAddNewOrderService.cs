using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Orders.Commands.AddNewOrder
{
    public interface IAddNewOrderService
    {
        ResultDto Execute(AddOrderRequestDto request);
    }

    public class AddNewOrderService : IAddNewOrderService
    {
        private readonly IDataBaseContext _context;

        public AddNewOrderService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(AddOrderRequestDto request)
        {
            var user = _context.Users.Find(request.UserId);
            var pay = _context.Pays.Find(request.PayId);
            var cart = _context.Carts.Include(p => p.CartItems)
                .ThenInclude(p => p.Product)
                .Where(p => p.Id == request.CartId).FirstOrDefault();
            pay.IsPay = true;
            pay.PayDate = DateTime.Now;
            pay.Authority = request.Authority;
            pay.RefId = request.RefId;
            cart.Finished = true;

            Order order = new Order()
            {
                Address = "",
                OrderStatus = OrderStatus.Processing,
                Pay = pay,
                User = user
            };
            _context.Orders.Add(order);
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cart.CartItems)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Order = order,
                    Count = item.Count,
                    Price = item.Product.Price,
                    Product = item.Product
                };
                orderDetails.Add(orderDetail);
            }

            _context.OrderDetails.AddRange(orderDetails);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "",
            };

        }
    }
    public class AddOrderRequestDto
    {
        public long UserId { get; set; }
        public long PayId { get; set; }
        public long CartId { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
    }

  }
