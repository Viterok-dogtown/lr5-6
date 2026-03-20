using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryDataModule.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int TimesIssued { get; set; }
    }
}

