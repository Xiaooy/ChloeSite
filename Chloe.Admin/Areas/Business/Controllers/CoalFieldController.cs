﻿using Chloe.Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chloe.Application.Interfaces.System;
using Chloe.Application.Models.Department;
using Chloe.Entities;
using Ace;
using Chloe.Application.Interfaces.Business;

namespace Chloe.Admin.Areas.Business.Controllers
{
    
    public class CoalFieldController : WebController
    {
        // GET: Business/IssueOneCoalField
        public ActionResult Index()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult GetMCPageData(int page, int limit, string date, string heap, string type)
        {
            date = date.IsNotNullOrEmpty() ? date.Replace("-", "") : "";
            Pagination p = new Pagination(page, limit);
            if (type == "1")
            {
                PagedData<Biz_MC1> pagedData = this.CreateService<ICoalField>().GetMC1PageData(p, date, heap);
                return this.SuccessData(pagedData);
            }
            else
            {
                PagedData<Biz_MC2> pagedData = this.CreateService<ICoalField>().GetMC2PageData(p, date, heap);
                return this.SuccessData(pagedData);
            }
        }
    }
}