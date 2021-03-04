﻿using Ace;
using Ace.IdStrategy;
using Chloe.Application.Interfaces.System;
using Chloe.Application.Models.Role;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.System
{
    public class RoleAppService : AdminAppService, IRoleAppService
    {
        public List<Sys_Role> GetRoles(string keyword = "")
        {
            var q = this.DbContext.Query<Sys_Role>();
            if (keyword.IsNotNullOrEmpty())
            {
                q = q.Where(a => a.Name.Contains(keyword) || a.EnCode.Contains(keyword));
            }

            var ret = q.OrderBy(a => a.SortCode).ToList();
            return ret;
        }
        public List<SimpleRoleModel> GetSimpleModels()
        {
            var q = this.DbContext.Query<Sys_Role>().FilterDeletedAndDisabled().OrderBy(a => a.SortCode);
            var ret = q.Select(a => new SimpleRoleModel() { Id = a.Id, Name = a.Name }).ToList();
            return ret;
        }
        public void Add(AddRoleInput input)
        {
            input.Validate();
            Sys_Role role = this.CreateEntity<Sys_Role>();

            this.MapValueFromInput(role, input);

            string[] permissionIds = input.GetPermissionIds();
            List<Sys_RoleAuthorize> roleAuthorizeEntitys = this.CreateRoleAuthorizes(role.Id, permissionIds);

            this.DbContext.DoWithTransaction(() =>
            {
                this.DbContext.Insert(role);

                foreach (var roleAuthorizeEntity in roleAuthorizeEntitys)
                {
                    this.DbContext.Insert(roleAuthorizeEntity);
                }
            });
        }
        public void Update(UpdateRoleInput input)
        {
            input.Validate();

            Sys_Role role = this.DbContext.QueryByKey<Sys_Role>(input.Id, true);

            role.NotNull("角色不存在");

            this.MapValueFromInput(role, input);

            string[] permissionIds = input.GetPermissionIds();

            List<Sys_RoleAuthorize> roleAuthorizeEntitys = this.CreateRoleAuthorizes(role.Id, permissionIds);

            this.DbContext.DoWithTransaction(() =>
            {
                this.DbContext.Update(role);

                this.DbContext.Delete<Sys_RoleAuthorize>(a => a.RoleId == role.Id);

                foreach (var roleAuthorizeEntity in roleAuthorizeEntitys)
                {
                    this.DbContext.Insert(roleAuthorizeEntity);
                }
            });
        }

        public void Delete(string id)
        {
            id.NotNullOrEmpty();
            this.SoftDelete<Sys_Role>(id);
        }

        void MapValueFromInput(Sys_Role role, AddOrUpdateRoleInputBase input)
        {
            role.EnCode = input.EnCode;
            role.Name = input.Name;
            role.SortCode = input.SortCode;
            role.IsEnabled = input.IsEnabled;
            role.Description = input.Description;
        }

        List<Sys_RoleAuthorize> CreateRoleAuthorizes(string roleId, string[] permissionIds)
        {
            List<Sys_RoleAuthorize> roleAuthorizeEntitys = new List<Sys_RoleAuthorize>();
            foreach (var moduleId in permissionIds)
            {
                Sys_RoleAuthorize roleAuthorizeEntity = this.CreateEntity<Sys_RoleAuthorize>();
                roleAuthorizeEntity.RoleId = roleId;
                roleAuthorizeEntity.ModuleId = moduleId;
                roleAuthorizeEntitys.Add(roleAuthorizeEntity);
            }

            return roleAuthorizeEntitys;
        }
    }
}
