using CodeExam.Dal;
using CodeExam.Models;
using CodeExam.ModelViews.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CodeExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeVm vm = new HomeVm();
            vm.Setting = await _context.Settings.FirstOrDefaultAsync();
            vm.chefs = await _context.Chefs.ToListAsync();
            return View(vm);
        }

       
    }
}
