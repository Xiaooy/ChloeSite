using Ace.IdStrategy;
using Chloe.Admin.Common.Tree;
using Chloe.Application.Interfaces;
using Chloe.Application.Interfaces.System;
using Chloe.Application.Models.Department;
using Chloe.Entities;
using Chloe.Admin.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chloe.Admin.Areas.SystemManage.Controllers
{
    [LoginAuthorizeAttribute]
    public class DepartmentController : WebController
    {
        // GET: SystemManage/Role
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetModels(string keyword)
        {
            List<Sys_Department> data = this.CreateService<IDepartmentAppService>().GetDepartments(keyword);
            return this.SuccessData(data);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Add(AddDepartmentInput input)
        {
            this.CreateService<IDepartmentAppService>().Add(input);
            return this.AddSuccessMsg();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Update(UpdateDepartmentInput input)
        {
            this.CreateService<IDepartmentAppService>().Update(input);
            return this.UpdateSuccessMsg();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            this.CreateService<IDepartmentAppService>().Delete(id);
            return this.DeleteSuccessMsg();
        }
    }

}