using CVManagerapp.Models;
using CVManagerapp.ViewModels;

namespace CVManagerapp.Interfaces
{
    public interface ICVService
    {
        Task<List<CV>?> ListCVs();
        Task<CV>? GetCVByStudentId(string studentId);
        Task CreateCV(CVCreateVM cVCreateVM, ApplicationUser user);
        Task<CVDetailsVM>? GetCVDetailsByStudentId(string studentId);
        Task DeleteCV(CV cv);
        Task AddEducationToCV(EducationVM educationVM);
        Task AddWorkExperienceToCV(WorkExperienceVM workExperienceVM);
        Task AddSkillToCV(SkillVM skillVM);
        Task AddProjectToCV(ProjectVM projectVM);
        Task AddCertificationToCV(CertificationVM certificationVM);
        Task AddLanguageToCV(LanguageVM languageVM);
        Task AddInterestToCV(InterestVM interestVM);
    }
}
