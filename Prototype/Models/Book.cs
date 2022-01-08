using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prototype.Models
{
    public class Book
    {
        [Key]
        public string ISBN { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public string Description { get; set; }

        public string Genre { get; set; }

    }
}
