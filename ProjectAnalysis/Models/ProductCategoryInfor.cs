using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAnalysis.Models
{
    [MetadataType(typeof(T_ProductCategoryValidate))]
    public partial class T_ProductCategory
    {
    }
    public class T_ProductCategoryValidate {
        [Display(Name ="类别名称"),Required(ErrorMessage ="不能为空")]
        public string PdCategoryName { get; set; }
        [Display(Name ="父级")]
        public long ParentCatgoryID { get; set; }
        [Required(ErrorMessage ="不能为空"), Display(Name ="类别编码")]
        public string pdCategoryCode { get; set; }
    }
    
}