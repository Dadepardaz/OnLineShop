using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Finances.Queries.GetPay
{
    public interface IGetPayService
    {
        ResultDto<GetPayResultDto> Execute(Guid guid);
    }

    public class GetPayService : IGetPayService
    {
        private readonly IDataBaseContext _context;

        public GetPayService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<GetPayResultDto> Execute(Guid guid)
        {
            var pay = _context.Pays.Where(p => p.Guid == guid).FirstOrDefault();
            if (pay != null)
            {
                return new ResultDto<GetPayResultDto>()
                {
                    Data = new GetPayResultDto
                    {
                        Id = pay.Id,
                        Amount = pay.Amount
                    },
                    IsSuccess = true,
                    Message = ""
                };
            }
            else
            {
                throw new Exception("request pay not found");
            }
        }
    }
    public class GetPayResultDto
    {
        public long Id { get; set; }
        public int Amount { get; set; }
    }
}
