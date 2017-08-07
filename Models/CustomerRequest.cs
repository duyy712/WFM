namespace WFM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerRequest")]
    public partial class CustomerRequest
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int TopicId { get; set; }

        public int FromEmployeeId { get; set; }

        public int ToEmployeeId { get; set; }

        public int DoEmployeeId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DesignDateExpired { get; set; }

        public DateTime CodeDateExpired { get; set; }

        public string Description { get; set; }

        public string DevDescription { get; set; }

        public int DescriptionFileId { get; set; }

        public int State { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Employee Employee2 { get; set; }

        public virtual Employee Employee3 { get; set; }

        public virtual SourceFile SourceFile { get; set; }

        public virtual Topic Topic { get; set; }
    }
}
