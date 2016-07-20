using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApiProj.Connection;

namespace TestApiProj.DataAccessLayer
{

  public class Base
  {
      protected IDatabaseHub DatabaseHub;
      private bool _isLiveConnection;

      protected bool IsLiveConnection
      {
          set
          {
              //if (value)
              //{
                  DatabaseHub = new LiveDatabaseHub(value);
              //}
              //else
              //{
              //    DatabaseHub = new LocalDatabaseHub();
              //}

              _isLiveConnection = value;
          }
          get
          {
              return _isLiveConnection;
          }
      }
  }
}
