using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.HomePage.MenuItem
{
    public interface IGetMenuItem
    {
        ResultDto<List<MenuItemDto>> Execute();
    }

    public class GetMenuItem : IGetMenuItem
    {
        private readonly IDataBaseContext _context;

        public GetMenuItem(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<MenuItemDto>> Execute()
        {
            var categories = _context.Categories.Include(p => p.SubCategories)
                .Where(p => p.ParentCategoryId == null).ToList().Select(p => new MenuItemDto
                { 
                    Id = p.Id,
                    Title = p.Title,
                    Child = p.SubCategories.ToList().Select(c => new MenuItemDto
                    {
                        Id = c.Id,
                        Title = c.Title,
                        
                    }).ToList()
                }).ToList();

            return new ResultDto<List<MenuItemDto>>()
            {
                Data = categories,
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class MenuItemDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<MenuItemDto> Child { get; set; }
    }
}
