using Microsoft.AspNetCore.Mvc;
using Prototype.Data;
using Prototype.Models;
using System.Collections.Generic;

namespace Prototype.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CatalogController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Book> bookList = _db.Books;
            return View(bookList);
        }
    }
}
