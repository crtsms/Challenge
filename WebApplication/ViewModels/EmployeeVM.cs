using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication.Model;

namespace WebApplication.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(30)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Skills of the employee")]
        public ICollection<EmployeeSkill> EmployeeSkills { get; set; }

        public static implicit operator EmployeeVM(Employee employee)
        {
            return new EmployeeVM
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                EmployeeSkills = employee.EmployeeSkills
            };
        }

        public static implicit operator Employee(EmployeeVM employeeVM)
        {
            return new Employee
            {
                Id = employeeVM.Id,
                FirstName = employeeVM.FirstName,
                LastName = employeeVM.LastName,
                PhoneNumber = employeeVM.PhoneNumber,
                EmployeeSkills = employeeVM.EmployeeSkills
            };
        }

    }
}