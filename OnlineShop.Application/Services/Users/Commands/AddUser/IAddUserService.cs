using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common;
using OnlineShop.Common.Dto;
using OnlineShop.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Users.Commands.AddUser
{
    public interface IAddUserService
    {
        ResultDto<ResultAddUserDto> Execute(RequestAddUserDto request);
    }

    public class AddUserService : IAddUserService
    {
        private readonly IDataBaseContext _context;

        public AddUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultAddUserDto> Execute(RequestAddUserDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return new ResultDto<ResultAddUserDto>
                {
                    Data = new ResultAddUserDto
                    {
                        UserId = 0
                    },
                    IsSuccess = false,
                    Message ="Enter a name "
                };
            }

            if (string.IsNullOrEmpty(request.Email))
            {
                return new ResultDto<ResultAddUserDto>
                {
                    Data = new ResultAddUserDto
                    {
                        UserId = 0
                    },
                    IsSuccess = false,
                    Message = "Enter a email "
                };
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                return new ResultDto<ResultAddUserDto>
                {
                    Data = new ResultAddUserDto
                    {
                        UserId = 0
                    },
                    IsSuccess = false,
                    Message = "Enter a password "
                };
            }

            var match = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
            var matchEmail = Regex.Match(request.Email, match, RegexOptions.IgnoreCase);
            if (!matchEmail.Success)
            {
                return new ResultDto<ResultAddUserDto>
                {
                    Data = new ResultAddUserDto
                    {
                        UserId = 0
                    },
                    IsSuccess = false,
                    Message = "Email "
                };
            }

            if (request.Password != request.RePassword)
            {
                return new ResultDto<ResultAddUserDto>
                {
                    Data = new ResultAddUserDto
                    {
                        UserId = 0
                    },
                    IsSuccess = false,
                    Message = "Password And repasword don't match "
                };
            }

            var passwordHashed = new PasswordHasher();
            
            User user = new User()
            {
                Email = request.Email,
                Name = request.Name,
                IsActive = true,
                Password = passwordHashed.HashPassword(request.Password)
            };

            List<UserInRole> userInRoles = new List<UserInRole>();
            foreach (var item in request.Roles)
            {
                var role = _context.Roles.Find(item.RoleId);
                if (role != null)
                {
                    userInRoles.Add(new UserInRole
                    {
                        User = user,
                        Role = role,
                        RoleId = role.Id,
                        UserId = user.Id
                    });
                }
            }
            user.UserInRoles = userInRoles;
            _context.Users.Add(user);
            _context.SaveChanges();
            return new ResultDto<ResultAddUserDto>
            {
                Data = new ResultAddUserDto
                {
                    UserId = user.Id
                },
                IsSuccess = true,
                Message = "Success"
            };
        }
    }
    public class RequestAddUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public List<UserRoleDto> Roles { get; set; }
    }

    public class UserRoleDto
    {
        public long RoleId { get; set; }
    }

    public class ResultAddUserDto
    {
        public long UserId { get; set; }

    }
}
