using Ace;
using Chloe.Application.Interfaces.Business;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Implements.Business
{
    public class CoalField : AdminAppService, ICoalField
    {
        public PagedData<Biz_MC1> GetMC1PageData(Pagination page, string data, string heap)
        {
           var query= this.dbContextBiz.Query<Biz_MC1>(x => x.Mch == heap);
            if(data.IsNotNullOrEmpty())
            {
                query = query.Where(x => x.Bh == data);
            }
            PagedData<Biz_MC1> pagedData = query.TakePageData(page);
            foreach (var item in pagedData.DataList)
            {
                item.setMdt();
            }
            return pagedData;
        }

        public PagedData<Biz_MC2> GetMC2PageData(Pagination page, string data, string heap)
        {
            var query = this.dbContextBiz.Query<Biz_MC2>(x => x.Mch == heap);
            if (data.IsNotNullOrEmpty())
            {
                query = query.Where(x => x.Bh == data);
            }
            PagedData<Biz_MC2> pagedData = query.TakePageData(page);
            foreach (var item in pagedData.DataList)
            {
                item.setMdt();
            }
            return pagedData;
        }

         
    }
}
