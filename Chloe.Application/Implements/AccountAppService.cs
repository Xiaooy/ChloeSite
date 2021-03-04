using Ace.Security;
using Chloe.Application.Common;
using Chloe.Application.Interfaces;
using Chloe.Application.Models.Account;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements
{
    public class AccountAppService : AdminAppService, IAccountAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password">前端传过来的是经过md5加密后的密码</param>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CheckLogin(string userName, string password, out Sys_User user, out string msg)
        {
            userName.NotNullOrEmpty();
            password.NotNullOrEmpty();

            user = null;
            msg = null;

            var users = this.DbContext.GetSys_Users();
            var userLogOns = this.DbContext.GetSys_UserLogOns();

            var view = users.InnerJoin(userLogOns, (u, userLogOn) => u.Id == userLogOn.UserId).Select((u, userLogOn) => new { User = u, UserLogOn = userLogOn });

            view = view.Where(a => a.User.IsDeleted == false);

            var viewEntity = view.FirstOrDefault(a => a.User.UserName == userName);

            if (viewEntity == null)
            {
                msg = "账户不存在，请重新输入";
                return false;
            }

            if (viewEntity.User.IsEnabled == false)
            {
                msg = "账户被系统锁定,请联系管理员";
                return false;
            }

            Sys_User userEntity = viewEntity.User;
            Sys_UserLogOn userLogOnEntity = viewEntity.UserLogOn;

            string dbPassword = PasswordHelper.EncryptMD5Password(password, userLogOnEntity.UserSecretkey);
            if (dbPassword != userLogOnEntity.UserPassword)
            {
                msg = "密码不正确，请重新输入";
                return false;
            }

            DateTime lastVisitTime = DateTime.Now;
            this.DbContext.Update<Sys_UserLogOn>(a => a.Id == userLogOnEntity.Id, a => new Sys_UserLogOn() { LogOnCount = a.LogOnCount + 1, PreviousVisitTime = userLogOnEntity.LastVisitTime, LastVisitTime = lastVisitTime });
            user = userEntity;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldPassword">明文</param>
        /// <param name="newPassword">明文</param>
        public void ChangePassword(string oldPassword, string newPassword)
        {
            PasswordHelper.EnsurePasswordLegal(newPassword);

            AdminSession session = this.Session;

            Sys_UserLogOn userLogOn = this.DbContext.Query<Sys_UserLogOn>().Where(a => a.UserId == session.UserId).First();

            string encryptedOldPassword = PasswordHelper.Encrypt(oldPassword, userLogOn.UserSecretkey);

            if (encryptedOldPassword != userLogOn.UserPassword)
                throw new Ace.Exceptions.InvalidDataException("旧密码不正确");

            string newUserSecretkey = UserHelper.GenUserSecretkey();
            string newEncryptedPassword = PasswordHelper.Encrypt(newPassword, newUserSecretkey);

            this.DbContext.DoWithTransaction(() =>
            {
                this.DbContext.Update<Sys_UserLogOn>(a => a.UserId == session.UserId, a => new Sys_UserLogOn() { UserSecretkey = newUserSecretkey, UserPassword = newEncryptedPassword });
                this.Log(Entities.Enums.LogType.Update, "Account", true, "用户[{0}]修改密码".ToFormat(session.UserId));
            });
        }

        public void ModifyInfo(ModifyAccountInfoInput input)
        {
            input.Validate();

            var session = this.Session;

            this.DbContext.Update<Sys_User>(a => a.Id == session.UserId, a => new Sys_User()
            {
                RealName = input.RealName,
                Gender = input.Gender,
                MobilePhone = input.MobilePhone,
                Birthday = input.Birthday,
                WeChat = input.WeChat,
                Email = input.Email,
                Description = input.Description,
            });
        }
    }
}
