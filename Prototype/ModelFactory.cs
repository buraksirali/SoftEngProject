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

        public IEnumerable<Book> GetBooks()
        {
            return _db.Books;
        }

        public Book GetBook(int id)
        {
            return GetBooks().Where(x => x.ID == id).First();
        }
    }
}
