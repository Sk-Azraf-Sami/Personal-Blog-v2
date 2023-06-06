using AspNetCoreHero.ToastNotification.Abstractions;
using FineBlog.Data;
using FineBlog.Migrations;
using FineBlog.Models;
using FineBlog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PageController : Controller
    {
    private readonly ApplicationDbContext _context;
    private readonly INotyfService _notification;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PageController(ApplicationDbContext context, 
                            INotyfService notification, 
                            IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _notification = notification;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> About()
    {
        var aboutPage = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            var vm = new PageVM()
            {
                Id = aboutPage!.Id,
                Title = aboutPage.Title,
                ShortDescription = aboutPage.ShortDescription,
                Description = aboutPage.Description,
                ThumbnailUrl = aboutPage.ThumbnailUrl,
            };

        return View(vm);
    }

        /*---------------- About ------------------*/

        [HttpPost]
        public async Task<IActionResult> About(PageVM vm)
        {
            if(!ModelState.IsValid) { return View(vm); }
            var aboutPage = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "about");
            if(aboutPage == null)
            {
                _notification.Error("Page not found");
                return View();
            }

            aboutPage.Title = vm.Title;
            aboutPage.ShortDescription = vm.ShortDescription;
            aboutPage.Description = vm.Description;

            if(vm.Thumbnail != null)
            {
                aboutPage.ThumbnailUrl = UploadImage(vm.Thumbnail); 
            }

            await _context .SaveChangesAsync();
            _notification.Success("About page updated successfully");
            return RedirectToAction("About", "Page", new {area = "Admin"}); 
        }

        /*------------ Contact ------------------*/
        [HttpPost]
        public async Task<IActionResult> Contact(PageVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var aboutPage = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "contact");
            if (aboutPage == null)
            {
                _notification.Error("Page not found");
                return View();
            }

            aboutPage.Title = vm.Title;
            aboutPage.ShortDescription = vm.ShortDescription;
            aboutPage.Description = vm.Description;

            if (vm.Thumbnail != null)
            {
                aboutPage.ThumbnailUrl = UploadImage(vm.Thumbnail);
            }

            await _context.SaveChangesAsync();
            _notification.Success("Contact page updated successfully");
            return RedirectToAction("Contact", "Page", new { area = "Admin" });
        }

        /*-------------- privacy -------------------------*/
        [HttpPost]
        public async Task<IActionResult> Privacy(PageVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var aboutPage = await _context.Pages!.FirstOrDefaultAsync(x => x.Slug == "privacy");
            if (aboutPage == null)
            {
                _notification.Error("Page not found");
                return View();
            }

            aboutPage.Title = vm.Title;
            aboutPage.ShortDescription = vm.ShortDescription;
            aboutPage.Description = vm.Description;

            if (vm.Thumbnail != null)
            {
                aboutPage.ThumbnailUrl = UploadImage(vm.Thumbnail);
            }

            await _context.SaveChangesAsync();
            _notification.Success("Private page updated successfully");
            return RedirectToAction("Privacy", "Page", new { area = "Admin" });
        }

        private string? UploadImage(IFormFile file)
        {
            string uniqueFileName = "";
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "thumbnails");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
