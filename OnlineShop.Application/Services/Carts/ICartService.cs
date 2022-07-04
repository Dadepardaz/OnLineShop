using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Domain.Entities.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Carts
{
    public interface ICartService
    {
        ResultDto AddItemToCart(Guid browserId, long productId);
        ResultDto RemoveItemFromCart(Guid browserId, long cartItemId);
        ResultDto<GetUserCartDto> GetMyCart(Guid browserId, long? userId);

        ResultDto AddCartItem(long cartItemId);
        ResultDto LowOffCartItem(long cartItemId);

    }

    public class CartService : ICartService
    {
        private readonly IDataBaseContext _context;

        public CartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto AddCartItem(long cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            if (cartItem == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }

            cartItem.Count++;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = ""
            };
        }

        public ResultDto AddItemToCart(Guid browserId, long productId)
        {
            var cart = _context.Carts.Where(p => p.BrowserId == browserId && p.Finished == false).SingleOrDefault();
            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    BrowserId = browserId,
                    Finished = false,

                };

                _context.Carts.Add(newCart);
                _context.SaveChanges();
                cart = newCart;
            }

            var product = _context.Products.Find(productId);
            var cartItem = _context.CartItems.Where(p => p.ProductId == productId && p.CartId == cart.Id).SingleOrDefault();
            if (cartItem != null)
            {
                cartItem.Count++;
            }
            else
            {
                CartItem newCartItem = new CartItem()
                {

                    Count = 1,
                    Price = product.Price,
                    Product = product,
                    Cart = cart
                };
                _context.CartItems.Add(newCartItem);
                _context.SaveChanges();
            }

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "Success"
            };
        }

        public ResultDto<GetUserCartDto> GetMyCart(Guid browserId, long? userId)
        {

            try
            {
                var cart = _context.Carts.Include(p => p.CartItems)
                    .ThenInclude(p => p.Product)
                    .ThenInclude(p => p.ProductsImages)
                    .Where(p => p.BrowserId == browserId && p.Finished == false).OrderByDescending(p => p.Id).FirstOrDefault();
                if (cart == null)
                {
                    return new ResultDto<GetUserCartDto>()
                    {
                        IsSuccess = false,
                        Message ="",
                        Data = new GetUserCartDto()
                        {
                            CartItems = new List<GetCarItemtDto>()
                        }
                    };
                }

                if (userId != null)
                {
                    var user = _context.Users.Find(userId);
                    cart.User = user;
                    _context.SaveChanges();
                }

                return new ResultDto<GetUserCartDto>()
                {
                    IsSuccess = true,
                    Message = "",
                    Data = new GetUserCartDto()
                    {
                        CartId = cart.Id,
                        ProductCount = cart.CartItems.Count(),
                        CartItems = cart.CartItems.Select(p =>  new GetCarItemtDto()
                        {
                            Id = p.Id,
                            Count = p.Count,
                            Price = p.Price,
                            ProductName = p.Product.Title,
                            Image = p.Product?.ProductsImages?.FirstOrDefault()?.Src ?? ""
                        }).ToList()
                        
                    }
                };
            }
            catch (Exception ex)
            {

                throw;
            }          

          
        }

        public ResultDto LowOffCartItem(long cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            if (cartItem == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }

            if (cartItem.Count <= 1)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = ""
                };
            }
            cartItem.Count--;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = ""
            };
        }

        public ResultDto RemoveItemFromCart(Guid browserId, long cartItemId)
        {
            var cartItem = _context.CartItems.Where(p => p.Cart.BrowserId == browserId && p.Id == cartItemId).FirstOrDefault();
            if (cartItem == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "not found"
                };
            }
            else
            {
                cartItem.IsRemoved = true;
                cartItem.RemoveTime = DateTime.Now;
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "Remove"
                };
            }

        }
    }

    public class GetUserCartDto
    {
        public long CartId { get; set; }
        public int ProductCount { get; set; }
        public List<GetCarItemtDto> CartItems { get; set; }
        public int TotalCost { 
            get {
                return CartItems.Sum(p => p.Count * p.Price);
            } }
    }

    public class GetCarItemtDto
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
