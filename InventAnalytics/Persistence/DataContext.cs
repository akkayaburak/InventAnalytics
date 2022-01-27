using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace InventAnalytics.Persistence
{
    public class DataContext : IDataContext
    {
        private readonly IConfiguration configuration;
        public SqlConnection SqlConnection;
        public DataContext(IConfiguration Configuration)
        {
            configuration = Configuration;
        }
        public void Close()
        {
            SqlConnection.Close();
        }

        public SqlConnection Open()
        {
            SqlConnection = new(configuration.GetConnectionString("Default"));
            SqlConnection.Open();
            return SqlConnection;
        }
    }

    public interface IDataContext
    {
        public SqlConnection Open();
        public void Close();
    }
}
