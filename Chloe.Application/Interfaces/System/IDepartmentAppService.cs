using Ace.Application;
using Chloe.Application.Models.Department;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Interfaces.System
{
    public interface IDepartmentAppService : IAppService
    {
        List<Sys_Department> GetDepartments(string keyword);
        List<SimpleDepartmentModel> GetSimpleModels();
        void Add(AddDepartmentInput input);
        void Update(UpdateDepartmentInput input);
        void Delete(string id);
    }
}
