using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Application.Interfaces.FacadPattern;
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

namespace OnlineShop.Application.Services.Users.FacadPattern
{
    public class UserFacad : IUserFacad
    {
        private readonly IDataBaseContext _context;

        public UserFacad(IDataBaseContext context)
        {
            _context = context;
        }

        private IAddUserService _addUserService;
        public IAddUserService AddUserService
        {
            get
            {
                return _addUserService = _addUserService ?? new AddUserService(_context);
            }
        }

        private IGetRoleService _getRoleService;
        public IGetRoleService GetRoleService 
        {
            get
            {
                return _getRoleService = _getRoleService ?? new GetRoleService(_context);
            }
        }

        private IGetUserForAdminService _getUserForAdminService;
        public IGetUserForAdminService GetUserForAdminService
        { 
            get
            {
                return _getUserForAdminService = _getUserForAdminService ?? new GetUserForAdminService(_context);
            }
        }

        private IRemoveUserService _removeUserService;
        public IRemoveUserService RemoveUserService
        {
            get
            {
                return _removeUserService = _removeUserService ?? new RemoveUserService(_context);
            }
        }

        private IChangeStatusUserService _changeStatusUserService;
        public IChangeStatusUserService ChangeStatusUserService 
        {
            get
            {
                return _changeStatusUserService = _changeStatusUserService ?? new ChangeStatusUserService(_context);
            }
        }

        private IEditUserService _editUserService;
        public IEditUserService EditUserService 
        {
            get
            {
                return _editUserService = _editUserService ?? new EditUserService(_context);
            }
        }

        private ILoginUserService _loginUserService;
        public ILoginUserService LoginUserService 
        {
            get
            {
                return _loginUserService = _loginUserService ?? new LoginUserService(_context);
            }
        }
    }
}
