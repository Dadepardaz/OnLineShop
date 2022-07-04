using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Finances.Queries.GetPayForAdmin
{
    public interface IGetPayForAdmin
    {
        ResultDto<PagingResultGetPayDto> Execute(int page = 1, int pageSize = 20);
    }

    public class GetPayForAdmin : IGetPayForAdmin
    {
        private readonly IDataBaseContext _context;

        public GetPayForAdmin(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<PagingResultGetPayDto> Execute(int page = 1, int pageSize = 20)
        {
            int rowCount = 0;
            var pays = _context.Pays.Include(p => p.User)
                .ToPaged(page, pageSize, out rowCount)
                 .Select(p => new GetPayDto 
                 {
                    Id = p.Id,
                    Guid = p.Guid,
                    UserId = p.UserId,
                    UserName = p.User.Name,
                    Amount = p.Amount,
                    IsPay = p.IsPay,
                    PayDate = p.PayDate,
                    Authority = p.Authority,
                    RefId = p.RefId
                 }).ToList();

            return new ResultDto<PagingResultGetPayDto>
            {
                Data = new PagingResultGetPayDto
                {
                    RowCount = rowCount,
                    Page = page,
                    PageSize = pageSize,
                    Pays = pays
                },
                IsSuccess = true,
                Message =""
            };
        }
    }

    public class PagingResultGetPayDto
    {
        public int RowCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<GetPayDto> Pays { get; set; }
    }
    public class GetPayDto
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;

    }
}
