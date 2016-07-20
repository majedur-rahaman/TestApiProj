using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace TestApiProj.Connection
{
   public interface IDatabaseHub
    {
        /// <summary>
        /// This method executes the Stored Procedure, gets the data from execution and returns that data in a list.
        /// This is a Generic Asynchronous Method, and it returns a list of POCO class.
        /// </summary>
        /// <typeparam name="TResult">This is the type of POCO class that will be sent as pamater and returned. For more info, refer to https://msdn.microsoft.com/en-us/library/vstudio/dd456872(v=vs.100).aspx. </typeparam>
        /// <param name="storedProcedureName">Stored Procedure's name. Expected to be a Verbatim String, e.g. @"[Schema].[Stored-Procedure-Name]"</param>
        /// <param name="parameters">Parameter required for executing Stored Procedure.</param>
        /// <returns>Returns a List of POCO class if successfully executed. If any exception is raised, it returns null.</returns>
        Task<IEnumerable<TResult>> QueryAsync<TResult>(string storedProcedureName, DynamicParameters parameters);
    }
}
