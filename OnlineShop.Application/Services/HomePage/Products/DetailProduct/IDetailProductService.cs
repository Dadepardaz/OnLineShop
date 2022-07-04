using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.HomePage.Products.DetailProduct
{
    public interface IDetailProductService
    {
        ResultDto<DetailProductDto> Execute(long productId);
    }

    public class DetailProductService : IDetailProductService
    {
        private readonly IDataBaseContext _context;

        public DetailProductService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<DetailProductDto> Execute(long productId)
        {
            var product = _context.Products.Include(p => p.ProductsImages)
                 .Include(p => p.ProductFeatures).Where(p => p.Id == productId)
                 .Select(p => new DetailProductDto { 
                    Id = p.Id,
                    Title = p.Title,
                    Price = p.Price,
                    Discription = p.Discription,
                    Images = p.ProductsImages.ToList().Select(p => new ImageProductsDto
                    {
                        Id = p.Id,
                        Src = p.Src
                    }).ToList(),
                    Features = p.ProductFeatures.ToList().Select(p => new FeatureProductsDto
                    {
                        Id = p.Id,
                        DisplayName = p.DisplayName,
                        Value = p.Value
                    }).ToList()

                 }).SingleOrDefault();

            if (product == null)
            {
                return new ResultDto<DetailProductDto>()
                {
                    
                    IsSuccess = false,
                    Message = "not found"
                };
            }

            return new ResultDto<DetailProductDto>()
            {
                Data = product,
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class DetailProductDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Discription { get; set; }
        public List<ImageProductsDto> Images { get; set; }
        public List<FeatureProductsDto> Features { get; set; }
    }
    public class ImageProductsDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
    }

    public class FeatureProductsDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }

}
