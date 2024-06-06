using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IPaymentService
    {
        public Task<bool> WebPayment(Guid cartId, Decimal amount);
        public Task<bool> USSDPayment(Guid cartId, Decimal amount);
        public Task<bool> TransferPayment(Guid cartId, Decimal amount);
    }
}
