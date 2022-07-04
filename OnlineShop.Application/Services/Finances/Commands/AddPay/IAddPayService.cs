using OnlineShop.Application.Interfaces.Contexts;
using OnlineShop.Common.Dto;
using OnlineShop.Domain.Entities.Finances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Services.Finances.Commands.AddPay
{
    public interface IAddPayService
    {
        ResultDto<AddPayResult> Execute(int amount, long userId);
    }

    public class AddPayService : IAddPayService
    {
        private readonly IDataBaseContext _context;

        public AddPayService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<AddPayResult> Execute(int amount, long userId)
        {
            var user = _context.Users.Find(userId);
            Pay pay = new Pay
            {
                IsPay = false,
                Amount = amount,
                Guid = Guid.NewGuid(),
                User = user
            };
            _context.Pays.Add(pay);
            _context.SaveChanges();

            return new ResultDto<AddPayResult>()
            {
                Data = new AddPayResult
                {
                    Guid = pay.Guid,
                    Amount = pay.Amount,
                    Email = user.Email,
                    PayId = pay.Id
                },
                IsSuccess = true,
                Message = ""
            };

        }
    }

    public class AddPayResult
    {
        public Guid Guid { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public long PayId { get; set; }
    }
}
