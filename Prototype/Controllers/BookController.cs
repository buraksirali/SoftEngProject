using Microsoft.AspNetCore.Mvc;
using Prototype.Data;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using System;
using System.IO;
using Prototype.Models;

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
            return View();
        }

        public IActionResult Read(int id)
        {
            Book book = modelFactory.GetBook(id);

            string path = Directory.GetCurrentDirectory() + @"\PdfFiles\" +  book.PdfName + @".pdf";

            StringBuilder sb = new StringBuilder();
            using (PdfReader reader = new PdfReader(path))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                    sb.Append(text);
                }
            }

            ViewBag.book = sb.ToString();

            Console.WriteLine(sb.ToString());

            return View(modelFactory.GetBook(id));
        }
    }
}
