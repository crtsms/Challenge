namespace WebApplication.Model
{
    using System.Data.Entity;

    public partial class Context : DbContext
    {

        /// <summary>
        /// Context of execution on EF
        /// </summary>
        public Context()
            : base("name=TSBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
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
