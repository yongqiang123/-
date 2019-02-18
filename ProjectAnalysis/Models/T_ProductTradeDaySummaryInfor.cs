using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAnalysis.Models
{
    [MetadataType(typeof(T_ProductTradeDaySummaryValidate))]
    public partial class T_ProductTradeDaySummary
    {
    }
    public  class T_ProductTradeDaySummaryValidate
       {
        [Display(Name ="交易日期")]
        public System.DateTime TradeDate { get; set; }
        [Display(Name ="商品")]
        public long ProductID { get; set; }
        //public int TradeMonth { get; set; }
        //public int TradeYear { get; set; }
        public string TradeYearMon { get; set; }
        [Display(Name ="市场")]
        public long CustomerID { get; set; }
        [Display(Name ="最低价")]
        public Nullable<decimal> MinPrice { get; set; }
        [Display(Name ="最高价")]
        public Nullable<decimal> MaxPrice { get; set; }
        [Display(Name ="平均价")]
        public Nullable<decimal> avgPrice { get; set; }
        [Display(Name ="交易数量")]
        public Nullable<decimal> TradeAmount { get; set; }
        [Display(Name ="交易额")]
        public Nullable<decimal> TradeMoney { get; set; }
        [Display(Name ="单位")]
        public string TradeUnit { get; set; }
        [Display(Name ="交易费")]
        public Nullable<decimal> TradeFee { get; set; }
    }
}