using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LibraryDataModule.Models;

namespace LibraryDataModule
{
    public class JsonDataProvider
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _options;

        public JsonDataProvider(string filePath = "library_data.json")
        {
            _filePath = filePath;
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };
        }

 
        public LibraryData LoadData()
        {
            try
            {
                if (!File.Exists(_filePath))
                {

                    return CreateDefaultData();
                }

                string json = File.ReadAllText(_filePath);
                var data = JsonSerializer.Deserialize<LibraryData>(json, _options);

                return data ?? CreateDefaultData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки данных: {ex.Message}");
                return CreateDefaultData();
            }
        }

 
        public void SaveData(LibraryData data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, _options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения данных: {ex.Message}");
                throw;
            }
        }

        private LibraryData CreateDefaultData()
        {
            var data = new LibraryData
            {
                Books = new List<Book>
                {
                    new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой", Year = 1869, IsAvailable = true, TimesIssued = 15 },
                    new Book { Id = 2, Title = "Преступление и наказание", Author = "Федор Достоевский", Year = 1866, IsAvailable = true, TimesIssued = 12 },
                    new Book { Id = 3, Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Year = 1967, IsAvailable = true, TimesIssued = 20 },
                    new Book { Id = 4, Title = "Евгений Онегин", Author = "Александр Пушкин", Year = 1833, IsAvailable = true, TimesIssued = 8 },
                    new Book { Id = 5, Title = "Мертвые души", Author = "Николай Гоголь", Year = 1842, IsAvailable = true, TimesIssued = 5 },
                    new Book { Id = 6, Title = "Идиот", Author = "Федор Достоевский", Year = 1869, IsAvailable = true, TimesIssued = 7 },
                    new Book { Id = 7, Title = "Анна Каренина", Author = "Лев Толстой", Year = 1877, IsAvailable = true, TimesIssued = 10 }
                },
                Readers = new List<Reader>
                {
                    new Reader { Id = 1, FullName = "Иванов Иван Иванович", Phone = "+7(123)456-78-90" },
                    new Reader { Id = 2, FullName = "Петров Петр Петрович", Phone = "+7(098)765-43-21" }
                },
                Issues = new List<Issue>()
            };


            SaveData(data);
            return data;
        }
    }
}