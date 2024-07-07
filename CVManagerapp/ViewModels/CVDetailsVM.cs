using CVManagerapp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CVManagerapp.ViewModels
{
    public class CVDetailsVM
    {
        public CVVM CV { get; set; }
        public List<EducationVM> Educations { get; set; }
        public List<WorkExperienceVM> workexperiences { get; set; }
        public List<SkillVM> skills { get; set; }
        public List<ProjectVM> projects { get; set; }
        public List<CertificationVM> certifications { get; set; }
        public List<LanguageVM> languages { get; set; }
        public List<InterestVM> interests { get; set; }

    }
 
    public class CVVM
    {
        public int Id {  get; set; }        
        public string UserId { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }       
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }       
        public string Address { get; set; }       
        public string Email { get; set; }        
        public string Phone { get; set; }
        
    }

    public class EducationVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Institution is required")]
        [StringLength(300)]
        public string Institution { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        [StringLength(300)]
        public string Degree { get; set; }

        [Required(ErrorMessage = "Field of Study is required")]
        [StringLength(300)]
        public string FieldOfStudy { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "End Date is required")]
        [EndDateAfterStartDate(ErrorMessage = "End Date must be after Start Date")]
        public DateTime EndDate { get; set; }
        public int CVId { get; set; } 
    }


    public class WorkExperienceVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Company is required")]
        [StringLength(300)]
        public string Company { get; set; }
        [Required(ErrorMessage = "Position is required")]
        [StringLength(300)]
        public string Position { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End Date is required")]
        [EndDateAfterStartDate(ErrorMessage = "End Date must be after Start Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(300)]
        public string Description { get; set; }
        public int CVId { get; set; }
    }
    public class SkillVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(300)]
        public string Name { get; set; }
        public int CVId { get; set; }
    }
    public class ProjectVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(300)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(300)]
        public string Description { get; set; }
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "End Date is required")]
        [EndDateAfterStartDate(ErrorMessage = "End Date must be after Start Date")]
        public DateTime EndDate { get; set; }
        public int CVId { get; set; }
    }

    public class CertificationVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        [StringLength(300)]
        public string Name { get; set; }

        [Display(Name = "Issue Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Issue Date is required")]
        public DateTime IssueDate { get; set; }
        [Display(Name = "Issuing Organization")]
        [Required(ErrorMessage = "IssuingOrganization is required")]
        [StringLength(300)]
        public string IssuingOrganization { get; set; }
        public int CVId { get; set; }
    }
    public class LanguageVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(300)]
        public string Name { get; set; }
        [Display(Name = "Proficiency Level")]
        [Required(ErrorMessage = "Proficiency Level is required")]
        [StringLength(300)]
        public string ProficiencyLevel { get; set; }
        public int CVId { get; set; }
    }
    public class InterestVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(300)]
        public string Name { get; set; }
        public int CVId { get; set; }
    }

 

public class EndDateAfterStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var endDate = (DateTime?)value;
            var startDateProperty = validationContext.ObjectType.GetProperty("StartDate");
            if (startDateProperty == null)
            {
                throw new ArgumentException("Property 'StartDate' not found.");
            }

            var startDateValue = (DateTime?)startDateProperty.GetValue(validationContext.ObjectInstance);
            if (endDate.HasValue && startDateValue.HasValue && endDate < startDateValue)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }



}