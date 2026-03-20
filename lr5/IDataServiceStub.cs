using LibraryUIModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryUIModule
{
    /// <summary>
    /// Интерфейс сервиса данных (контракт с Разработчиком 2)
    /// </summary>
    public interface IDataService
    {
        List<Book> GetAllBooks();
        List<Book> GetAvailableBooks();
        List<Book> GetTopBooks(int count);
        bool IssueBook(int bookId, string readerName);
        bool ReturnBook(int bookId);
        List<Issue> GetBookHistory(int bookId);
        string GenerateFineReport();
    }
    public class DataServiceStub : IDataService
    {
        private List<Book> _books;
        private List<Issue> _issues;
        private int _nextIssueId = 1;

        public DataServiceStub()
        {
            _books = new List<Book>
            {
                new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой", Year = 1869, IsAvailable = true, TimesIssued = 15 },
                new Book { Id = 2, Title = "Преступление и наказание", Author = "Федор Достоевский", Year = 1866, IsAvailable = true, TimesIssued = 12 },
                new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Year = 1967, IsAvailable = true, TimesIssued = 20 },
                new Book { Id = 4, Title = "Евгений Онегин", Author = "Александр Пушкин", Year = 1833, IsAvailable = true, TimesIssued = 8 },
                new Book { Id = 5, Title = "Мертвые души", Author = "Николай Гоголь", Year = 1842, IsAvailable = false, TimesIssued = 5 },
                new Book { Id = 6, Title = "Идиот", Author = "Федор Достоевский", Year = 1869, IsAvailable = true, TimesIssued = 7 },
                new Book { Id = 7, Title = "Анна Каренина", Author = "Лев Толстой", Year = 1877, IsAvailable = true, TimesIssued = 10 }
            };

            _issues = new List<Issue>
            {
                new Issue {
                    Id = 1,
                    BookId = 5,
                    ReaderName = "Иванов И.И.",
                    IssueDate = DateTime.Now.AddDays(-10),
                    ReturnDate = DateTime.Now.AddDays(4),
                    ActualReturnDate = null
                }
            };
        }

        public List<Book> GetAllBooks() => _books.ToList();

        public List<Book> GetAvailableBooks() => _books.Where(b => b.IsAvailable).ToList();

        public List<Book> GetTopBooks(int count) =>
            _books.OrderByDescending(b => b.TimesIssued).Take(count).ToList();

        public bool IssueBook(int bookId, string readerName)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null || !book.IsAvailable)
                return false;

            book.IsAvailable = false;
            book.TimesIssued++;

            _issues.Add(new Issue
            {
                Id = _nextIssueId++,
                BookId = bookId,
                ReaderName = readerName,
                IssueDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(14),
                ActualReturnDate = null
            });

            return true;
        }

        public bool ReturnBook(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            var issue = _issues
                .Where(i => i.BookId == bookId && !i.IsReturned)
                .OrderByDescending(i => i.IssueDate)
                .FirstOrDefault();

            if (book == null || issue == null)
                return false;

            book.IsAvailable = true;
            issue.ActualReturnDate = DateTime.Now;
            return true;
        }

        public List<Issue> GetBookHistory(int bookId)
        {
            return _issues
                .Where(i => i.BookId == bookId)
                .OrderByDescending(i => i.IssueDate)
                .ToList();
        }

        public string GenerateFineReport()
        {
            string reportPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"FineReport_{DateTime.Now:yyyyMMdd}.xlsx");

            System.Windows.Forms.MessageBox.Show(
                $"Отчет сохранен: {reportPath}",
                "Заглушка",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Information);

            return reportPath;
        }
    }
}