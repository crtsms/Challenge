using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebApplication.Model;

namespace WebApplication.ViewModels
{
    public class EmployeeSkillVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Id of employee")]
        public int EmployeeId { get; set; }

        [Display(Name = "Skill of the employee")]
        public string Skill { get; set; }

        [Required]
        [Display(Name = "Id of skill")]
        [System.Web.Mvc.Remote(
            "SkillAlreadyAssigned", 
            "EmployeeSkill", 
            AdditionalFields = "EmployeeId,Id",  
            ErrorMessage = "Skill already assigned to the employee")]
        public int SkillId { get; set; }

        [Display(Name = "Skills avaible to be assing to employee")]
        public IEnumerable<SelectListItem> Skills { get; set; }

        [Required]
        [Display(Name = "Years of experience in that skill")]
        public int YearsExperience { get; set; }

        public static implicit operator EmployeeSkillVM(EmployeeSkill employeeSkill)
        {
            if (employeeSkill != null)
            {
                return new EmployeeSkillVM
                {
                    Id = employeeSkill.Id,
                    EmployeeId = employeeSkill.EmployeeId,
                    SkillId = employeeSkill.SkillId,
                    Skill = employeeSkill.Skill.Name,
                    YearsExperience = employeeSkill.YearsExperience
                };
            }
            else { return null; }
        }

        public static implicit operator EmployeeSkill(EmployeeSkillVM employeeSkillVM)
        {
            if (employeeSkillVM != null)
            {
                return new EmployeeSkill
                {
                    Id = employeeSkillVM.Id,
                    EmployeeId = employeeSkillVM.EmployeeId,
                    SkillId = employeeSkillVM.SkillId,
                    YearsExperience = employeeSkillVM.YearsExperience
                };
            }else { return null; }
        }
    }
}