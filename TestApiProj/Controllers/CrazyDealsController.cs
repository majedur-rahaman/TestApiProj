using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TestApiProj.DataAccessLayer;
using TestApiProj.Models;

namespace TestApiProj.Controllers
{
    [RoutePrefix("CrazyDeals")]
    public class CrazyDealsController : ApiController
    {
        private bool isLiveConnection = true;


        [HttpPost, Route("GetCrazyDeals/{index}/{count}")]
        public async Task<IHttpActionResult> GetCrazyDeals(int index, int count)
        {
            try
            {
                var resultData = await new CrazyDeals(isLiveConnection).GetCrazyDeals<CrazyDealsModel>(index, count);

                if (resultData == null)
                {
                    return InternalServerError(new ApplicationException(message: "Database returned null."));
                }
                return Ok(resultData);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception: exception);
            }
        }

    }
}
