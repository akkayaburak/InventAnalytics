using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InventAnalytics.Persistence
{
    public class DataContext : IDataContext
    {
        private readonly IConfiguration configuration;
        public DataContext(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        public void Close()
        {
        }

        public SqlConnection Open()
        {
            SqlConnection connection = new(configuration.GetConnectionString("Default"));
            connection.Open();
            return connection;
        }
    }

    public interface IDataContext
    {
        public SqlConnection Open();
        public void Close();
    }
}
