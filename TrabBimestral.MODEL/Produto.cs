//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrabBimestral.MODEL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Produto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Produto()
        {
            this.VendaProduto = new HashSet<VendaProduto>();
        }
    
        public int Pro_ID { get; set; }
        public string Pro_Nome { get; set; }
        public int Pro_Categoria { get; set; }
        public int Pro_Fornecedor { get; set; }
        public decimal Pro_Preco { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendaProduto> VendaProduto { get; set; }
    }
}