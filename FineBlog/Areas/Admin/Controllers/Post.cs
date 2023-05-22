using Microsoft.AspNetCore.Mvc;

namespace FineBlog.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class Post : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
