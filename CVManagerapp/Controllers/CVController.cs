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
                    workexperiences = c.WorkExperiences.Select(e => new WorkexperienceVM
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
       


    }
}
    
