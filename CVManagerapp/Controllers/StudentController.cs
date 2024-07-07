using CVManagerapp.Interfaces;
using CVManagerapp.Models;
using CVManagerapp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVManagerapp.Controllers
{
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICVService _cvService;

        public StudentController(UserManager<ApplicationUser> userManager, ICVService cvService)
        {
            _userManager = userManager;
            _cvService = cvService;
        }
        [Authorize(Roles = "student")]
        public async Task<IActionResult> MyCV()
        {
            var user = await _userManager.GetUserAsync(User);
            var cvDetails = await _cvService.GetCVDetailsByStudentId(user.Id);
            if (cvDetails == null)
            {
                TempData["error"] = "No CV found for this student";
                return RedirectToAction("Index", "Home");
            }
            return View("~/Views/CV/Details.cshtml", cvDetails);

        }
    }
}
