using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace UniHive.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserID") == null || HttpContext.Session.GetString("Role") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
            return View("Index");
        }
    }
}
