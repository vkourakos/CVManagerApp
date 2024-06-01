using CVManagerapp.Data;
using CVManagerapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CVManagerapp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> ListStudents(int page = 1)
        {
            const int pageSize = 10;

           var students = await _userManager.GetUsersInRoleAsync(UserRoles.Student);
            var pagedStudents = await students.ToPagedListAsync(page, pageSize);
            return View(pagedStudents);
        }
    }
}
