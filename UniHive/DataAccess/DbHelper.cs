using Microsoft.AspNetCore.Connections;
using Microsoft.Data.SqlClient;

namespace UniHive.DataAccess
{

    /*This class provides a reusable way to access the database connection
    using the configured connection string from appsettings.json.*/
    
    public class DbHelper
    {
        private readonly IConfiguration _configuration;

        public DbHelper(IConfiguration configuration)
        { 
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            var connString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connString);
        }
    }
}
