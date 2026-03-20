using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LibraryUIModule.Models;

namespace LibraryUIModule
{
    public partial class FormMain : Form
    {
        private IDataService _dataService;

        private Book _selectedBook;

        public FormMain()
        {
            InitializeComponent();

            // Инициализируем заглушку
            _dataService = new DataServiceStub();

            // Настройка формы
            this.Text = "Библиотечная система";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(900, 650);
            this.MinimumSize = new Size(800, 600);

            // Настройка DataGridView для каталога
            ConfigureDataGridView(dgvCatalog);
            dgvCatalog.SelectionChanged += DgvCatalog_SelectionChanged;
            dgvCatalog.CellDoubleClick += DgvCatalog_CellDoubleClick;

            ConfigureDataGridView(dgvTopBooks);

            cmbBooks.DisplayMember = "Title";
            cmbBooks.ValueMember = "Id";
            cmbBooks.DropDownStyle = ComboBoxStyle.DropDownList;

            this.Load += FormMain_Load;
            btnRefreshTop.Click += BtnRefreshTop_Click;
            btnIssue.Click += BtnIssue_Click;
            btnReturn.Click += BtnReturn_Click;

            выходStripMenuItem2.Click += (s, e) => Application.Exit();
            отчетПоШтрафамToolStripMenuItem.Click += BtnReport_Click;
            оПрограммеToolStripMenuItem.Click += AboutMenuItem_Click;

            CreateStatusStrip();
        }

        private void ConfigureDataGridView(DataGridView dgv)
        {
            dgv.AutoGenerateColumns = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        }
        private void CreateStatusStrip()
        {
            StatusStrip statusStrip = new StatusStrip();
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("Готов к работе");
            ToolStripStatusLabel dateLabel = new ToolStripStatusLabel(DateTime.Now.ToLongDateString());

            statusStrip.Items.Add(statusLabel);
            statusStrip.Items.Add(new ToolStripStatusLabel(" "));
            statusStrip.Items.Add(dateLabel);

            this.Controls.Add(statusStrip);
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void FormMain_Load(object sender, EventArgs e)
        {
            RefreshAllData();
        }

        /// <summary>
        /// Обновление всех данных
        /// </summary>
        private void RefreshAllData()
        {
            LoadCatalog();
            LoadTopBooks();
            LoadAvailableBooks();
            UpdateStatus();
        }

        /// <summary>
        /// Загрузка каталога книг
        /// </summary>
        private void LoadCatalog()
        {
            try
            {
                var books = _dataService.GetAllBooks();
                dgvCatalog.DataSource = null;
                dgvCatalog.DataSource = books;

                // Настройка названий столбцов
                if (dgvCatalog.Columns.Count > 0)
                {
                    SetColumnHeaders(dgvCatalog);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки каталога: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загрузка топа книг
        /// </summary>
        private void LoadTopBooks()
        {
            try
            {
                var topBooks = _dataService.GetTopBooks(10);
                dgvTopBooks.DataSource = null;
                dgvTopBooks.DataSource = topBooks;

                if (dgvTopBooks.Columns.Count > 0)
                {
                    SetColumnHeaders(dgvTopBooks);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки топа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загрузка доступных книг в ComboBox
        /// </summary>
        private void LoadAvailableBooks()
        {
            try
            {
                var availableBooks = _dataService.GetAvailableBooks();
                cmbBooks.DataSource = null;
                cmbBooks.DataSource = availableBooks;

                if (availableBooks.Count == 0)
                {
                    cmbBooks.Text = "Нет доступных книг";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки доступных книг: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Загрузка истории выдач выбранной книги
        /// </summary>
        private void LoadBookHistory(int bookId)
        {
            try
            {
                var history = _dataService.GetBookHistory(bookId);
                lstHistory.Items.Clear();

                if (history.Count == 0)
                {
                    lstHistory.Items.Add("История выдач отсутствует");
                }
                else
                {
                    foreach (var issue in history)
                    {
                        string status = issue.IsReturned ? "возвращена" : "не возвращена";
                        lstHistory.Items.Add($"{issue.IssueDate:d} - {issue.ReaderName} ({status})");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки истории: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновление статуса
        /// </summary>
        private void UpdateStatus()
        {
            try
            {
                int totalBooks = _dataService.GetAllBooks().Count;
                int availableBooks = _dataService.GetAvailableBooks().Count;

                this.Text = $"Библиотечная система | Всего книг: {totalBooks} | Доступно: {availableBooks}";
            }
            catch { }
        }

        /// <summary>
        /// Настройка заголовков столбцов
        /// </summary>
        private void SetColumnHeaders(DataGridView dgv)
        {
            if (dgv.Columns["Id"] != null)
                dgv.Columns["Id"].HeaderText = "ID";
            if (dgv.Columns["Title"] != null)
                dgv.Columns["Title"].HeaderText = "Название";
            if (dgv.Columns["Author"] != null)
                dgv.Columns["Author"].HeaderText = "Автор";
            if (dgv.Columns["Year"] != null)
                dgv.Columns["Year"].HeaderText = "Год";
            if (dgv.Columns["IsAvailable"] != null)
            {
                dgv.Columns["IsAvailable"].HeaderText = "Доступна";
                // Форматирование булевого значения
                dgv.Columns["IsAvailable"].DefaultCellStyle.Format = "Да;Нет";
            }
            if (dgv.Columns["TimesIssued"] != null)
                dgv.Columns["TimesIssued"].HeaderText = "Выдана раз";
        }

        /// <summary>
        /// Выбор книги в каталоге
        /// </summary>
        private void DgvCatalog_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCatalog.SelectedRows.Count > 0)
            {
                _selectedBook = dgvCatalog.SelectedRows[0].DataBoundItem as Book;
                if (_selectedBook != null)
                {
                    LoadBookHistory(_selectedBook.Id);
                }
            }
        }

        /// <summary>
        /// Двойной клик по книге в каталоге
        /// </summary>
        private void DgvCatalog_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && _selectedBook != null)
            {
                ShowBookDetails(_selectedBook);
            }
        }

        /// <summary>
        /// Показать детальную информацию о книге
        /// </summary>
        private void ShowBookDetails(Book book)
        {
            string availability = book.IsAvailable ? "Доступна" : "Выдана";
            string message = $"Название: {book.Title}\n" +
                            $"Автор: {book.Author}\n" +
                            $"Год: {book.Year}\n" +
                            $"Статус: {availability}\n" +
                            $"Всего выдач: {book.TimesIssued}";

            MessageBox.Show(message, $"Книга ID: {book.Id}",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обновление топа
        /// </summary>
        private void BtnRefreshTop_Click(object sender, EventArgs e)
        {
            LoadTopBooks();
        }

        /// <summary>
        /// Выдача книги
        /// </summary>
        private void BtnIssue_Click(object sender, EventArgs e)
        {
            // Проверка выбора книги
            if (cmbBooks.SelectedItem == null)
            {
                MessageBox.Show("Выберите книгу из списка!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка ввода ФИО
            if (string.IsNullOrWhiteSpace(txtReaderName.Text))
            {
                MessageBox.Show("Введите ФИО читателя!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedBook = (Book)cmbBooks.SelectedItem;

            // Подтверждение выдачи
            var result = MessageBox.Show(
                $"Выдать книгу \"{selectedBook.Title}\" читателю {txtReaderName.Text}?",
                "Подтверждение выдачи",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _dataService.IssueBook(selectedBook.Id, txtReaderName.Text);

                    if (success)
                    {
                        MessageBox.Show(
                            $"Книга \"{selectedBook.Title}\" успешно выдана!",
                            "Успешно",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        RefreshAllData();

                        txtReaderName.Clear();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Не удалось выдать книгу. Возможно, она уже недоступна.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при выдаче: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Возврат книги
        /// </summary>
        private void BtnReturn_Click(object sender, EventArgs e)
        {
            if (_selectedBook == null)
            {
                MessageBox.Show(
                    "Выберите книгу в каталоге для возврата!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_selectedBook.IsAvailable)
            {
                MessageBox.Show(
                    "Эта книга уже доступна в библиотеке!",
                    "Информация",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"Вернуть книгу \"{_selectedBook.Title}\"?",
                "Подтверждение возврата",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _dataService.ReturnBook(_selectedBook.Id);

                    if (success)
                    {
                        MessageBox.Show(
                            "Книга успешно возвращена!",
                            "Успешно",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        RefreshAllData();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Не удалось вернуть книгу.",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при возврате: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Генерация отчета по штрафам
        /// </summary>
        private void BtnReport_Click(object sender, EventArgs e)
        {
            try
            {
                string reportPath = _dataService.GenerateFineReport();

                // Спрашиваем, открыть ли отчет
                var result = MessageBox.Show(
                    $"Отчет сохранен:\n{reportPath}\n\nОткрыть папку с отчетом?",
                    "Отчет создан",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string folder = System.IO.Path.GetDirectoryName(reportPath);
                    System.Diagnostics.Process.Start("explorer.exe", folder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации отчета: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// О программе
        /// </summary>
        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            string aboutText =
                "Библиотечная система\n" +
                "Версия 1.0\n\n" +
                "Разработчики:\n" +
                "• Иванов И.И. (UI модуль)\n" +
                "• Петров П.П. (Модуль данных)\n\n" +
                "© 2026";

            MessageBox.Show(aboutText, "О программе",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}