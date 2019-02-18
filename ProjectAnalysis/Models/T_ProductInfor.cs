using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAnalysis.Models
{
    [MetadataType(typeof(T_ProductValidate))]
    public partial class T_Product
    {
    }
    public class T_ProductValidate
    {
        [Display(Name = "商品编码")]
        public string ProductCode { get; set; }
        [Display(Name="商品名称"),Required]
        public string ProductName { get; set; }
        [Display(Name ="商品类别")]
        public long pdCatetoryID { get; set; }
        [Display(Name ="市场")]
        public long CustomerID { get; set; }
        //public string ProductAliasName { get; set; }
    }
}