using Microsoft.Extensions.Configuration;
using Npgsql;
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
        public NpgsqlConnection CreateConnection()
        {
            var cfg = config.GetConnectionString("BookStoreConnection");
            return new Npgsql.NpgsqlConnection (config.GetConnectionString("BookStoreConnection"));
        }
    }
}
