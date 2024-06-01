using CVManagerapp.Data;
using CVManagerapp.Interfaces;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICVService _cvService;

        public CVController(UserManager<ApplicationUser> userManager,
            ICVService cvService)
        {
            _userManager = userManager;
            _cvService = cvService;
        }

        public async Task<IActionResult> ListCVs(string? searchString, int page = 1)
        {
            const int PageSize = 5;
            var CVs = await _cvService.ListCVs(searchString, page, PageSize);            
            var ViewModel = new CVListVM(CVs, searchString);

            return View(ViewModel);
        }

        public async Task<IActionResult> Create(string studentId)
        {
            if (studentId.IsNullOrEmpty())
                return BadRequest(ModelState);

            var cv = await _cvService.GetCVByStudentId(studentId);

            if (cv != null)
            {
                TempData["error"] = "This Student already has a CV";
                return RedirectToAction("ListStudents", "Admin");
            }

            var vm = new CVCreateVM
            {
                UserId = studentId
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

            await _cvService.CreateCV(cVCreateVM, user);
            
            return RedirectToAction("ListStudents", "Admin");

        }
        //todo add edit functionality
        //todo add language list for languages and add issuing organization field

        public async Task<IActionResult> Details(string studentId)
        {            
            var cvDetails = await _cvService.GetCVDetailsByStudentId(studentId);

            if (cvDetails == null)
            {
                TempData["error"] = "No CV found for this student";
                return RedirectToAction("ListStudents", "Admin");
            }        

            return View(cvDetails);
        }

        public async Task<IActionResult> Delete(string studentId)
        {
            if (studentId.IsNullOrEmpty())
                return BadRequest();

            var cv = await _cvService.GetCVByStudentId(studentId);

            if (cv == null)
                return NotFound();

            await _cvService.DeleteCV(cv);
            return RedirectToAction("ListCVs");
        }

        [HttpPost]
        public async Task<JsonResult> AddEducation(EducationVM model)
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
                await  _cvService.AddEducationToCV(model);

                return Json(new { success = true, message = "Education added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding education: " + ex.Message });
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddWorkExperience(WorkExperienceVM model)
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
                await _cvService.AddWorkExperienceToCV(model);

                return Json(new { success = true, message = "Work Experience added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding Work Experience: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddSkill(SkillVM model)
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
                await _cvService.AddSkillToCV(model);

                return Json(new { success = true, message = "Skill added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding skill: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddProject(ProjectVM model)
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
                await _cvService.AddProjectToCV(model);

                return Json(new { success = true, message = "Project added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding project: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddCertification(CertificationVM model)
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
                await _cvService.AddCertificationToCV(model);

                return Json(new { success = true, message = "Certification added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding Certification: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddLanguage(LanguageVM model)
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
                await _cvService.AddLanguageToCV(model);

                return Json(new { success = true, message = "Language added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding Language: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> AddInterest(InterestVM model)
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
                await _cvService.AddInterestToCV(model);

                return Json(new { success = true, message = "Interest added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding Interest: " + ex.Message });
            }
        }


    }
}
    
