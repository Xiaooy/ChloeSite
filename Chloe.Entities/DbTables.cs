using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    public static class DbTables
    {
        public static IQuery<User> GetUsers(this IDbContext dbContext)
        {
            return dbContext.Query<User>();
        }
        public static IQuery<WikiDocument> GetWikiDocuments(this IDbContext dbContext)
        {
            return dbContext.Query<WikiDocument>();
        }

        public static IQuery<Sys_User> GetSys_Users(this IDbContext dbContext)
        {
            return dbContext.Query<Sys_User>();
        }
        public static IQuery<Sys_UserLogOn> GetSys_UserLogOns(this IDbContext dbContext)
        {
            return dbContext.Query<Sys_UserLogOn>();
        }
        public static IQuery<Sys_Module> GetSys_Modules(this IDbContext dbContext)
        {
            return dbContext.Query<Sys_Module>();
        }
        public static IQuery<Sys_RoleAuthorize> GetSys_RoleAuthorizes(this IDbContext dbContext)
        {
            return dbContext.Query<Sys_RoleAuthorize>();
        }
    }
}
