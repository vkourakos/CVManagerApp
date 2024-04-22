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
                DateOfBirth = cVCreateVM.DateOfBirth,
                Address = cVCreateVM.Address,
                Phone = cVCreateVM.Phone
            };

            await _db.CVs.AddAsync(cv);
            await _db.SaveChangesAsync();

            return RedirectToAction("ListCVs");
        }
    }
}
