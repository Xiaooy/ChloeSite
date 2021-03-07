using Ace.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chloe.Application.Models.Business
{
    public class CoalPile : ValidationModel
    {

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "{0}不能为空")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Bh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mch { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Dh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Cbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pzbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Cl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Dq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Wzdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Mzmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Cm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? Hf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? Hff { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? Rz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? Sf { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? T2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float? Lf { get; set; }


    }
}
