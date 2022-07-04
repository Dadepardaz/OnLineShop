using OnlineShop.Application.Services.Users.Commands.AddUser;
using OnlineShop.Application.Services.Users.Commands.ChangeStatusUser;
using OnlineShop.Application.Services.Users.Commands.EditUser;
using OnlineShop.Application.Services.Users.Commands.LoginUser;
using OnlineShop.Application.Services.Users.Commands.RemoveUser;
using OnlineShop.Application.Services.Users.Queries.GetRole;
using OnlineShop.Application.Services.Users.Queries.GetUserForAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Interfaces.FacadPattern
{
    public interface IUserFacad
    {
        IAddUserService AddUserService  { get; }
        IGetRoleService GetRoleService { get; }
        IGetUserForAdminService GetUserForAdminService { get; }
        IRemoveUserService RemoveUserService { get; }
        IChangeStatusUserService ChangeStatusUserService { get; }
        IEditUserService EditUserService { get; }
        ILoginUserService LoginUserService { get; }
    }
}
