namespace WebApplication.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeSkill")]
    public partial class EmployeeSkill
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int SkillId { get; set; }

        public int YearsExperience { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Skill Skill { get; set; }
    }
}
