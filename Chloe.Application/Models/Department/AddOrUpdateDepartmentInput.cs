using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Department
{
    public class AddOrUpdateDepartmentInput : ValidationModel
    {
        [RequiredAttribute(ErrorMessage = "部门名称不能为空")]
        public string Name { get; set; }
        public string EnCode { get; set; }
        public string Manager { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string WeChat { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public int? SortCode { get; set; }
        public string Description { get; set; }
    }

    public class AddDepartmentInput : AddOrUpdateDepartmentInput
    {
    }
    public class UpdateDepartmentInput : AddOrUpdateDepartmentInput
    {
        [RequiredAttribute(ErrorMessage = "{0}不能为空")]
        public string Id { get; set; }
    }
}
