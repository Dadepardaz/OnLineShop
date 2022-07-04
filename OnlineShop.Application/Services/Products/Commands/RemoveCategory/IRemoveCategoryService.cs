using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Commands.RemoveCategory
{
    public interface IRemoveCategoryService
    {
        ResultDto Execute(long? categoryId);
    }

    public class RemoveCategoryService : IRemoveCategoryService
    {
        private readonly IDataBaseContext _context;

        public RemoveCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long? categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if (category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "Not Found"
                };
            }
            category.IsRemoved = true;
            category.RemoveTime = DateTime.Now;
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "Remove"
            };
        }
    }
}
