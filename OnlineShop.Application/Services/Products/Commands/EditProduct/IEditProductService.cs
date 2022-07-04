using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProductService
    {
        ResultDto Execute(EditProductDto request);
    }
    public class EditProductService : IEditProductService
    {
        private readonly IDataBaseContext _context;

        public EditProductService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(EditProductDto request)
        {
            var product = _context.Products.Find(request.ProductId);
            if (product ==  null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "not found"
                };
            }
            product.Title = request.Title;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "edit success"
            };
        }
    }
    public class EditProductDto
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
    }
}
