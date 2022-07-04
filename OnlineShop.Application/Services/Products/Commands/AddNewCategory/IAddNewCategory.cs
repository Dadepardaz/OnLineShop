using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Commands.AddNewCategory
{
    public interface IAddNewCategory
    {
        ResultDto Execute(AddNewCategoryDto request);
    }
    public class AddNewCategory : IAddNewCategory
    {
        private readonly IDataBaseContext _context;

        public AddNewCategory(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(AddNewCategoryDto request)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = " Please enter the title of category "
                };
            }
            Category category = new Category()
            {
                Title = request.Title,
               ParentCategory = GetCategoryparent(request.ParentId)
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message="Register is successed"
            };
        }

        private Category GetCategoryparent(long? parentId)
        {
            return _context.Categories.Find(parentId);
        }
    }

    public class AddNewCategoryDto
    {
        public string Title { get; set; }
        public long? ParentId { get; set; }
     
    }
}
