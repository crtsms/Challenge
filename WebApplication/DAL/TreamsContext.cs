namespace WebApplication.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using WebApplication.Model;

    public partial class TreamsContext : DbContext
    {

        /// <summary>
        /// Context of execution on EF
        /// </summary>
        public TreamsContext()
            : base("name=TreamsContext")
        {
        }

        /// <summary>
        /// Employees from database
        /// </summary>
        public virtual DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// EmployeeSkills from database
        /// </summary>
        public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }

        /// <summary>
        /// Skills from database
        /// </summary>
        public virtual DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
             * TO THE FUTURE
             * NECESSARY SPEND MORE TIME PREPARING THE ARCHITECTURE
             * AND THE MIGRATIONS FOR THE DATABASE
             * 
            modelBuilder.Entity<EmployeeSkill>()
                .HasKey(e => new { e.EmployeeId, e.SkillId });

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeSkills)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Skill>()
                .HasMany(e => e.EmployeeSkills)
                .WithRequired(e => e.Skill)
                .HasForeignKey(e => e.SkillId)
                .WillCascadeOnDelete(false);

            */
        }
    }
}
