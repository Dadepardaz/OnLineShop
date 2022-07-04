using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.HomePage.Products.GetProduct
{
    public interface IGetProductForIndex
    {
        ResultDto<List<GetProductDto>> Execute();
    }

    public class GetProductForIndex : IGetProductForIndex
    {
        private readonly IDataBaseContext _context;

        public GetProductForIndex(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetProductDto>> Execute()
        {
            var products = _context.Products.Include(p => p.ProductsImages)
                .ToList().OrderByDescending(p => p.Id).Take(8).Select(p => new GetProductDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Image = p.ProductsImages.FirstOrDefault().Src

                }).ToList();

            return new ResultDto<List<GetProductDto>>()
            {
                Data = products,
                IsSuccess = true,
                Message = ""
            };
        }
    }

    public class GetProductDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }

}
