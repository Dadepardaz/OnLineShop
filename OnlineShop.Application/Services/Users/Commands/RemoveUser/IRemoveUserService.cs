using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Users.Commands.RemoveUser
{
    public interface IRemoveUserService
    {
        ResultDto Execute(long userId);
    }

    public class RemoveUserService : IRemoveUserService
    {
        private readonly IDataBaseContext _context;

        public RemoveUserService(IDataBaseContext context)
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

            user.IsRemoved = true;
            user.RemoveTime = DateTime.Now;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "User is removed"
            };
        }
    }
}
