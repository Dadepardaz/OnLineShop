using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Users.Queries.GetRole
{
    public interface IGetRoleService
    {
        ResultDto<List<GetRolDto>> Execute();
    }

    public class GetRoleService : IGetRoleService
    {
        private readonly IDataBaseContext _context;

        public GetRoleService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<GetRolDto>> Execute()
        {
            var roles = _context.Roles.ToList().Select(p => new GetRolDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return new ResultDto<List<GetRolDto>>()
            {
                Data = roles,
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class GetRolDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
