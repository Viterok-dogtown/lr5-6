using System;

namespace LibraryUIModule.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string ReaderName { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public bool IsReturned => ActualReturnDate.HasValue;

        public string DisplayText => $"{IssueDate:d} - {ReaderName}";
    }
}