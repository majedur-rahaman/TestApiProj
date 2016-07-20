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
   public abstract class Connection: IDisposable
    {


       private static SqlConnectionStringBuilder ConnectionSetting(bool symbol)
       {
           if (symbol)
           {
                return new SqlConnectionStringBuilder
                {
                    ApplicationName = "LiveConnection",
                    DataSource = "50.28.38.161",
                    //DataSource = "192.168.0.4", 
                    InitialCatalog = "AjkerDeal",
                    IntegratedSecurity = false,

                    Password = "AD#RS@Dl+016",
                    PersistSecurityInfo = false,
                    Pooling = true,
                    UserID = "AjkerD"
                };


            }
           else
           {
                return new SqlConnectionStringBuilder
                {
                    ApplicationName = "MyLocalConnection", // Connection String's Name.
                    DataSource = "AJKERDEAL-SERVE",      // Database Source Address, e.g. IP Address, or Computer NetBIOS Name.
                    InitialCatalog = "AjkerDeal",        // Initial Database.
                    IntegratedSecurity = false,          // This is for indicating if connection uses is SSPI (false),
                                                         // or Windows Authentication (true). SSPI requires UserId and Password.
                    Password = "Rony@Deal",              // The password if SSPI is used.
                    PersistSecurityInfo = false,         // Discards every login info 
                                                         // e.g., UserId and Password after connection is established
                    Pooling = true,                      // Determines if ADO.Net Pooling is enabled.
                                                         // See https://msdn.microsoft.com/en-us/library/8xx3tyca(v=vs.110).aspx for more info.
                    UserID = "rony"                      // UserId for SSPI login.
                };
            }
        }

       private static SqlConnectionStringBuilder LiveConnectionString()
       {

           return new SqlConnectionStringBuilder
           {
               ApplicationName = "LiveConnection",
               DataSource = "50.28.38.161",
               //DataSource = "192.168.0.4", 
               IntegratedSecurity = false,
               InitialCatalog = "AjkerDeal",
               Password = "AD#RS@Dl+016",
               PersistSecurityInfo = false,
               UserID = "AjkerD",
               Pooling = true
           };

       }


       private static SqlConnectionStringBuilder LocalConnectionString()
       {
           return new SqlConnectionStringBuilder
           {
               ApplicationName = "LocalConnection", // Connection String's Name.
               DataSource = "AJKERDEAL-SERVE",      // Database Source Address, e.g. IP Address, or Computer NetBIOS Name.
               InitialCatalog = "AjkerDeal",        // Initial Database.
               IntegratedSecurity = false,          // This is for indicating if connection uses is SSPI (false),
                                                    // or Windows Authentication (true). SSPI requires UserId and Password.
               Password = "Rony@Deal",              // The password if SSPI is used.
               PersistSecurityInfo = false,         // Discards every login info 
                                                    // e.g., UserId and Password after connection is established
               Pooling = true,                      // Determines if ADO.Net Pooling is enabled.
                                                    // See https://msdn.microsoft.com/en-us/library/8xx3tyca(v=vs.110).aspx for more info.
               UserID = "rony"                      // UserId for SSPI login.
           };
       }

      
        /// <summary>
        /// This creates and returns a connection to the Live SQL Server.
        /// Connection will immediately open after it is created.
        /// The connection pool is cleared before connection is made.
        /// </summary>
        /// <returns>This returns a connection to the Live Database, or null is exception is raised.</returns>

        protected static IDbConnection LiveConnection(bool cconnection)
       {
           try
           {
                //ClearePool
                var connection = OpenConnection(ConnectionSetting(cconnection));
                connection.Open();

               return connection;
           }
           catch (Exception exception) when(exception is InvalidOperationException || exception is SqlException)
           {
               
               throw;
           }

       }
        /// <summary>
        /// This creates and returns a connection to the Local SQL Server.
        /// Connection will immediately open after it is created.
        /// The connection pool is cleared before connection is made.
        /// </summary>
        /// <returns>This returns a connection to the Live Database, or null is exception is raised.</returns>

        protected static IDbConnection LocalConnection()
       {

           try
           {
                //ClearePool
                var connection = OpenConnection(LocalConnectionString());
                connection.Open();
               return connection;
           }
           catch (Exception exception) when(exception is InvalidOperationException || exception is SqlException )
           {
               
               throw;
           }
       }

        /// <summary>
        /// This method creates a new connection to the Database.
        /// </summary>
        /// <param name="connectionString">This parameter contains the connection string used to establish connection.</param>
        /// <returns>The Database connection</returns>
        private static IDbConnection OpenConnection(DbConnectionStringBuilder connectionString)
        {
            return new SqlConnection(connectionString.ConnectionString);
        }
        /// <summary>
        /// This closes the given Database's connection.
        /// </summary>
        /// <param name="connection">This parameter contains the connection that need to be closed.</param>
        /// <returns>This function returns true when connection is successfullu closed, or closed prviously. Otherwise it returns false.</returns>

        protected static bool CloseConnection(IDbConnection connection)
        {
            try
            {

                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
                return true;

            }
            catch (Exception exception) when (exception is SqlException)
            {

                throw;
            }
        }
        private static void ClearPool()
       {
           SqlConnection.ClearAllPools();
       }

        /// <summary>
        /// Clears the ADO.Net Server Connection Pooling.
        /// This method is called before initiating any connection.
        /// See https://msdn.microsoft.com/en-us/library/8xx3tyca(v=vs.110).aspx for more info        
        /// </summary>
        public void Dispose()
       {
           ClearPool();
       }
    }
}
