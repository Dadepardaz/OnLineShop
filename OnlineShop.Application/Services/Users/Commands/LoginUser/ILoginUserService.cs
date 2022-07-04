using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Users.Commands.LoginUser
{
    public interface ILoginUserService
    {
        ResultDto<ResultLoginUserDto> Execute(RequestLoginUserDto request);
    }
    public class LoginUserService : ILoginUserService
    {
        private readonly IDataBaseContext _context;

        public LoginUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultLoginUserDto> Execute(RequestLoginUserDto request)
        {
            if (string.IsNullOrEmpty(request.UserName) || string .IsNullOrEmpty(request.Password))
            {
                return new ResultDto<ResultLoginUserDto>()
                {
                   
                    IsSuccess = false,
                    Message = "Enter your info"
                };
            }

            var user = _context.Users.Include(p => p.UserInRoles).ThenInclude(p => p.Role)
                .Where(p => p.Equals(request.UserName) && p.IsActive).FirstOrDefault();
            if (user == null)
            {
                return new ResultDto<ResultLoginUserDto>()
                {

                    IsSuccess = false,
                    Message = "User Not Found"
                };
            }
            var passwordHasher = new PasswordHasher();
            var verifyPassword = passwordHasher.VerifyPassword(user.Password, request.Password);
            if (verifyPassword)
            {
                List<string> roles = new List<string>();
                foreach (var item in user.UserInRoles)
                {
                    roles.Add(item.Role.Name);
                }
                return new ResultDto<ResultLoginUserDto>()
                {
                    Data = new ResultLoginUserDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Roles = roles
                    },
                    IsSuccess = true,
                    Message = "success"
                };
            }
            else
            {
                return new ResultDto<ResultLoginUserDto>()
                {
                    IsSuccess = false,
                    Message = "warning"
                };
            }
        }
    }

    public class RequestLoginUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ResultLoginUserDto
    {
        public long Id { get; set; }
        public List<string> Roles { get; set; }
        public string Name { get; set; }
    }
}
