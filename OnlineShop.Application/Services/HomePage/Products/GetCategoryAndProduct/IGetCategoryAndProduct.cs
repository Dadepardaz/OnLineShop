using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.HomePage.Products.GetCategoryAndProduct
{
    public interface IGetCategoryAndProduct
    {
        ResultDto<List<CategoryAndCountDto>> Execute();
    }

    public class GetCategoryAndProduct : IGetCategoryAndProduct
    {
        private readonly IDataBaseContext _context;

        public GetCategoryAndProduct(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CategoryAndCountDto>> Execute()
        {
        
            var categories = _context.Products.Include(p => p.Category)
                .Include(p => p.ProductsImages).ToList()
                                              .OrderByDescending(p => p.Id).Take(6)
                                              .Select(p => new CategoryAndCountDto
                                              { 
                                                ProductId = p.Id,
                                                Category = p.Category.Title,
                                                CategoryCount = GetCountCategory(p.CategoryId),
                                                Image = p.ProductsImages.FirstOrDefault().Src
                                              }).ToList();

            return new ResultDto<List<CategoryAndCountDto>>()
            {
                Data= categories,
                IsSuccess = true,
                Message = ""
            };
        }

        private int GetCountCategory(long categoryId)
        {
            var categoriesCount = _context.Products.Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId).GroupBy(p => p.CategoryId).Select(p => new { 
                
                    Categorycount = p.Count()
                }).ToList();
            return categoriesCount.First().Categorycount;
        }
    }
    
    public class CategoryAndCountDto
    {
        public long ProductId { get; set; }
        public string Category { get; set; }
        public int CategoryCount { get; set; }
        public string Image { get; set; }
    }
}
