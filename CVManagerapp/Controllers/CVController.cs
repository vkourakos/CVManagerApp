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

        public IActionResult Create(string userId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string userId, CVCreateVM cVCreateVM)
        {
            if (userId.IsNullOrEmpty())
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(cVCreateVM);


            return View(cVCreateVM);
        }
    }
}
