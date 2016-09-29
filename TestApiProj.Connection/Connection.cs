using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApiProj.Connection
{
    public abstract class Connection : IDisposable
    {      
        private static SqlConnectionStringBuilder LiveConnectionString()
        {
            return new SqlConnectionStringBuilder
            {
                ApplicationName = "LiveConnection",
                DataSource = "", //Ip address
                IntegratedSecurity = false,
                InitialCatalog = "AjkerDeal",
                Password = "", //password
                PersistSecurityInfo = false,
                UserID = "", //userId
                Pooling = true
            };        
        }


        private static SqlConnectionStringBuilder LocalConnectionString()
        {
            return new SqlConnectionStringBuilder
            {
                ApplicationName = "LocalConnection",
                DataSource = "", //Datasource
                InitialCatalog = "AjkerDeal",
                IntegratedSecurity = false,
                Password = "", //Password
                PersistSecurityInfo = false,
                Pooling = true,       
                UserID = ""//UserId
            };
        }
        
        protected static IDbConnection LiveConnection()
        {
            var connection = OpenConnection(LiveConnectionString());
            connection.Open();
            return connection;
        }
        
        protected static IDbConnection LocalConnection()
        {
            var connection = OpenConnection(LocalConnectionString());
            connection.Open();
            return connection;
        }

        private static IDbConnection OpenConnection(DbConnectionStringBuilder connectionString)
        {
            return new SqlConnection(connectionString.ConnectionString);
        }
        
        protected static bool CloseConnection(IDbConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
            return true;
        }
        private static void ClearPool()
        {
            SqlConnection.ClearAllPools();
        }
        
        public void Dispose()
        {
            ClearPool();
        }
    }
}
