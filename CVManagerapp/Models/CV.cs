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

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^69\d{8}$", ErrorMessage = "Phone number must start with '69' and be 10 digits long")]
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
