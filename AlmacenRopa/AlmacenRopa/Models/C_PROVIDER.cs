//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlmacenRopa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class C_PROVIDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_PROVIDER()
        {
            this.PRODUCT = new HashSet<PRODUCT>();
        }
    
        public int ID_PROVIDER { get; set; }
        public string IDENTIFICATIONCARD { get; set; }
        public string NAMES { get; set; }
        public string SURNAMES { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public string NIT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT> PRODUCT { get; set; }
    }
}