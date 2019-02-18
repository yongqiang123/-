using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAnalysis.Models
{
    [MetadataType(typeof(T_CustomerUserValidate))]
    public partial class T_CustomerUser
    {
    }
    public class T_CustomerUserValidate
    {
        //public long CustomerUserID { get; set; }
        public string CustomerUserBH { get; set; }
        [Display(Name = "姓名")]
        public string CustomerUserName { get; set; }
        [Display(Name = "市场")]
        public long CustomerID { get; set; }
        [Display(Name = "部门")]
        public long CustomerDepID { get; set; }
        [Display(Name = "职务")]
        public int UserTypeID { get; set; }
        //[Display="电话"]
        //public byte[] UserPhone { get; set; }
        [Display(Name ="地址")]
        public string UserAddress { get; set; }
        [Display(Name ="密码")]
        public string UserPwd { get; set; }
    }
}