using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Dapper;

namespace TestApiProj.Connection
{
   public class LocalDatabaseHub: Connection,IDatabaseHub
    {
        private static bool IsStoreProcedureNameCorrect(string storeProcedureName)
        {
            if (string.IsNullOrEmpty(storeProcedureName))
            {
                return false;
            }

            if (storeProcedureName.StartsWith("[") && storeProcedureName.EndsWith("]"))
            {
                return Regex.IsMatch(storeProcedureName,
                    @"^[\[]{1}[A-Za-z0-9_]+[\]]{1}[\.]{1}[\[]{1}[A-Za-z0-9_]+[\]]{1}$");
            }

            return Regex.IsMatch(storeProcedureName, @"^[A-Za-z0-9]+[\.]{1}[A-Za-z0-9]+$");
        }

        public IEnumerable<TResult> Query<TResult>(string storedProcedureName)
        {
            if (!IsStoreProcedureNameCorrect(storedProcedureName))
            {
                return null;
            }

            using (var connection = LocalConnection())
            {
                var result = connection.Query<TResult>(
                    sql: storedProcedureName, commandTimeout: null, commandType: CommandType.StoredProcedure);
                CloseConnection(connection);
                return result;
            }
        }

        public IEnumerable<TResult> Query<TResult>(string storedProcedureName, DynamicParameters parameters)
        {
            if (!IsStoreProcedureNameCorrect(storedProcedureName))
            {
                return null;
            }

            using (var connection = LocalConnection())
            {
                var result = connection.Query<TResult>(
                    sql: storedProcedureName, param: parameters, commandTimeout: null,
                    commandType: CommandType.StoredProcedure);
                CloseConnection(connection);
                return result;
            }
        }

        public IEnumerable<TResult> Query<TModel, TResult>(string storedProcedureName, TModel model)
        {
            if (!IsStoreProcedureNameCorrect(storedProcedureName))
            {
                return null;
            }

            using (var connection = LocalConnection())
            {
                var result = connection.Query<TResult>(
                        sql: storedProcedureName, param: model, commandTimeout: null,
                        commandType: CommandType.StoredProcedure);
                CloseConnection(connection);
                return result;
            }
        }

        public Task<IEnumerable<TResult>> QueryAsync<TResult>(string storedProcedureName, DynamicParameters parameters)
       {
            if (!IsStoreProcedureNameCorrect(storedProcedureName))
            {
                return null;
            }

            using (var connection = LocalConnection())
            {
                try
                {
                    return connection.QueryAsync<TResult>(
                        sql: storedProcedureName,
                        commandTimeout: null,
                        commandType: CommandType.StoredProcedure);
                }
                catch (Exception exception)
                {

                    throw exception;
                }

                finally
                {
                    CloseConnection(connection);
                }
            }
       }
    }
}
