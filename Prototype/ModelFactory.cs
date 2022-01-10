using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Prototype.Data;
using Prototype.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Prototype
{
    public class ModelFactory
    {
        private readonly ApplicationDbContext _db;

        public ModelFactory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Book> GetBooks(string searching = null)
        {
            return _db.Books.Where(book => book.Name.Contains(searching) || searching == null);
        }

        public Book GetBook(int id)
        {
            return GetBooks().Where(x => x.ID == id).First();
        }

        public void AddBook(Book obj)
        {
            _db.Add(obj);
            _db.SaveChanges();
        }

        public IEnumerable<string> GetPages(Book obj)
        {
            string path = Directory.GetCurrentDirectory() + @"\PdfFiles\" + obj.PdfName + @".pdf";

            var pages = new List<string>();

            using (PdfReader reader = new(path))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(reader, page, strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                    pages.Add(text);
                }
            }

            return pages;
        }
    }
}
