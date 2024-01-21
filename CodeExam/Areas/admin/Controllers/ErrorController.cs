using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeExam.Areas.admin.Controllers
{
    [Area("Admin")]

    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin,Moderator")]
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Setting()
        {
            return View();
        }
    }
}
