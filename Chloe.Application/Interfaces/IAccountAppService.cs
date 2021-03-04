using Ace.Application;
using Chloe.Application.Models.Account;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Interfaces
{
    public interface IAccountAppService : IAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password">经过md5加密后的密码</param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CheckLogin(string userName, string password, out Sys_User user, out string msg);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPassword">明文</param>
        /// <param name="newPassword">明文</param>
        void ChangePassword(string oldPassword, string newPassword);
        void ModifyInfo(ModifyAccountInfoInput input);
    }
}
