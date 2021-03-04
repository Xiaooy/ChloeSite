using Ace.IdStrategy;
using Chloe.Application.Interfaces.System;
using Chloe.Application.Models.Department;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.System
{
    public class DepartmentAppService : AdminAppService, IDepartmentAppService
    {
        public List<Sys_Department> GetDepartments(string keyword)
        {
            var q = this.DbContext.Query<Sys_Department>().FilterDeleted().WhereIfNotNullOrEmpty(keyword, a => a.Name.Contains(keyword) || a.EnCode.Contains(keyword));
            q = q.OrderBy(a => a.SortCode);

            return q.ToList();
        }
        public List<SimpleDepartmentModel> GetSimpleModels()
        {
            var q = this.DbContext.Query<Sys_Department>().FilterDeleted();
            q = q.OrderBy(a => a.SortCode);

            return q.Select(a => new SimpleDepartmentModel() { Id = a.Id, Name = a.Name }).ToList();
        }
        public void Add(AddDepartmentInput input)
        {
            input.Validate();
            Sys_Department entity = this.CreateEntity<Sys_Department>();

            entity.Name = input.Name;
            entity.EnCode = input.EnCode;
            entity.Manager = input.Manager;
            entity.Telephone = input.Telephone;
            entity.MobilePhone = input.MobilePhone;
            entity.WeChat = input.WeChat;
            entity.Fax = input.Fax;
            entity.Email = input.Email;
            entity.SortCode = input.SortCode;
            entity.Description = input.Description;

            this.DbContext.Insert(entity);
        }
        public void Update(UpdateDepartmentInput input)
        {
            input.Validate();

            this.DbContext.Update<Sys_Department>(a => a.Id == input.Id, a => new Sys_Department()
            {
                Name = input.Name,
                EnCode = input.EnCode,
                Manager = input.Manager,
                Telephone = input.Telephone,
                MobilePhone = input.MobilePhone,
                WeChat = input.WeChat,
                Fax = input.Fax,
                Email = input.Email,
                SortCode = input.SortCode,
                Description = input.Description,
            });
        }
        public void Delete(string id)
        {
            this.SoftDelete<Sys_Department>(id);
        }
    }
}
