//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mise.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Termin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Termin()
        {
            this.Mjesta = new HashSet<Mjesta>();
        }
    
        public int Id { get; set; }
        public string Naziv { get; set; }
        public System.TimeSpan Vrijeme { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mjesta> Mjesta { get; set; }
    }
}
