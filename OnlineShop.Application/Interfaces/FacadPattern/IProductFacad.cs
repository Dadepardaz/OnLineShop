using OnlineShop.Application.Services.Products.Commands.AddNewCategory;
using OnlineShop.Application.Services.Products.Commands.AddProduct;
using OnlineShop.Application.Services.Products.Commands.DeleteProduct;
using OnlineShop.Application.Services.Products.Commands.EditProduct;
using OnlineShop.Application.Services.Products.Commands.RemoveCategory;
using OnlineShop.Application.Services.Products.Queries.GetCategoriesForProduct;
using OnlineShop.Application.Services.Products.Queries.GetCategory;
using OnlineShop.Application.Services.Products.Queries.GetProductDetailForAdmin;
using OnlineShop.Application.Services.Products.Queries.GetProductForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Interfaces.FacadPattern
{
    public interface IProductFacad
    {
        IAddNewCategory AddNewCategory { get; }
        IGetCategoryService GetCategoryService { get; }
        IRemoveCategoryService RemoveCategoryService { get; }
        IAddProductService AddProductService { get; }
        IGetCategoriesForProductService GetCategoriesForProductService { get; }
        IGetProductForAdminService GetProductForAdminService { get; }
        IDeleteProductService DeleteProductService { get; }
        IGetProductDetailForAdminService GetProductDetailForAdminService { get; }
        IEditProductService EditProductService { get; }
    }
}
