﻿using CVManagerapp.Models;
using CVManagerapp.ViewModels;
using X.PagedList;

namespace CVManagerapp.Interfaces
{
    public interface ICVService
    {
        Task<IPagedList<CV>?> ListCVs(string searchString, int page, int pageSize);
        Task<CV>? GetCVByStudentId(string studentId);
        Task CreateCV(CVCreateVM cVCreateVM, ApplicationUser user);
        Task<CVDetailsVM>? GetCVDetailsByStudentId(string studentId);
        Task DeleteCV(CV cv);
        Task EditCV(CVEditVM cVEditVM);
        Task<int> AddEducationToCV(EducationVM educationVM);
        Task AddWorkExperienceToCV(WorkExperienceVM workExperienceVM);
        Task AddSkillToCV(SkillVM skillVM);
        Task AddProjectToCV(ProjectVM projectVM);
        Task AddCertificationToCV(CertificationVM certificationVM);
        Task AddLanguageToCV(LanguageVM languageVM);
        Task AddInterestToCV(InterestVM interestVM);
        Task EditEducation(EducationVM educationVM);
        Task EditWorkExperience(WorkExperienceVM workExperienceVM);
        Task EditSkill(SkillVM skillVM);
        Task EditProject(ProjectVM projectVM);
        Task EditCertification(CertificationVM certificationVM);
        Task EditLanguage(LanguageVM languageVM);
        Task EditInterest(InterestVM interestVM);


    }
}
