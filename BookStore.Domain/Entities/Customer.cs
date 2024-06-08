using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Customer: Nameable
    {
        public Customer(string name):base(name)
        {
            
        }
        public Customer(): base(string.Empty)
        {
            
        }
    }
}
