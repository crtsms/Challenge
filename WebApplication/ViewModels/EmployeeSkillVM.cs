using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Model;

namespace WebApplication.ViewModels
{
    public class EmployeeSkillVM
    {
        public int Id { get; set; }

        public Employee Employee { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        public int YearsExperience { get; set; }
    }
}