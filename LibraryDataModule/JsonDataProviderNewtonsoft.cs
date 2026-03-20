using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using LibraryDataModule.Models;

namespace LibraryDataModule
{
    public class JsonDataProviderNewtonsoft
    {
        private readonly string _filePath;

        public JsonDataProviderNewtonsoft(string filePath = "library_data.json")
        {
            _filePath = filePath;
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
                var data = JsonConvert.DeserializeObject<LibraryData>(json);

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
                string json = JsonConvert.SerializeObject(data, Formatting.Indented);
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
                    new Book { Id = 5, Title = "Мертвые души", Author = "Николай Гоголь", Year = 1842, IsAvailable = true, TimesIssued = 5 }
                },
                Readers = new List<Reader>(),
                Issues = new List<Issue>()
            };

            SaveData(data);
            return data;
        }
    }
}