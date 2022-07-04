using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Commands.DeleteProduct
{
    public interface IDeleteProductService
    {
        ResultDto Execute(long productId);
    }

    public class DeleteProductService : IDeleteProductService
    {
        private readonly IDataBaseContext _context;

        public DeleteProductService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "Not Found"
                };
            }

            product.IsRemoved = true;
            product.RemoveTime = DateTime.Now;
            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "Product was deleted"
            };
        }
    }
}
