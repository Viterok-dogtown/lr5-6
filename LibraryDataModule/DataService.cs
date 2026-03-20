using System;
using System.Collections.Generic;
using System.Linq;
using LibraryDataModule.Models;

namespace LibraryDataModule
{
    public class DataService
    {
        private readonly JsonDataProvider _dataProvider;
        private readonly ExcelReportGenerator _reportGenerator;

        private LibraryData _libraryData;

        // События для уведомления об изменениях
        public event EventHandler DataChanged;

        public DataService()
        {
            _dataProvider = new JsonDataProvider();
            _reportGenerator = new ExcelReportGenerator();

            // Загружаем данные
            _libraryData = _dataProvider.LoadData();
        }

        // Публичные свойства для доступа к данным
        public List<Book> Books => _libraryData.Books;
        public List<Reader> Readers => _libraryData.Readers;
        public List<Issue> Issues => _libraryData.Issues;

        #region Работа с книгами

        /// <summary>
        /// Получить все книги
        /// </summary>
        public List<Book> GetAllBooks()
        {
            return _libraryData.Books.ToList();
        }

        /// <summary>
        /// Получить доступные книги
        /// </summary>
        public List<Book> GetAvailableBooks()
        {
            return _libraryData.Books.Where(b => b.IsAvailable).ToList();
        }

        /// <summary>
        /// Получить топ N книг по выдачам
        /// </summary>
        public List<Book> GetTopBooks(int count)
        {
            return _libraryData.Books
                .OrderByDescending(b => b.TimesIssued)
                .Take(count)
                .ToList();
        }

        /// <summary>
        /// Добавить новую книгу
        /// </summary>
        public void AddBook(Book book)
        {
            book.Id = _libraryData.Books.Count > 0
                ? _libraryData.Books.Max(b => b.Id) + 1
                : 1;

            _libraryData.Books.Add(book);
            SaveChanges();
        }

        /// <summary>
        /// Обновить информацию о книге
        /// </summary>
        public void UpdateBook(Book updatedBook)
        {
            var book = _libraryData.Books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (book != null)
            {
                book.Title = updatedBook.Title;
                book.Author = updatedBook.Author;
                book.Year = updatedBook.Year;
                SaveChanges();
            }
        }

        /// <summary>
        /// Удалить книгу
        /// </summary>
        public bool DeleteBook(int bookId)
        {
            var book = _libraryData.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                // Проверяем, не выдана ли книга
                if (!book.IsAvailable)
                {
                    return false; // Нельзя удалить выданную книгу
                }

                _libraryData.Books.Remove(book);
                SaveChanges();
                return true;
            }
            return false;
        }

        #endregion

        #region Работа с выдачей книг

        /// <summary>
        /// Выдать книгу читателю
        /// </summary>
        public bool IssueBook(int bookId, string readerName, int returnDays = 14)
        {
            var book = _libraryData.Books.FirstOrDefault(b => b.Id == bookId);

            if (book == null || !book.IsAvailable)
            {
                return false; // Книга не найдена или недоступна
            }

            // Обновляем статус книги
            book.IsAvailable = false;
            book.TimesIssued++;

            // Создаем запись о выдаче
            var issue = new Issue
            {
                Id = _libraryData.Issues.Count > 0
                    ? _libraryData.Issues.Max(i => i.Id) + 1
                    : 1,
                BookId = bookId,
                ReaderName = readerName,
                IssueDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(returnDays),
                ActualReturnDate = null
            };

            _libraryData.Issues.Add(issue);
            SaveChanges();

            return true;
        }

        /// <summary>
        /// Вернуть книгу
        /// </summary>
        public bool ReturnBook(int bookId)
        {
            var book = _libraryData.Books.FirstOrDefault(b => b.Id == bookId);
            var issue = _libraryData.Issues
                .Where(i => i.BookId == bookId && !i.IsReturned)
                .OrderByDescending(i => i.IssueDate)
                .FirstOrDefault();

            if (book == null || issue == null)
            {
                return false;
            }

            // Обновляем статус книги
            book.IsAvailable = true;

            // Обновляем запись о выдаче
            issue.ActualReturnDate = DateTime.Now;

            SaveChanges();
            return true;
        }

        /// <summary>
        /// Получить историю выдач книги
        /// </summary>
        public List<Issue> GetBookHistory(int bookId)
        {
            return _libraryData.Issues
                .Where(i => i.BookId == bookId)
                .OrderByDescending(i => i.IssueDate)
                .ToList();
        }

        /// <summary>
        /// Получить текущие выдачи читателя
        /// </summary>
        public List<Issue> GetReaderCurrentIssues(string readerName)
        {
            return _libraryData.Issues
                .Where(i => i.ReaderName == readerName && !i.IsReturned)
                .ToList();
        }

        #endregion

        #region Отчеты и аналитика

        /// <summary>
        /// Сгенерировать отчет по штрафам
        /// </summary>
        public string GenerateFineReport()
        {
            return _reportGenerator.GenerateFineReport(_libraryData.Issues, _libraryData.Books);
        }

        /// <summary>
        /// Сгенерировать отчет по популярности книг
        /// </summary>
        public string GeneratePopularityReport()
        {
            return _reportGenerator.GeneratePopularityReport(_libraryData.Books);
        }

        /// <summary>
        /// Получить статистику по библиотеке
        /// </summary>
        public (int totalBooks, int availableBooks, int issuedBooks, int overdueBooks) GetStatistics()
        {
            int totalBooks = _libraryData.Books.Count;
            int availableBooks = _libraryData.Books.Count(b => b.IsAvailable);
            int issuedBooks = _libraryData.Books.Count(b => !b.IsAvailable);

            int overdueBooks = _libraryData.Issues
                .Count(i => !i.IsReturned && i.ReturnDate < DateTime.Now);

            return (totalBooks, availableBooks, issuedBooks, overdueBooks);
        }

        /// <summary>
        /// Получить список просроченных выдач
        /// </summary>
        public List<Issue> GetOverdueIssues()
        {
            return _libraryData.Issues
                .Where(i => !i.IsReturned && i.ReturnDate < DateTime.Now)
                .OrderBy(i => i.ReturnDate)
                .ToList();
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        private void SaveChanges()
        {
            _dataProvider.SaveData(_libraryData);
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Поиск книг по названию или автору
        /// </summary>
        public List<Book> SearchBooks(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return GetAllBooks();

            searchText = searchText.ToLower();
            return _libraryData.Books
                .Where(b => b.Title.ToLower().Contains(searchText) ||
                           b.Author.ToLower().Contains(searchText))
                .ToList();
        }

        #endregion
    }
}