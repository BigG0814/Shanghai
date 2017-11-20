namespace WorkScheduleSystem.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using WorkSchedule.Data.Entities;

    public partial class WorkScheduleContext : DbContext
    {
        public WorkScheduleContext()
            : base("name=WorkScheduleDB")
        {
        }

        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<EmployeeSkills> EmployeeSkills { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>()
                .Property(e => e.HomePhone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .HasMany(e => e.EmployeeSkills)
                .WithRequired(e => e.Employees)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EmployeeSkills>()
                .Property(e => e.HourlyWage)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Skills>()
                .HasMany(e => e.EmployeeSkills)
                .WithRequired(e => e.Skills)
                .WillCascadeOnDelete(false);
        }
    }
}
