using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using LibraryDataModule.Models;

namespace LibraryDataModule
{
    public class ExcelReportGenerator
    {
        private readonly string _reportsFolder;

        public ExcelReportGenerator(string reportsFolder = "Reports")
        {
            _reportsFolder = reportsFolder;

            // Создаем папку для отчетов, если её нет
            if (!Directory.Exists(_reportsFolder))
            {
                Directory.CreateDirectory(_reportsFolder);
            }
        }

        /// <summary>
        /// Генерация отчета по штрафам
        /// </summary>
        public string GenerateFineReport(List<Issue> issues, List<Book> books)
        {
            string fileName = $"FineReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string filePath = Path.Combine(_reportsFolder, fileName);

            // Устанавливаем лицензию для EPPlus (в бесплатной версии)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Создаем лист
                var worksheet = package.Workbook.Worksheets.Add("Штрафы");

                // Заголовки
                worksheet.Cells[1, 1].Value = "№";
                worksheet.Cells[1, 2].Value = "Книга";
                worksheet.Cells[1, 3].Value = "Читатель";
                worksheet.Cells[1, 4].Value = "Дата выдачи";
                worksheet.Cells[1, 5].Value = "План. возврат";
                worksheet.Cells[1, 6].Value = "Дней просрочки";
                worksheet.Cells[1, 7].Value = "Штраф (руб)";

                // Форматирование заголовков
                using (var range = worksheet.Cells[1, 1, 1, 7])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                int row = 2;
                int totalFines = 0;

                // Фильтруем только просроченные и невозвращенные книги
                var overdueIssues = issues.Where(i =>
                    !i.IsReturned && i.ReturnDate < DateTime.Now).ToList();

                foreach (var issue in overdueIssues)
                {
                    var book = books.FirstOrDefault(b => b.Id == issue.BookId);
                    string bookTitle = book?.Title ?? "Неизвестная книга";

                    int daysOverdue = (DateTime.Now - issue.ReturnDate).Days;
                    int fine = daysOverdue * 10; // 10 рублей в день

                    worksheet.Cells[row, 1].Value = row - 1;
                    worksheet.Cells[row, 2].Value = bookTitle;
                    worksheet.Cells[row, 3].Value = issue.ReaderName;
                    worksheet.Cells[row, 4].Value = issue.IssueDate.ToShortDateString();
                    worksheet.Cells[row, 5].Value = issue.ReturnDate.ToShortDateString();
                    worksheet.Cells[row, 6].Value = daysOverdue;
                    worksheet.Cells[row, 7].Value = fine;

                    // Выделяем просрочку красным
                    worksheet.Cells[row, 6, row, 7].Style.Font.Color.SetColor(System.Drawing.Color.Red);

                    totalFines += fine;
                    row++;
                }

                // Итог
                worksheet.Cells[row, 6].Value = "ИТОГО:";
                worksheet.Cells[row, 6].Style.Font.Bold = true;
                worksheet.Cells[row, 7].Value = totalFines;
                worksheet.Cells[row, 7].Style.Font.Bold = true;

                // Автоподбор ширины колонок
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Сохраняем файл
                package.SaveAs(new FileInfo(filePath));
            }

            return filePath;
        }

        /// <summary>
        /// Генерация отчета по популярности книг
        /// </summary>
        public string GeneratePopularityReport(List<Book> books)
        {
            string fileName = $"PopularityReport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            string filePath = Path.Combine(_reportsFolder, fileName);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Популярность книг");

                // Заголовки
                worksheet.Cells[1, 1].Value = "№";
                worksheet.Cells[1, 2].Value = "Название";
                worksheet.Cells[1, 3].Value = "Автор";
                worksheet.Cells[1, 4].Value = "Год";
                worksheet.Cells[1, 5].Value = "Кол-во выдач";
                worksheet.Cells[1, 6].Value = "Статус";

                // Форматирование заголовков
                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                }

                int row = 2;
                foreach (var book in books.OrderByDescending(b => b.TimesIssued))
                {
                    worksheet.Cells[row, 1].Value = row - 1;
                    worksheet.Cells[row, 2].Value = book.Title;
                    worksheet.Cells[row, 3].Value = book.Author;
                    worksheet.Cells[row, 4].Value = book.Year;
                    worksheet.Cells[row, 5].Value = book.TimesIssued;
                    worksheet.Cells[row, 6].Value = book.IsAvailable ? "Доступна" : "Выдана";

                    row++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                package.SaveAs(new FileInfo(filePath));
            }

            return filePath;
        }
    }
}