using Ace;
using Chloe.Application.Interfaces.Business;
using Chloe.Application.Models.Business;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="data">日期</param>
        /// <param name="heap">煤场号</param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="data"></param>
        /// <param name="heap"></param>
        /// <returns></returns>
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

        public void UpdateMC(CoalPile cp,string type)
        {
            cp.Validate();
            if (type == "1")
            {
                this.dbContextBiz.Update<Biz_MC1>(x => x.Id == cp.Id, x => new Biz_MC1()
                {
                    Bh = cp.Bh,
                    Mch = cp.Mch,
                    Dh = cp.Dh,
                    Cbh = cp.Cbh,
                    Pzbh = cp.Pzbh,
                    Cl = cp.Cl,
                    Dq = cp.Dq,
                    Wzdm = cp.Wzdm,
                    Mzmc = cp.Mzmc,
                    Cm = cp.Cm,
                    Hf = cp.Hf,
                    Hff = cp.Hff,
                    Rz = cp.Rz,
                    Sf = cp.Sf,
                    T2 = cp.T2,
                    Lf = cp.Lf,

                });
            }
            else
            {
                this.dbContextBiz.Update<Biz_MC2>(x => x.Id == cp.Id, x => new Biz_MC2()
                {
                    Bh = cp.Bh,
                    Mch = cp.Mch,
                    Dh = cp.Dh,
                    Cbh = cp.Cbh,
                    Pzbh = cp.Pzbh,
                    Cl = cp.Cl,
                    Dq = cp.Dq,
                    Wzdm = cp.Wzdm,
                    Mzmc = cp.Mzmc,
                    Cm = cp.Cm,
                    Hf = cp.Hf,
                    Hff = cp.Hff,
                    Rz = cp.Rz,
                    Sf = cp.Sf,
                    T2 = cp.T2,
                    Lf = cp.Lf,
                });

            }
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="bdate"></param>
        /// <param name="edate"></param>
        /// <returns></returns>
        public PagedData<Biz_RlglCzhHygl> GetRlglCzhHyglPagedData(Pagination page, DateTime? bdate, DateTime? edate)
        {
            var d_date = DateTime.Now.AddDays(-123);
            var query = this.dbContextBiz.Query<Biz_RlglCzhHygl>(x => x.Hylx == 160 && x.Hyrq> d_date);
            if (bdate != null)
            {
                query = query.Where(x => x.Hyrq >= bdate);
            }
            if (edate != null)
            {
                query = query.Where(x => x.Hyrq <= edate);
            }
            query = query.OrderByDesc(x => x.Hyrq);
            PagedData<Biz_RlglCzhHygl> pagedData = query.TakePageData(page);
            return pagedData;
        }
    }
}
