﻿using Ace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application
{
    public class AdminSession : AceSession<string>
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string DepartmentId { get; set; }
        public string DutyId { get; set; }
        public string RoleId { get; set; }
        public string LoginIP { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsAdmin { get; set; }
    }
}
