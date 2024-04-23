using CVManagerapp.Models;
using System.Collections.Generic;
 
namespace CVManagerapp.ViewModels
{
    public class CVDetailsVM
    {
        public CVVM CV { get; set; }
        public List<EducationVM> Educations { get; set; }
        public List<WorkexperienceVM> workexperiences { get; set; }
        public List<SkillVM> skills { get; set; }
        public List<ProjectVM> projects { get; set; }
        public List<CertificationVM> certifications { get; set; }
        public List<LanguageVM> languages { get; set; }
        public List<InterestVM> interests { get; set; }

    }
 
    public class CVVM
    {
                
        public string UserId { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }       
        public string Title { get; set; }       
        public DateTime? DateOfBirth { get; set; }       
        public string Address { get; set; }       
        public string Email { get; set; }        
        public string Phone { get; set; }
        
    }
 
    public class EducationVM
    {
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    
    public class WorkexperienceVM
    {
        public string Company { get; set; }
        public string Position { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
    public class SkillVM
    {
        public string Name { get; set; }
    }
    public class ProjectVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class CertificationVM
    {
        public string Name { get; set; }
        public DateTime? IssueDate { get; set; }
        public string IssuingOrganization { get; set; }
    }
    public class LanguageVM
    {
        public string Name { get; set; }
        public string ProficiencyLevel { get; set; }
    }
    public class InterestVM
    {
        public string Name { get; set; }
    }


}