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
    
    public partial class T_ProductCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_ProductCategory()
        {
            this.T_Product = new HashSet<T_Product>();
        }
    
        public long pdCatetoryID { get; set; }
        public string PdCategoryName { get; set; }
        public long ParentCatgoryID { get; set; }
        public string pdCategoryCode { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Product> T_Product { get; set; }
    }
}
