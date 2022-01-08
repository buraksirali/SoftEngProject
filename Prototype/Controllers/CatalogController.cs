using Microsoft.AspNetCore.Mvc;
using Prototype.Data;
using Prototype.Models;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ModelFactory modelFactory;

        public CatalogController(ApplicationDbContext db)
        {
            _db = db;
            modelFactory = new ModelFactory(_db);
        }

        public IActionResult Index()
        {
            return View(modelFactory.GetBooks());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {
            if (obj == null)
            {
                return BadRequest("Given book doesn't have enough information.");
            }

            modelFactory.AddBook(obj);

            return RedirectToAction("Success", obj);
        }

        public IActionResult Success(Book obj)
        {
            return View(obj);
        }


        public IActionResult Detail(int id)
        {
            return View(modelFactory.GetBook(id));
        }
    }
}
