using CVManagerapp.Data;
using CVManagerapp.Interfaces;
using CVManagerapp.Models;
using CVManagerapp.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using X.PagedList;

namespace CVManagerapp.Implementations
{
    public class CVService : ICVService
    {
        private readonly ApplicationDbContext _db;

        public CVService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddCertificationToCV(CertificationVM certificationVM)
        {
            var certification = new Certification
            {
                CVId = certificationVM.CVId,
                Name = certificationVM.Name,
                IssueDate = certificationVM.IssueDate.Date,
                IssuingOrganization = certificationVM.IssuingOrganization,
            };
            await _db.Certifications.AddAsync(certification);
            await _db.SaveChangesAsync();
            return certification.Id;
        }

        public async Task<int> AddEducationToCV(EducationVM educationVM)
        {
            var education = new Education
            {
                CVId = educationVM.CVId,
                Institution = educationVM.Institution,
                Degree = educationVM.Degree,
                FieldOfStudy = educationVM.FieldOfStudy,
                StartDate = educationVM.StartDate.Date,
                EndDate = educationVM.EndDate.Date
            };
            await _db.Educations.AddAsync(education);
            await _db.SaveChangesAsync();
            return education.Id;
        }

        public async Task<int> AddInterestToCV(InterestVM interestVM)
        {
            var interest = new Interest
            {
                CVId = interestVM.CVId,
                Name = interestVM.Name,
            };
            await _db.Interests.AddAsync(interest);
            await _db.SaveChangesAsync();
            return interest.Id;
        }

        public async Task<int> AddLanguageToCV(LanguageVM languageVM)
        {
            var language = new Language
            {
                CVId = languageVM.CVId,
                Name = languageVM.Name,
                ProficiencyLevel = languageVM.ProficiencyLevel,
            };
            await _db.Languages.AddAsync(language);
            await _db.SaveChangesAsync();
            return language.Id;
        }

        public async Task<int> AddProjectToCV(ProjectVM projectVM)
        {
            var project = new Project
            {
                CVId = projectVM.CVId,
                Title = projectVM.Title,
                Description = projectVM.Description,
                StartDate = projectVM.StartDate.Date,
                EndDate = projectVM.EndDate.Date
            };
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();
            return project.Id;
        }

        public async Task<int> AddSkillToCV(SkillVM skillVM)
        {
            var skill = new Skill
            {
                CVId = skillVM.CVId,
                Name = skillVM.Name,
            };
            await _db.Skills.AddAsync(skill);
            await _db.SaveChangesAsync();
            return skill.Id;
        }

        public async Task<int> AddWorkExperienceToCV(WorkExperienceVM workExperienceVM)
        {
            var workExperience = new WorkExperience
            {
                CVId = workExperienceVM.CVId,
                Company = workExperienceVM.Company,
                Position = workExperienceVM.Position,
                Description = workExperienceVM.Description,
                StartDate = workExperienceVM.StartDate.Date,
                EndDate = workExperienceVM.EndDate.Date
            };
            await _db.WorkExperiences.AddAsync(workExperience);
            await _db.SaveChangesAsync();
            return workExperience.Id;
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
                DateOfBirth = (DateTime)cVCreateVM.DateOfBirth,
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

        public async Task EditCertification(CertificationVM certificationVM)
        {
            var certification = await _db.Certifications.FindAsync(certificationVM.Id);
            if (certification == null) return;

            certification.Name = certificationVM.Name;
            certification.IssueDate = certificationVM.IssueDate;
            certification.IssuingOrganization = certificationVM.IssuingOrganization;

            _db.Certifications.Update(certification);
            await _db.SaveChangesAsync();

        }

        public async Task EditCV(CVEditVM vm)
        {
            var cv = await GetCVByStudentId(vm.UserId);

            if(cv == null)
                return;

            cv.Title = vm.Title;
            cv.Address = vm.Address;
            cv.Phone = vm.Phone;
            cv.DateOfBirth = (DateTime)vm.DateOfBirth;

            _db.Update(cv);
            await _db.SaveChangesAsync();
        }

        public async Task EditEducation(EducationVM educationVM)
        {
            var education = await _db.Educations.FindAsync(educationVM.Id);
            if (education == null) return;

            education.Institution = educationVM.Institution;
            education.Degree = educationVM.Degree;
            education.FieldOfStudy = educationVM.FieldOfStudy;
            education.StartDate = educationVM.StartDate.Date;
            education.EndDate = educationVM.EndDate.Date;

            _db.Educations.Update(education);
            await _db.SaveChangesAsync();
        }

        public async Task EditInterest(InterestVM interestVM)
        {
            var interest = await _db.Interests.FindAsync(interestVM.Id);
            if (interest == null) return;

            interest.Name = interestVM.Name;

            _db.Interests.Update(interest);
            await _db.SaveChangesAsync();

        }

        public async Task EditLanguage(LanguageVM languageVM)
        {
            var language = await _db.Languages.FindAsync(languageVM.Id);
            if (language == null) return;

            language.Name = languageVM.Name;
            language.ProficiencyLevel = languageVM.ProficiencyLevel;

            _db.Languages.Update(language);
            await _db.SaveChangesAsync();

        }

        public async Task EditProject(ProjectVM projectVM)
        {
            var project = await _db.Projects.FindAsync(projectVM.Id);
            if (project == null) return;

            project.Title = projectVM.Title;
            project.Description = projectVM.Description;
            project.StartDate = projectVM.StartDate.Date;
            project.EndDate = projectVM.EndDate.Date;

            _db.Projects.Update(project);
            await _db.SaveChangesAsync();

        }

        public async Task EditSkill(SkillVM skillVM)
        {
            var skill = await _db.Skills.FindAsync(skillVM.Id);
            if (skill == null) return;

            skill.Name = skillVM.Name;

            _db.Skills.Update(skill);
            await _db.SaveChangesAsync();

        }

        public async Task EditWorkExperience(WorkExperienceVM workExperienceVM)
        {
            var workExperience = await _db.WorkExperiences.FindAsync(workExperienceVM.Id);
            if (workExperience == null) return;

            workExperience.Company = workExperienceVM.Company;
            workExperience.Position = workExperienceVM.Position;
            workExperience.Description = workExperienceVM.Description;
            workExperience.StartDate = workExperienceVM.StartDate.Date;
            workExperience.EndDate = workExperienceVM.EndDate.Date;

            _db.WorkExperiences.Update(workExperience);
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
                        Id = e.Id,
                        Institution = e.Institution,
                        Degree = e.Degree,
                        FieldOfStudy = e.FieldOfStudy,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate
                    }).ToList(),
                    workexperiences = c.WorkExperiences.Select(e => new WorkExperienceVM
                    {
                        Id = e.Id,
                        Company = e.Company,
                        Position = e.Position,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate,
                        Description = e.Description,
                    }).ToList(),
                    skills = c.Skills.Select(e => new SkillVM
                    {
                        Id = e.Id,
                        Name = e.Name
                    }).ToList(),
                    projects = c.Projects.Select(e => new ProjectVM
                    {
                        Id = e.Id,
                        Title = e.Title,
                        Description = e.Description,
                        StartDate = e.StartDate,
                        EndDate = e.EndDate
                    }).ToList(),
                    certifications = c.Certifications.Select(e => new CertificationVM
                    {
                        Id = e.Id,
                        Name = e.Name,
                        IssueDate = e.IssueDate,
                        IssuingOrganization = e.IssuingOrganization,
                    }).ToList(),
                    languages = c.Languages.Select(e => new LanguageVM
                    {
                        Id = e.Id,
                        Name = e.Name,
                        ProficiencyLevel = e.ProficiencyLevel
                    }).ToList(),
                    interests = c.Interests.Select(e => new InterestVM
                    {
                        Id = e.Id,
                        Name = e.Name
                    }).ToList(),
                })
                .FirstOrDefaultAsync();
            return cvDetails;
        }

        public async Task<IPagedList<CV>?> ListCVs(string searchString,  int page, int pageSize)
        {
            IQueryable<CV> filteredCvs = _db.CVs.AsNoTracking();

            if (!searchString.IsNullOrEmpty())
                filteredCvs = filteredCvs.Where(cv => 
                         cv.FirstName.Contains(searchString) ||
                         cv.LastName.Contains(searchString) ||
                         cv.Title.Contains(searchString) ||
                         cv.Educations.Any(e => e.Degree.Contains(searchString) || e.Institution.Contains(searchString) || e.FieldOfStudy.Contains(searchString)) ||
                         cv.WorkExperiences.Any(we => we.Position.Contains(searchString) || we.Company.Contains(searchString)) ||
                         cv.Interests.Any(i => i.Name.Contains(searchString)) ||
                         cv.Certifications.Any(c => c.Name.Contains(searchString) || c.IssuingOrganization.Contains(searchString)) ||
                         cv.Languages.Any(l => l.Name.Contains(searchString)) ||
                         cv.Projects.Any(p => p.Title.Contains(searchString)) ||
                         cv.Skills.Any(s => s.Name.Contains(searchString)));

            var CVs = await filteredCvs.ToPagedListAsync(page, pageSize);
            return CVs;
        }
    }
}
