using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace TestApiProj.DataAccessLayer
{
   public class CrazyDeals:Base
    {

       private CrazyDeals()
       {
           
       }

       public CrazyDeals(bool isLiveConnection)
       {
           IsLiveConnection = isLiveConnection;
       }

       public async Task<IEnumerable<TResult>>GetCrazyDeals<TResult>( int lowerLimit, int upperLimit)
            where TResult: class 
       {
           try
           {
                var parameters = new DynamicParameters();
                parameters.Add(name: "@LowerLimit", value: lowerLimit, dbType: DbType.Int32,
                    direction: ParameterDirection.Input);
                parameters.Add(name: "@UpperLimit", value: upperLimit, dbType: DbType.Int32,
                    direction: ParameterDirection.Input);

                return await DatabaseHub.QueryAsync<TResult>(
                   storedProcedureName: @"[Deal].[USP_LoadHotDealsUsingLimit]",
                   parameters: parameters);

            }
           catch (Exception ex)
           {

               return null;
               //throw;
           }
            
       }
    }
}
