using System.Collections.Generic;

namespace LibraryDataModule.Models
{
    public class LibraryData
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Reader> Readers { get; set; } = new List<Reader>();
        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}