namespace WFM.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataModelContext : DbContext
    {
        public DataModelContext()
            : base("name=DataModelContext")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerRequest> CustomerRequests { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<SourceFile> SourceFiles { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CustomerRequests)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.FromEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CustomerRequests1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.DoEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CustomerRequests2)
                .WithRequired(e => e.Employee2)
                .HasForeignKey(e => e.DoEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CustomerRequests3)
                .WithRequired(e => e.Employee3)
                .HasForeignKey(e => e.ToEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SourceFile>()
                .HasMany(e => e.CustomerRequests)
                .WithRequired(e => e.SourceFile)
                .HasForeignKey(e => e.DescriptionFileId);
        }
    }
}
