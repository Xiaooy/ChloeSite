using Ace.Business;
using Chloe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Entities
{
    [Table("MC2")]
    public class Biz_MC2
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("Id", IsPrimaryKey = true)]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("BH")]
        public string Bh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("MCH")]
        public string Mch { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("DH")]
        public string Dh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("CBH")]
        public string Cbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("PZBH")]
        public string Pzbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("CL")]
        public decimal Cl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("DQ")]
        public DateTime Dq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("WZDM")]
        public string Wzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("MZMC")]
        public string Mzmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("CM")]
        public string Cm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("HF")]
        public decimal Hf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("HFF")]
        public decimal Hff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("RZ")]
        public decimal Rz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("SF")]
        public decimal Sf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("T2")]
        public decimal T2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("LF")]
        public decimal Lf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string[] mdt
        {
            get; set;
        }

        public void setMdt()
        {
            this.mdt = DataHelper.ConvertList(this.Wzdm, 2);
        }
    }
}
