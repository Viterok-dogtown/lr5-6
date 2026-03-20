using LibraryDataModule;
using System;
namespace DataModuleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем сервис
            var service = new DataService();

            // Проверяем получение книг
            Console.WriteLine("=== Все книги ===");
            foreach (var book in service.GetAllBooks())
            {
                Console.WriteLine($"{book.Id}: {book.Title} - {book.Author} (доступна: {book.IsAvailable})");
            }

            // Проверяем топ книг
            Console.WriteLine("\n=== Топ книг ===");
            foreach (var book in service.GetTopBooks(3))
            {
                Console.WriteLine($"{book.Title} - выдана {book.TimesIssued} раз");
            }

            // Проверяем выдачу книги
            Console.WriteLine("\n=== Выдача книги ===");
            bool issued = service.IssueBook(1, "Тестовый читатель");
            Console.WriteLine($"Результат выдачи: {issued}");

            // Проверяем генерацию отчета
            Console.WriteLine("\n=== Генерация отчета ===");
            string reportPath = service.GenerateFineReport();
            Console.WriteLine($"Отчет сохранен: {reportPath}");

            Console.ReadLine();
        }
    }
}