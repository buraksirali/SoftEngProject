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

        public IActionResult Detail(int id)
        {
            return View(modelFactory.GetBook(id));
        }
    }
}
