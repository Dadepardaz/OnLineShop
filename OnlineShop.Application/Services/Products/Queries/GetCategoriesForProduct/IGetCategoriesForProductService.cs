using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Queries.GetCategoriesForProduct
{
    public interface IGetCategoriesForProductService
    {
        ResultDto<List<GetCategoriesForProduct>> Execute();
    }
    public class GetCategoriesForProductService : IGetCategoriesForProductService
    {
        private readonly IDataBaseContext _context;

        public GetCategoriesForProductService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetCategoriesForProduct>> Execute()
        {
            var categories = _context.Categories.Include(p => p.ParentCategory)
                .ToList()
                 .Select(p => new GetCategoriesForProduct
                 {
                     Id = p.Id,
                     Title = p.Title
                 }).ToList();
            if (categories == null)
            {
                return new ResultDto<List<GetCategoriesForProduct>>
                {

                };
            }
            return new ResultDto<List<GetCategoriesForProduct>> ()
            { 
                Data = categories,
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class GetCategoriesForProduct
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
