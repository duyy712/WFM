namespace WFM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SourceFile")]
    public partial class SourceFile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SourceFile()
        {
            CustomerRequests = new HashSet<CustomerRequest>();
        }

        public SourceFile( string n, string p)
        {
            FileName = n;
            FilePath = p;
        }

        public int Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerRequest> CustomerRequests { get; set; }
    }
}
