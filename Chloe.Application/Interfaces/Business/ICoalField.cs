using Ace;
using Ace.Application;
using Chloe.Application.Models.Business;
using Chloe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Interfaces.Business
{
    public interface ICoalField : IAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="data"></param>
        /// <param name="heap">场地</param>
        /// <returns></returns>
        PagedData<Biz_MC1> GetMC1PageData(Pagination page, string data, string heap);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="data"></param>
        /// <param name="heap">场地</param>
        PagedData<Biz_MC2> GetMC2PageData(Pagination page, string data, string heap);

        /// <summary>
        /// 编辑煤场
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="type"></param>
        void UpdateMC(CoalPile cp, string type);
    }
}
