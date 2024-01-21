using CodeExam.Areas.admin.ViewModel.Chef;
using CodeExam.Dal;
using CodeExam.Helpers;
using CodeExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeExam.Areas.admin.Controllers
{
    [Area("Admin")]

    [Authorize(Roles = "Admin,Moderator")]

    public class ChefController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ChefController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ICollection<Chef> chefList = _appDbContext.Chefs.ToList();
            return View(chefList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateChefVm createChefVm)
        {
            if(!ModelState.IsValid)
            {
                return View(createChefVm);
            }
            if (!createChefVm.Image.CheckType())
            {
                ModelState.AddModelError("Image", "Image must be in image format");
                return View(createChefVm);
            }
            if (!createChefVm.Image.CheckLength())
            {
                ModelState.AddModelError("Image", "Image size can not more 2 mb");
                return View(createChefVm);
            }
            Chef chef = new Chef()
            {
                Fullname = createChefVm.Fullname,
                Description= createChefVm.Description,
                CreatedTime=DateTime.Now,
                UpdatedTime=DateTime.Now,
                ImageUrl= createChefVm.Image.Upload(_webHostEnvironment.WebRootPath,@"\Upload\Chef\")
            };
            await _appDbContext.Chefs.AddAsync(chef);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Chef chef=await _appDbContext.Chefs.Where(x=>x.Id==id).FirstOrDefaultAsync();
             _appDbContext.Chefs.Remove(chef);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Chef chef=await _appDbContext.Chefs.Where(x=>x.Id == id).FirstOrDefaultAsync();
            UpdateChefVm updateChefVm = new UpdateChefVm()
            {
                Fullname = chef.Fullname, 
                Description=chef.Description,
                ImageUrl=chef.ImageUrl,
                Id=id
            };
            return View(updateChefVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateChefVm updateChefVm)
        {
            if (!ModelState.IsValid)
            {
                return View(updateChefVm);
            }
            Chef chef = await _appDbContext.Chefs.Where(x=>x.Id==updateChefVm.Id).FirstOrDefaultAsync();
            chef.Fullname = updateChefVm.Fullname;
            chef.Description = updateChefVm.Description;
            chef.ImageUrl = updateChefVm.Image.Upload(_webHostEnvironment.WebRootPath, @"\Upload\Chef\");
            chef.UpdatedTime = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            Chef chef =await _appDbContext.Chefs.Where(x=>x.Id==id).FirstOrDefaultAsync();
            return View(chef);
        }
    }
}
