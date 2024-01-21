using CodeExam.Areas.admin.ViewModel.Setting;
using CodeExam.Dal;
using CodeExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CodeExam.Areas.admin.Controllers
{
    [Area("Admin")]
    

    
    [Authorize(Roles = "Admin,Moderator")]


    public class SettingController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SettingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IActionResult> Index()
        {
            
            return View(await _dbContext.Settings.ToListAsync());
        }

        public async Task<IActionResult> Edit()
        {
            if (User.IsInRole("Moderator"))
            {
                return RedirectToAction("Error","Error");
            }
            Setting setting = await _dbContext.Settings.FirstOrDefaultAsync();
            SettingVm settingVm = new SettingVm()
            {
                Email=setting.Email,
                Facebook=setting.Facebook,
                Twitter=setting.Twitter,
                Phone=setting.Phone,
                Instagram=setting.Instagram
            };
            return View(settingVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SettingVm? settingVm)
        {
            try
            {
                Setting setting = await _dbContext.Settings.FirstOrDefaultAsync();
                setting.Twitter = settingVm.Twitter;
                setting.Facebook = settingVm.Facebook;
                setting.Instagram = settingVm.Instagram;
                setting.Phone = settingVm.Phone;
                setting.Email = settingVm.Email;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return RedirectToAction("Setting", "Error");
            }
            
            return RedirectToAction("Index");

        }
    }
}
