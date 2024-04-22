using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVManagerapp.Models
{
    public class CV
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } 

        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Title { get; set; } 

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }

        
        public ICollection<Education> Educations { get; set; }

        
        public ICollection<WorkExperience> WorkExperiences { get; set; }

        
        public ICollection<Skill> Skills { get; set; }

        
        public ICollection<Project> Projects { get; set; }

        
        public ICollection<Certification> Certifications { get; set; }

        
        public ICollection<Language> Languages { get; set; }

        
        public ICollection<Interest> Interests { get; set; }

        
    }

    
}
