using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Queries.GetCategory
{
    public interface IGetCategoryService
    {
        ResultDto<List<GetCategoryDto>> Execute(long? parentId);
    }
    public class GetCategoryService : IGetCategoryService
    {
        private readonly IDataBaseContext _context;

        public GetCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetCategoryDto>> Execute(long? parentId)
        {
            var categories = _context.Categories.Include(p => p.ParentCategory)
                .Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == parentId).ToList()
                .Select(p => new GetCategoryDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    ParentCategory = p.ParentCategory!= null ? new ParentCategoryDto
                    {
                        Id = p.ParentCategory.Id,
                        Title = p.ParentCategory.Title

                    } : null,
                    HasChild = p.SubCategories.Count > 0 ? true : false

                }).ToList();

            return new ResultDto<List<GetCategoryDto>>
            {
                Data = categories,
                IsSuccess = true,
                Message = "Succeess"
            };
        }
    }
    public class GetCategoryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public ParentCategoryDto ParentCategory { get; set; }
        public bool HasChild { get; set; }

    }
    public class ParentCategoryDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
    }
}
