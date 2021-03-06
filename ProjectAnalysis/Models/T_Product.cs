//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectAnalysis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_Product()
        {
            this.T_ProductTradeDaySummary = new HashSet<T_ProductTradeDaySummary>();
        }
    
        public long ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public long pdCatetoryID { get; set; }
        public long CustomerID { get; set; }
        public string ProductAliasName { get; set; }
    
        public virtual T_Customer T_Customer { get; set; }
        public virtual T_ProductCategory T_ProductCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_ProductTradeDaySummary> T_ProductTradeDaySummary { get; set; }
    }
}
