using Microsoft.AspNetCore.Mvc;

namespace FineBlog.Controllers
{
    public class PageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
