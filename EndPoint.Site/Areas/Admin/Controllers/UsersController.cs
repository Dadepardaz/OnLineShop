using EndPoint.Site.Areas.Admin.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Application.Interfaces.FacadPattern;
using OnlineShop.Application.Services.Users.Commands.AddUser;
using OnlineShop.Application.Services.Users.Commands.EditUser;
using OnlineShop.Application.Services.Users.Queries.GetUserForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserFacad _userFacad;

        public UsersController(IUserFacad userFacad)
        {
            _userFacad = userFacad;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var result = _userFacad.GetUserForAdminService.Execute(new RequestGetAllUser
            {
                Page = page,
                PageSize = pageSize
            }).Data;
            return View(result);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            ViewBag.Roles = new SelectList(_userFacad.GetRoleService.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(AddUserViewModel model)
        {
            var result = _userFacad.AddUserService.Execute(new RequestAddUserDto()
            {
                Email = model.Email,
                Password = model.Password,
                RePassword = model.RePassword,
                Name = model.Name,
                Roles = new List<UserRoleDto>
                {
                  new UserRoleDto{ RoleId = model.Roles}
                }
            });
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long userId)
        {
            return Json(_userFacad.RemoveUserService.Execute(userId));
        }

        [HttpPost]
        public IActionResult ChangeStatus(long userId)
        {
            return Json(_userFacad.ChangeStatusUserService.Execute(userId));
        }


        [HttpPost]
        public IActionResult Edit(long userId, string name)
        {
            return Json(_userFacad.EditUserService.Execute(new RequestEditUserDto
            {
                Name = name,
                UserId = userId
            }));
        }
    }
}
