using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Orders.Queries.GetOrderForUser
{
    public interface IGetOrderForUserService
    {
        ResultDto<List<ResultGetOrderDto>> Execute(long userId);
    }
    public class GetOrderForUserService : IGetOrderForUserService
    {
        private readonly IDataBaseContext _context;

        public GetOrderForUserService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ResultGetOrderDto>> Execute(long userId)
        {
            var orders = _context.Orders.Include(p => p.OrderDetails)
                .ThenInclude(p => p.Product)
                .Where(p => p.UserId == userId).OrderByDescending(p => p.Id)
                .Select(p => new ResultGetOrderDto 
                { 
                    Id = p.Id,
                    OrderStatus = p.OrderStatus,
                    PayId = p.PayId,
                    OrderDetails = p.OrderDetails.Select(o => new OrderDetailDto 
                    { 
                        OrderDetailId = o.Id,
                        ProductId = o.ProductId,
                        ProductName = o.Product.Title,
                        Count = o.Count,
                        Price = o.Price
                    }).ToList()
                }).ToList();

            return new ResultDto<List<ResultGetOrderDto>>()
            {
               Data = orders,
               IsSuccess = true,
               Message = ""
            };
        }
    }
    public class ResultGetOrderDto
    {
        public long Id { get; set; }
        public long PayId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    public class OrderDetailDto
    {
        public long OrderDetailId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
