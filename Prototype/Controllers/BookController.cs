using Microsoft.AspNetCore.Mvc;
using Prototype.Data;
using System.Collections.Generic;

namespace Prototype.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ModelFactory modelFactory;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
            modelFactory = new ModelFactory(_db);
        }

        public IActionResult Index(int id)
        {
            modelFactory.GetBook(id);

            // Pdf To Text
            /*Dictionary<string, string> data = new Dictionary<string, string>()
            {
                text: "",
                name: ""
            }*/

            return View();
        }
    }
}
