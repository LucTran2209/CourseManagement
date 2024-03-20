using Microsoft.AspNetCore.Mvc;

namespace CMS_MVC.Controllers
{
    public class ContentCourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
