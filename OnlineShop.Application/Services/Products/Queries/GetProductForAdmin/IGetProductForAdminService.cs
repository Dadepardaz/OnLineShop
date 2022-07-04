using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {
        ResultDto<PaginationGetProduc> Execute(int page = 1, int pageSize = 20);
    }

    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetProductForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<PaginationGetProduc> Execute(int page = 1, int pageSize = 20)
        {
            int rowCount = 0;
            var products = _context.Products.Include(p => p.Category)
                .ToPaged(page, pageSize, out rowCount)
                .Select(p => new GetProductForAdminDto
                { 
                    Title= p.Title,
                    Brand = p.Brand,
                    Category = p.Category.Title,
                    Discription = p.Discription,
                    Inventory = p.Inventory,
                    Price = p.Price,
                    IsDisplayed = p.IsDisplayed,
                    Id = p.Id

                }).ToList();

            return new ResultDto<PaginationGetProduc>()
            {
                Data = new PaginationGetProduc
                { 
                
                    RowCount = rowCount,
                    Page = page,
                    PageSize = pageSize,
                    Products = products
                },
                IsSuccess = true,
                Message = ""

            };
        }
    }

    public class PaginationGetProduc
    {
        public int RowCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<GetProductForAdminDto> Products { get; set; }
    }
    public class GetProductForAdminDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool IsDisplayed { get; set; }
        public string Discription { get; set; }
        public string Category { get; set; }
    }
}
