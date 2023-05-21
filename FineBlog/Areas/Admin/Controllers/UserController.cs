using Microsoft.AspNetCore.Mvc;

namespace FineBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        // before page address : https://localhost:7189/admin/user/login/
        [HttpGet("Login")] // now address: https://localhost:7189/login
        public IActionResult Login()
        {
            return View();
        }
    }
}
