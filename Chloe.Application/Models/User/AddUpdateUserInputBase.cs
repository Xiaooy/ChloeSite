using Chloe.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.User
{
    public class AddUpdateUserInputBase : Ace.Application.ValidationModel
    {
        public string DepartmentId { get; set; }
        public string RoleId { get; set; }
        public string DutyId { get; set; }
        public string RealName { get; set; }
        public Gender? Gender { get; set; }
        public string MobilePhone { get; set; }
        public DateTime? Birthday { get; set; }
        public string WeChat { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public string Description { get; set; }
    }
}
