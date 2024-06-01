using CVManagerapp.Data;
using CVManagerapp.Interfaces;
using CVManagerapp.Models;
using CVManagerapp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CVManagerapp.Implementations
{
    public class CVService : ICVService
    {
        private readonly ApplicationDbContext _db;

        public CVService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddCertificationToCV(CertificationVM certificationVM)
        {
            var certification = new Certification
            {
                CVId = certificationVM.CVId,
                Name = certificationVM.Name,
                IssueDate = certificationVM.IssueDate,
                IssuingOrganization = certificationVM.IssuingOrganization,
            };
            await _db.Certifications.AddAsync(certification);
            await _db.SaveChangesAsync();
        }

        public async Task AddEducationToCV(EducationVM educationVM)
        {
            var education = new Education
            {
                CVId = educationVM.CVId,
                Institution = educationVM.Institution,
                Degree = educationVM.Degree,
                FieldOfStudy = educationVM.FieldOfStudy,
                StartDate = educationVM.StartDate,
                EndDate = educationVM.EndDate
            };
            await _db.Educations.AddAsync(education);
            await _db.SaveChangesAsync();
        }

        public async Task AddInterestToCV(InterestVM interestVM)
        {
            var interest = new Interest
            {
                CVId = interestVM.CVId,
                Name = interestVM.Name,
            };
            await _db.Interests.AddAsync(interest);
            await _db.SaveChangesAsync();
        }

        public async Task AddLanguageToCV(LanguageVM languageVM)
        {
            var language = new Language
            {
                CVId = languageVM.CVId,
                Name = languageVM.Name,
                ProficiencyLevel = languageVM.ProficiencyLevel,
            };
            await _db.Languages.AddAsync(language);
            await _db.SaveChangesAsync();
        }

        public async Task AddProjectToCV(ProjectVM projectVM)
        {
            var project = new Project
            {
                CVId = projectVM.CVId,
                Title = projectVM.Title,
                Description = projectVM.Description,
                StartDate = projectVM.StartDate,
                EndDate = projectVM.EndDate
            };
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
        }

        public async Task AddSkillToCV(SkillVM skillVM)
        {
            var skill = new Skill
            {
                CVId = skillVM.CVId,
                Name = skillVM.Name,
            };
            await _db.Skills.AddAsync(skill);
            await _db.SaveChangesAsync();
        }

        public async Task AddWorkExperienceToCV(WorkExperienceVM workExperienceVM)
        {
            var workExperience = new WorkExperience
            {
                CVId = workExperienceVM.CVId,
                Company = workExperienceVM.Company,
                Position = workExperienceVM.Position,
                Description = workExperienceVM.Description,
                StartDate = workExperienceVM.StartDate,
                EndDate = workExperienceVM.EndDate
            };
            await _db.WorkExperiences.AddAsync(workExperience);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCV(CVCreateVM cVCreateVM, ApplicationUser user)
        {
            var cv = new CV()
            {
                UserId = cVCreateVM.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Title = cVCreateVM.Title,
                DateOfBirth = cVCreateVM.DateOfBirth.HasValue ? cVCreateVM.DateOfBirth.Value.Date : default,
                Address = cVCreateVM.Address,
                Phone = cVCreateVM.Phone
            };

            await _db.CVs.AddAsync(cv);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCV(CV cv)
        {
            _db.CVs.Remove(cv);
            await _db.SaveChangesAsync();
        }

        public async Task<CV>? GetCVByStudentId(string studentId)
        {
            var cv = await _db.CVs.SingleOrDefaultAsync(x => x.UserId == studentId);
            return cv;
        }

        public async Task<CVDetailsVM>? GetCVDetailsByStudentId(string studentId)
        {
            var cvDetails = await _db.CVs
                .Where(c => c.UserId == studentId)
                .Select(c => new CVDetailsVM
                {
                    CV = new CVVM
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        Title = c.Title,
                        DateOfBirth = c.DateOfBirth,
                        Address = c.Address,
                        Email = c.Email,
                        Phone = c.Phone
                    },
                    Educations = c.Educations.Select(e => new EducationVM
                    {
                        Institution = e.Institution,
                        Degree = e.Degree,
                        FieldOfStudy = e.FieldOfStudy,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate
                    }).ToList(),
                    workexperiences = c.WorkExperiences.Select(e => new WorkExperienceVM
                    {
                        Company = e.Company,
                        Position = e.Position,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Description = e.Description,
                    }).ToList(),
                    skills = c.Skills.Select(e => new SkillVM
                    {
                        Name = e.Name
                    }).ToList(),
                    projects = c.Projects.Select(e => new ProjectVM
                    {
                        Title = e.Title,
                        Description = e.Description,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate
                    }).ToList(),
                    certifications = c.Certifications.Select(e => new CertificationVM
                    {
                        Name = e.Name,
                        IssueDate = e.IssueDate,
                        IssuingOrganization = e.IssuingOrganization,
                    }).ToList(),
                    languages = c.Languages.Select(e => new LanguageVM
                    {
                        Name = e.Name,
                        ProficiencyLevel = e.ProficiencyLevel
                    }).ToList(),
                    interests = c.Interests.Select(e => new InterestVM
                    {
                        Name = e.Name
                    }).ToList(),
                })
                .FirstOrDefaultAsync();
            return cvDetails;
        }

        public async Task<List<CV>?> ListCVs()
        {
            var CVs = await _db.CVs.ToListAsync();
            return CVs;
        }
    }
}
