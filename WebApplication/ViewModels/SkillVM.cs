using System.ComponentModel.DataAnnotations;
using WebApplication.Model;

namespace WebApplication.ViewModels
{
    public class SkillVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name of the skill")]
        public string Name { get; set; }

        [Display(Name = "Description of the skill")]
        public string Description { get; set; }

        public static implicit operator SkillVM(Skill skill)
        {
            return new SkillVM
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description
            };
        }

        public static implicit operator Skill(SkillVM skillVM)
        {
            return new Skill
            {
                Id = skillVM.Id,
                Name = skillVM.Name,
                Description = skillVM.Description
            };
        }

    }
}