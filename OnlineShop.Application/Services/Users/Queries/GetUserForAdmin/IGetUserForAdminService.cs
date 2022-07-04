using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Users.Queries.GetUserForAdmin
{
    public interface IGetUserForAdminService
    {
        ResultDto<ResultGetUser> Execute(RequestGetAllUser request);
    }

    public class GetUserForAdminService : IGetUserForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetUserForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultGetUser> Execute(RequestGetAllUser request)
        {
            int rowCount = 0;
            var users = _context.Users.
                Include(p => p.UserInRoles).ThenInclude(p => p.Role)
                .ToPaged(request.Page, request.PageSize, out rowCount).Select(p => new GetUserDto
                {
                    Email = p.Email,
                    Name = p.Name,
                    IsActive = p.IsActive,
                    Id = p.Id,
                    Roles = p.UserInRoles.Select(p => new GetUserDto.UserInRole
                    {
                        Name = p.Role.Name
                    }).ToList()
                }).ToList();

            return new ResultDto<ResultGetUser>
            {
                Data = new ResultGetUser
                {
                    Page = request.Page,
                    PageSize = request.PageSize,
                    RowCount = rowCount,
                    Users = users

                },
                IsSuccess = true,
                Message = ""
            };
        }


    }

    public class RequestGetAllUser
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public class ResultGetUser
    {
        public int RowCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<GetUserDto> Users { get; set; }
    }
    public class GetUserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public List<UserInRole> Roles { get; set; }

        public class UserInRole
        {
            public string Name { get; set; }
        }
    }
}
