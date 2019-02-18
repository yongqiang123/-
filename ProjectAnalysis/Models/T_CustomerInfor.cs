using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAnalysis.Models
{
    [MetadataType(typeof(T_CustomerValidate))]
    public partial class T_Customer
    {
    }
    public class T_CustomerValidate
    {
        [Required(ErrorMessage = "不能为空")]
        [Display(Name ="市场名称")]
        public string CustomerName { get; set; }
        [Display(Name ="地址")]
        public string CustomerAddress { get; set; }
         
         [RegularExpression(@"^1([358][0-9]|4[579]|66|7[0135678]|9[89])[0-9]{8}$", ErrorMessage = "手机号不正确"),Display(Name ="联系电话")]//可以验证178的手机号码  
        public string CustomerPhone { get; set; }
        [Display(Name ="联系人")]
        public string CustomerLinkMan { get; set; }
        [Required(ErrorMessage ="不能为空")]
        public string CustomerKey { get; set; }

    }
}