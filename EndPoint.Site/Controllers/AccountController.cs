using EndPoint.Site.Models.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.Users.Commands.LoginUser;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndPoint.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserFacad _userFacad;

        public AccountController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json (new ResultDto() { IsSuccess = false, Message="You are registered"});
            }
            var result = _userFacad.AddUserService.Execute(new OnlineShop.Application.Services.Users.Commands.AddUser.RequestAddUserDto 
            { 
                Email = model.Email,
                Name = model.Name,
                Password = model.Password,
                RePassword = model.RePassword,
                Roles = new List<OnlineShop.Application.Services.Users.Commands.AddUser.UserRoleDto>()
                {
                    new OnlineShop.Application.Services.Users.Commands.AddUser.UserRoleDto
                    {
                        RoleId = 3
                    }
                }
            });

            if (result.IsSuccess == true)
            {
                List<Claim> claims = new List<Claim>() 
                { 
                    new Claim(ClaimTypes.NameIdentifier, result.Data.UserId.ToString()),
                    new Claim(ClaimTypes.Name, model.Name),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Role,"Customer")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                HttpContext.SignInAsync(principal, properties);
                
            }
            return Json(result);
        }

        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginUserViewModel
            { 
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel model)
        {
            var result = _userFacad.LoginUserService.Execute(new RequestLoginUserDto
            {
                UserName = model.UserName,
                Password = model.Password
            });
            if (result.IsSuccess == true)
            {
                var clamis = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()),
                    new Claim(ClaimTypes.Name, result.Data.Name),
                    new Claim(ClaimTypes.Email, model.UserName)
                };

                foreach (var item in result.Data.Roles)
                {
                    clamis.Add(new Claim(ClaimTypes.Role,item));
                }

                var identity = new ClaimsIdentity(clamis, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(5)
                };
                HttpContext.SignInAsync(principal, properties);

            }
            return Json(result);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
    }
}
