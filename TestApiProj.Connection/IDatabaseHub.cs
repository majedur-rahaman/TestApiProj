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
        IEnumerable<TResult> Query<TResult>(string storedProcedureName);
        IEnumerable<TResult> Query<TResult>(string storedProcedureName, DynamicParameters parameters);
        IEnumerable<TResult> Query<TModel, TResult>(string storedProcedureName, TModel model);

        Task<IEnumerable<TResult>> QueryAsync<TResult>(string storedProcedureName, DynamicParameters parameters);
    }
}