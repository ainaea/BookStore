using BookStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Implementations
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> TransferPayment(Guid cartId, decimal amount)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return true;
        }

        public async Task<bool> USSDPayment(Guid cartId, decimal amount)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return true;
        }

        public async Task<bool> WebPayment(Guid cartId, decimal amount)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return true;
        }
    }
}
