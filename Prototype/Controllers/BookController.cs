using Microsoft.AspNetCore.Mvc;

namespace Prototype.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index(string ISBN)
        {
            
            return View();
        }
    }
}
