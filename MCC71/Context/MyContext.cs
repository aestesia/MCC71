using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCC71.Context
{
    public class MyContext
    {
        public static string GetConnection() 
        {
            return "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DB_MCC71;User ID=mcc71;Password=1234567890;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
    }
}
