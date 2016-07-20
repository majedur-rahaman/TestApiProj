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
    
  public class LiveDatabaseHub: Connection, IDatabaseHub
  {

      private readonly bool _connectionSet;
      public LiveDatabaseHub(bool connectionHub)
      {
          this._connectionSet = connectionHub;
      }
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

      public Task<IEnumerable<TResult>> QueryAsync<TResult>(string storedProcedureName, DynamicParameters parameters)
      {
          if (!IsStoreProcedureNameCorrect(storedProcedureName))
          {
              return null;
          }

          using (var connection = LiveConnection(_connectionSet))
          {
              try
              {
                  return connection.QueryAsync<TResult>(
                      sql: storedProcedureName,
                      param:parameters,
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
