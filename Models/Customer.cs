namespace WFM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Topics = new HashSet<Topic>();
        }

        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Team { get; set; }

        public string Version { get; set; }

        public string SqlVersion { get; set; }

        public bool IsCompleted { get; set; }

        public string Info { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Topic> Topics { get; set; }
    }
}
