using CVManagerapp.Data;
using CVManagerapp.Models;
using CVManagerapp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CVManagerapp.Controllers
{
    public class CVController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CVController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> ListCVs()
        {
            var CVs = await _db.CVs
                .ToListAsync();            

            var ViewModel = new CVListVM(CVs);

            return View(ViewModel);
        }

        public async Task<IActionResult> Create(string userId)
        {
            if (userId.IsNullOrEmpty())
                return BadRequest(ModelState);

            var cv = await _db.CVs.SingleOrDefaultAsync(x => x.UserId == userId);
            
            if (cv != null)
                return BadRequest("already exists");

            var vm = new CVCreateVM
            {
                UserId = userId
            };
            return View(vm);
        }


        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CVCreateVM cVCreateVM)
        {
            if (cVCreateVM.UserId.IsNullOrEmpty())
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(cVCreateVM.UserId);
            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(cVCreateVM);

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

            return RedirectToAction("Details", new { userId = cVCreateVM.UserId });

        }


        public async Task<IActionResult> Details(string userId)
        {
            // Retrieve CV details and associated models from the database
            var cvDetails = _db.CVs
                .Where(c => c.UserId == userId)
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
                    projects= c.Projects.Select(e => new ProjectVM
                    {
                        Title = e.Title,
                        Description = e.Description,
                        StartDate= e.StartDate, 
                        EndDate= e.EndDate
                    }).ToList(),
                    certifications = c.Certifications.Select(e => new CertificationVM
                    {
                        Name= e.Name,
                        IssueDate= e.IssueDate,
                        IssuingOrganization= e.IssuingOrganization,
                    }).ToList(),
                    languages = c.Languages.Select(e => new LanguageVM
                    {
                        Name = e.Name,
                        ProficiencyLevel= e.ProficiencyLevel
                    }).ToList(),
                    interests = c.Interests.Select(e => new InterestVM
                    {
                        Name = e.Name
                    }).ToList(),
                })
                .FirstOrDefault();

            if (cvDetails == null)
            {
                return NotFound();
            }

            return View(cvDetails);
        }

        public async Task<IActionResult> Delete(string userId)
        {
            if (userId.IsNullOrEmpty())
                return BadRequest();

            var cv = await _db.CVs.SingleAsync(c => c.UserId == userId);

            if (cv == null)
                return NotFound();

            _db.CVs.Remove(cv);
            await _db.SaveChangesAsync();
            return RedirectToAction("ListCVs");
        }

        [HttpPost]
        public JsonResult AddEducation(EducationVM model)
        {  
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return Json(new { success = false, errors = errors });
            }            

            try
            {               
                var education = new Education
                {
                    CVId = model.CVId,
                    Institution = model.Institution,
                    Degree = model.Degree,
                    FieldOfStudy = model.FieldOfStudy,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };
                _db.Educations.Add(education);
                _db.SaveChanges();

                return Json(new { success = true, message = "Education added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding education: " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult AddWorkExperience(WorkExperienceVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return Json(new { success = false, errors = errors });
            }

            try
            {
                var workExperience = new WorkExperience
                {
                    CVId = model.CVId,
                    Company = model.Company,
                    Position = model.Position,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };
                _db.WorkExperiences.Add(workExperience);
                _db.SaveChanges();

                return Json(new { success = true, message = "Work Experience added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding Work Experience: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AddSkill(SkillVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return Json(new { success = false, errors = errors });
            }

            try
            {
                var skill = new Skill
                {
                    CVId = model.CVId,
                    Name= model.Name,
                };
                _db.Skills.Add(skill);
                _db.SaveChanges();

                return Json(new { success = true, message = "Skill added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding skill: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AddProject(ProjectVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return Json(new { success = false, errors = errors });
            }

            try
            {
                var project = new Project
                {
                    CVId = model.CVId,
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate
                };
                _db.Projects.Add(project);
                _db.SaveChanges();

                return Json(new { success = true, message = "Project added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding project: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AddCertification(CertificationVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return Json(new { success = false, errors = errors });
            }

            try
            {
                var certification = new Certification
                {
                    CVId = model.CVId,
                    Name = model.Name,
                    IssueDate = model.IssueDate,
                    IssuingOrganization = model.IssuingOrganization,
                };
                _db.Certifications.Add(certification);
                _db.SaveChanges();

                return Json(new { success = true, message = "Certification added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding Certification: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AddLanguage(LanguageVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return Json(new { success = false, errors = errors });
            }

            try
            {
                var language = new Language
                {
                    CVId = model.CVId,
                    Name = model.Name,
                    ProficiencyLevel = model.ProficiencyLevel,
                };
                _db.Languages.Add(language);
                _db.SaveChanges();

                return Json(new { success = true, message = "Language added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding Language: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AddInterest(InterestVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return Json(new { success = false, errors = errors });
            }

            try
            {
                var interest = new Interest
                {
                    CVId = model.CVId,
                    Name = model.Name,
                };
                _db.Interests.Add(interest);
                _db.SaveChanges();

                return Json(new { success = true, message = "Interest added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding Interest: " + ex.Message });
            }
        }


    }
}
    
