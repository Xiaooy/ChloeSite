using Ace;
using Ace.Exceptions;
using Ace.IdStrategy;
using Ace.Security;
using Chloe.Application.Common;
using Chloe.Application.Interfaces;
using Chloe.Application.Interfaces.System;
using Chloe.Application.Models.User;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.System
{
    public class UserAppService : AdminAppService, IUserAppService
    {
        public void EnableAccount(string userId)
        {
            this.DbContext.Enable<Sys_User>(userId);
        }
        public void DisableAccount(string userId)
        {
            this.DbContext.Disable<Sys_User>(userId);
        }
        public void RevisePassword(string userId, string pwdText)
        {
            userId.NotNullOrEmpty("用户 Id 不能为空");
            PasswordHelper.EnsurePasswordLegal(pwdText);

            var user = this.DbContext.QueryByKey<Sys_User>(userId);
            if (user == null || user.UserName.ToLower() == AppConsts.AdminUserName)
                throw new Ace.Exceptions.InvalidDataException("用户不存在");

            string userSecretkey = UserHelper.GenUserSecretkey();
            string encryptedPassword = PasswordHelper.Encrypt(pwdText, userSecretkey);

            this.DbContext.DoWithTransaction(() =>
            {
                this.DbContext.Update<Sys_UserLogOn>(a => a.UserId == userId, a => new Sys_UserLogOn() { UserSecretkey = userSecretkey, UserPassword = encryptedPassword });
                this.Log(Entities.Enums.LogType.Update, "User", true, "重置用户[{0}]密码".ToFormat(userId));
            });
        }
        public void DeleteAccount(string userId)
        {
            userId.NotNullOrEmpty("用户 Id 不能为空");
            this.SoftDelete<Sys_User>(userId);
        }

        public void AddUser(AddUserInput input)
        {
            input.Validate();

            string userName = input.UserName.ToLower();

            bool exists = this.DbContext.GetSys_Users().Where(a => a.UserName == userName).Any();
            if (exists)
                throw new InvalidDataException("用户名[{0}]已存在".ToFormat(input.UserName));

            Sys_User user = this.CreateEntity<Sys_User>();
            user.UserName = userName;
            user.DepartmentId = input.DepartmentId;
            user.RoleId = input.RoleId;
            user.DutyId = input.DutyId;
            user.RealName = input.RealName;
            user.Gender = input.Gender;
            user.MobilePhone = input.MobilePhone;
            user.Birthday = input.Birthday;
            user.WeChat = input.WeChat;
            user.Email = input.Email;
            user.IsEnabled = input.IsEnabled;
            user.Description = input.Description;

            string userSecretkey = UserHelper.GenUserSecretkey();
            string encryptedPassword = PasswordHelper.Encrypt(input.Password, userSecretkey);

            Sys_UserLogOn logOnEntity = new Sys_UserLogOn();
            logOnEntity.Id = IdHelper.CreateGuid();
            logOnEntity.UserId = user.Id;
            logOnEntity.UserSecretkey = userSecretkey;
            logOnEntity.UserPassword = encryptedPassword;

            this.DbContext.DoWithTransaction(() =>
            {
                this.DbContext.Insert(user);
                this.DbContext.Insert(logOnEntity);
            });
        }
        public void UpdateUser(UpdateUserInput input)
        {
            input.Validate();

            this.DbContext.Update<Sys_User>(a => a.Id == input.Id, a => new Sys_User()
            {
                //OrganizeId = input.OrganizeId,
                DepartmentId = input.DepartmentId,
                RoleId = input.RoleId,
                DutyId = input.DutyId,
                RealName = input.RealName,
                Gender = input.Gender,
                MobilePhone = input.MobilePhone,
                Birthday = input.Birthday,
                WeChat = input.WeChat,
                Email = input.Email,
                IsEnabled = input.IsEnabled,
                Description = input.Description,
            });
        }

        public PagedData<Sys_User> GetPageData(Pagination page, string keyword)
        {
            var q = this.DbContext.GetSys_Users().FilterDeleted();

            q = q.WhereIfNotNullOrEmpty(keyword, a => a.UserName.Contains(keyword) || a.RealName.Contains(keyword) || a.MobilePhone.Contains(keyword));
            q = q.Where(a => a.UserName != AppConsts.AdminUserName);
            q = q.OrderBy(a => a.DepartmentId).ThenByDesc(a => a.CreationTime);

            PagedData<Sys_User> pagedData = q.TakePageData(page);

            return pagedData;
        }
    }
}
