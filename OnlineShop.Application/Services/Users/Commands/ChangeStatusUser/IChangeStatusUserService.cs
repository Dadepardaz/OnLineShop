using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Users.Commands.ChangeStatusUser
{
    public interface IChangeStatusUserService
    {
        ResultDto Execute(long userId);
    }

    public class ChangeStatusUserService : IChangeStatusUserService
    {
        private readonly IDataBaseContext _context;

        public ChangeStatusUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            var userStatus = user.IsActive == true ? "Active" : "Not Active";
            user.IsActive = !user.IsActive;
            
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = $"User is { userStatus}"
            };
        }
    }
}
