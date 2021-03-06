﻿using Ace.Business;
using Chloe.Annotations;
using System;

namespace Chloe.Entities
{
    [Table("MC1")]
    public class Biz_MC1
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
        public float Cl { get; set; }

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
        public float? Hf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("HFF")]
        public float? Hff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("RZ")]
        public float? Rz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("SF")]
        public float? Sf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("T2")]
        public float? T2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("LF")]
        public float? Lf { get; set; }



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
            this.mdt = DataHelper.ConvertList(this.Wzdm, 1);
        }
    }
}
