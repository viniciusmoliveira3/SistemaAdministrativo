using Microsoft.AspNetCore.Mvc;

namespace Colex.Controllers
{
    public class SairController : Controller
    {
        public IActionResult Index()
        {
            TempData.Clear();
            HttpContext.Session.Clear();
            return Redirect("/login/login");
        }
    }
}
