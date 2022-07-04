using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common;
using OnlineShop.Common.Dto;
using OnlineShop.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Orders.Queries.GetOrderForAdmin
{
    public interface IGetOrderForAdminService
    {
        ResultDto<PagedResultGetOrderForAdminDto> Execute(OrderStatus orderStatus, int page = 1, int pageSize = 20);
    }

    public class GetOrderForAdminService : IGetOrderForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetOrderForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<PagedResultGetOrderForAdminDto> Execute(OrderStatus orderStatus, int page = 1, int pageSize = 20)
        {
            int rowCount = 0;
            var orders = _context.Orders.Include(p => p.OrderDetails)
                .Where(p => p.OrderStatus == orderStatus)
                .OrderByDescending(p => p.Id)
                .ToPaged(page, pageSize, out rowCount)
                .Select(p => new GetOrderForAdminDto
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    OrderStatus = p.OrderStatus,
                    PayId = p.PayId,
                    ProductCount = p.OrderDetails.Count(),
                    InsertTime = p.InsertTime,

                }).ToList();
            return new ResultDto<PagedResultGetOrderForAdminDto>()
            {
                Data = new PagedResultGetOrderForAdminDto
                {
                    Page = page,
                    PageSize = pageSize,
                    RowCount = rowCount,
                    Orders = orders
                },
                IsSuccess = true,
                Message = ""
            };

        }
    }
    public class PagedResultGetOrderForAdminDto
    {
        public int RowCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<GetOrderForAdminDto> Orders { get; set; }
    }
    public class GetOrderForAdminDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PayId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int ProductCount { get; set; }
        public DateTime InsertTime { get; set; }

    }
}
