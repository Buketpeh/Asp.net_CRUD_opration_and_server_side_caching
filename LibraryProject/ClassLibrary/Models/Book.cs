using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string PublishingHouse { get; set; }
        public string Category { get; set; }
        public int? AuthorId { get; set; }
    }
}
