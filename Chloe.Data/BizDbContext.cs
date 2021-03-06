using Chloe.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Data
{
   public class BizDbContext
    {
        public static IDbContext CreateContext()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["BizDbContext"].ConnectionString;
            IDbContext dbContext = CreateSqlServerContext(ConnectionString);
            return dbContext;
        }

        static IDbContext CreateSqlServerContext(string connString)
        {
            MsSqlContext dbContext = new MsSqlContext(connString);
            //dbContext.PagingMode = PagingMode.OFFSET_FETCH;
            return dbContext;
        }
    }
}
