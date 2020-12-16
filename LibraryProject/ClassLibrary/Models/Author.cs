using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public string Fistrname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateofBirth { get; set; }
    }
}
