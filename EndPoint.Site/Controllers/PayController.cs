using EndPoint.Site.Utilites;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services.Carts;
using OnlineShop.Application.Services.Finances.Commands.AddPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using ZarinPal.Class;
using Dto.Payment;
using OnlineShop.Application.Services.Finances.Queries.GetPay;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Application.Services.Orders.Commands.AddNewOrder;

namespace EndPoint.Site.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IAddPayService _addPayService;
        private readonly ICookiesManager _cookiesManager;
        private readonly IGetPayService _getPayService;
        private readonly IAddNewOrderService _addNewOrderService;
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;

        public PayController(ICartService cartService,
                                  IAddPayService addPayService,
                                  ICookiesManager cookiesManager,
                                  IGetPayService getPayService,
                                  IAddNewOrderService addNewOrderService)
        {
            _cartService = cartService;
            _addPayService = addPayService;
            _cookiesManager = cookiesManager;
            _getPayService = getPayService;
            _addNewOrderService = addNewOrderService;
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();

        }

        public async Task<IActionResult> Index()
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), UserId);

            if (cart.Data.TotalCost > 0)
            {
                var pay = _addPayService.Execute(cart.Data.TotalCost, UserId.Value);
                // ارسال در گاه پرداخت
                var result = await _payment.Request(new DtoRequest()
                {
                    Mobile = "09121112222",
                    CallbackUrl = $"https://localhost:44350/Pay/Verify?guid={pay.Data.Guid}",
                    Description = "پرداخت فاکتور شماره:" + pay.Data.PayId,
                    Email = pay.Data.Email,
                    Amount = pay.Data.Amount,
                    MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
                }, ZarinPal.Class.Payment.Mode.sandbox);
                return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");
            }
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

       
        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {
            var pay = _getPayService.Execute(guid);
            var verification = await _payment.Verification(new DtoVerification
            {
                Amount = pay.Data.Amount,
                MerchantId = "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX",
                Authority = authority
            }, Payment.Mode.sandbox);

            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManager.GetBrowserId(HttpContext), UserId);
            if (verification.Status == 100)
            {
                _addNewOrderService.Execute(new AddOrderRequestDto
                {
                    CartId = cart.Data.CartId,
                    UserId = UserId.Value,
                    PayId = pay.Data.Id
                });

                //redirect to orders
                return RedirectToAction("Index", "Orders");
            }
            else
            {
              
            }

            return View();
        }
    }
}
