using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Products.Queries.GetProductDetailForAdmin
{
    public interface IGetProductDetailForAdminService
    {
        ResultDto<DetailProductForAdminDto> Execute(long productId);
    }

    public class GetProductDetailForAdminService : IGetProductDetailForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetProductDetailForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<DetailProductForAdminDto> Execute(long productId)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductsImages)
                .Where(p => p.Id == productId).FirstOrDefault();

            if (product == null)
            {
                return new ResultDto<DetailProductForAdminDto>()
                {
                    IsSuccess = false,
                    Message = "not found"
                };
            }

            return new ResultDto<DetailProductForAdminDto>()
            {
                Data = new DetailProductForAdminDto
                {
                    Title = product.Title,
                    Brand = product.Brand,
                    Price = product.Price,
                    Inventory = product.Inventory,
                    IsDisplayed = product.IsDisplayed,
                    Discription = product.Discription,
                    Category = product.Category.Title,
                    Images = product.ProductsImages.Select(p => new ImageProductForAdminDto { 
                        Id = p.Id,
                        Src = p.Src
                    }).ToList(),
                    FeatureProduct = product.ProductFeatures.Select(p => new FeatureProductForAdminDto { 
                        Id = p.Id,
                        DisplayName = p.DisplayName,
                        Value = p.Value
                    }).ToList()
                },
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class DetailProductForAdminDto
    {
        public string Title { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool IsDisplayed { get; set; }
        public string Discription { get; set; }
        public string Category { get; set; }
        public List<ImageProductForAdminDto> Images { get; set; }
        public List<FeatureProductForAdminDto> FeatureProduct { get; set; }

    }

    public class ImageProductForAdminDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
    }
    public class FeatureProductForAdminDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
