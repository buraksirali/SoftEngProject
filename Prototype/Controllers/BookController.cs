using Microsoft.AspNetCore.Mvc;
using Prototype.Data;
using System.Collections.Generic;
using System.Linq;
using Prototype.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Prototype.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ModelFactory modelFactory;
        private IEnumerable<string> Pages;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
            modelFactory = new ModelFactory(_db);
        }

        public IActionResult Index(int id)
        {
            Pages = modelFactory.GetPages(modelFactory.GetBook(id));
            HttpContext.Session.SetString("Pages", JsonConvert.SerializeObject(Pages));
            HttpContext.Session.SetString("PageNumber", "1");

            return RedirectToAction("read");
        }

        public IActionResult Read()
        {
            Pages = JsonConvert.DeserializeObject<IEnumerable<string>>(HttpContext.Session.GetString("Pages"));
            int pageNum = int.Parse(HttpContext.Session.GetString("PageNumber"));

            if (pageNum > 0)
            {
                if (pageNum > Pages.Count())
                {
                    ViewBag.book = Pages.Last();
                }

                ViewBag.book = Pages.ElementAt(pageNum);
            } else
            {
                ViewBag.book = Pages.ElementAt(0);
            }

            return View();
        }

        public IActionResult Next()
        {
            int pageNum = int.Parse(HttpContext.Session.GetString("PageNumber"));

            HttpContext.Session.SetString("PageNumber", $"{pageNum + 1}");

            return RedirectToAction("read");
        }

        public IActionResult Previous()
        {
            int pageNum = int.Parse(HttpContext.Session.GetString("PageNumber"));

            HttpContext.Session.SetString("PageNumber", $"{pageNum - 1}");

            return RedirectToAction("read");
        }
    }
}
