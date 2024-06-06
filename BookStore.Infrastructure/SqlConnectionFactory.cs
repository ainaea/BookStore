using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure
{
    public class SqlConnectionFactory
    {
        private readonly IConfiguration config;

        public SqlConnectionFactory(IConfiguration config)
        {
            this.config = config;
        }
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(config.GetConnectionString("BookStoreConnection"));
        }
    }
}
