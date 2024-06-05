using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Nameable: Identifiable
    {
        public string Name { get; set; }
        public Nameable(string name)
        {
            Name = name;
        }
    }
}
