using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Model;

namespace WebApplication.ViewModels
{
    public class EmployeeVM
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<EmployeeSkill> EmployeeSkills { get; set; }

        public EmployeeVM() { }

        public EmployeeVM(IEnumerable<Employee> employees, IEnumerable<Skill> skills, IEnumerable<EmployeeSkill> employeeSkills)
        {
            Employees = employees;
            Skills = skills;
            EmployeeSkills = employeeSkills;
        }
    }
}