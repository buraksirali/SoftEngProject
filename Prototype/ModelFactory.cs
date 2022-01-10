using Prototype.Data;
using Prototype.Models;
using System.Collections.Generic;
using System.Linq;

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
    }
}
