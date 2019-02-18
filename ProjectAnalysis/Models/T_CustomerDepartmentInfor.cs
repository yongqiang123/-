using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectAnalysis.Models
{
    [MetadataType(typeof(T_CustomerDepartmentValidate))]
    public partial class T_CustomerDepartment
    {
    }
    public class T_CustomerDepartmentValidate
    {
        [Required,Display(Name ="部门名称")]
        public string  CustomerDepName { get; set; }
        [Display(Name ="市场名称")]
        public long CustomerID { get; set; }
    }
 }