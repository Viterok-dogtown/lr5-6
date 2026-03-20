namespace LibraryUIModule
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетПоШтрафамToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvCatalog;
        private Label label1;
        private TextBox txtReaderName;
        private GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvTopBooks;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnRefreshTop;
        private System.Windows.Forms.ComboBox cmbBooks;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.GroupBox groupHistory;
        private System.Windows.Forms.ListBox lstHistory;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            выходStripMenuItem2 = new ToolStripMenuItem();
            отчетыToolStripMenuItem = new ToolStripMenuItem();
            отчетПоШтрафамToolStripMenuItem = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            оПрограммеToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            dgvCatalog = new DataGridView();
            tabPage2 = new TabPage();
            dgvTopBooks = new DataGridView();
            panelTop = new Panel();
            btnRefreshTop = new Button();
            tabPage3 = new TabPage();
            lstHistory = new ListBox();
            groupBox1 = new GroupBox();
            btnReturn = new Button();
            btnIssue = new Button();
            txtReaderName = new TextBox();
            cmbBooks = new ComboBox();
            label1 = new Label();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCatalog).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopBooks).BeginInit();
            panelTop.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, отчетыToolStripMenuItem, справкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(915, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { выходStripMenuItem2 });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(59, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // выходStripMenuItem2
            // 
            выходStripMenuItem2.Name = "выходStripMenuItem2";
            выходStripMenuItem2.Size = new Size(136, 26);
            выходStripMenuItem2.Text = "Выход";
            // 
            // отчетыToolStripMenuItem
            // 
            отчетыToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { отчетПоШтрафамToolStripMenuItem });
            отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            отчетыToolStripMenuItem.Size = new Size(73, 24);
            отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // отчетПоШтрафамToolStripMenuItem
            // 
            отчетПоШтрафамToolStripMenuItem.Name = "отчетПоШтрафамToolStripMenuItem";
            отчетПоШтрафамToolStripMenuItem.Size = new Size(219, 26);
            отчетПоШтрафамToolStripMenuItem.Text = "отчет по штрафам";
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { оПрограммеToolStripMenuItem });
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(81, 24);
            справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            оПрограммеToolStripMenuItem.Size = new Size(185, 26);
            оПрограммеToolStripMenuItem.Text = "о программе";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(0, 31);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(909, 368);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dgvCatalog);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(901, 335);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvCatalog
            // 
            dgvCatalog.AllowUserToAddRows = false;
            dgvCatalog.AllowUserToDeleteRows = false;
            dgvCatalog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCatalog.Dock = DockStyle.Fill;
            dgvCatalog.Location = new Point(3, 3);
            dgvCatalog.Name = "dgvCatalog";
            dgvCatalog.ReadOnly = true;
            dgvCatalog.RowHeadersWidth = 51;
            dgvCatalog.Size = new Size(895, 329);
            dgvCatalog.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dgvTopBooks);
            tabPage2.Controls.Add(panelTop);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(901, 335);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvTopBooks
            // 
            dgvTopBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTopBooks.Dock = DockStyle.Fill;
            dgvTopBooks.Location = new Point(3, 3);
            dgvTopBooks.Name = "dgvTopBooks";
            dgvTopBooks.RowHeadersWidth = 51;
            dgvTopBooks.Size = new Size(895, 204);
            dgvTopBooks.TabIndex = 1;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnRefreshTop);
            panelTop.Dock = DockStyle.Bottom;
            panelTop.Location = new Point(3, 207);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(895, 125);
            panelTop.TabIndex = 0;
            // 
            // btnRefreshTop
            // 
            btnRefreshTop.Location = new Point(753, 93);
            btnRefreshTop.Name = "btnRefreshTop";
            btnRefreshTop.Size = new Size(139, 29);
            btnRefreshTop.TabIndex = 1;
            btnRefreshTop.Text = "Обновите топ";
            btnRefreshTop.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(lstHistory);
            tabPage3.Controls.Add(groupBox1);
            tabPage3.Controls.Add(btnReturn);
            tabPage3.Controls.Add(btnIssue);
            tabPage3.Controls.Add(txtReaderName);
            tabPage3.Controls.Add(cmbBooks);
            tabPage3.Controls.Add(label1);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(901, 335);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // lstHistory
            // 
            lstHistory.FormattingEnabled = true;
            lstHistory.Location = new Point(673, 39);
            lstHistory.Name = "lstHistory";
            lstHistory.Size = new Size(222, 244);
            lstHistory.TabIndex = 6;
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(425, 24);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(220, 260);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "История Выдачи";
            // 
            // btnReturn
            // 
            btnReturn.Location = new Point(201, 84);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(147, 29);
            btnReturn.TabIndex = 4;
            btnReturn.Text = "Вернуть книгу";
            btnReturn.UseVisualStyleBackColor = true;
            // 
            // btnIssue
            // 
            btnIssue.Location = new Point(201, 49);
            btnIssue.Name = "btnIssue";
            btnIssue.Size = new Size(147, 29);
            btnIssue.TabIndex = 3;
            btnIssue.Text = "Выдать книгу";
            btnIssue.UseVisualStyleBackColor = true;
            // 
            // txtReaderName
            // 
            txtReaderName.Location = new Point(218, 16);
            txtReaderName.Name = "txtReaderName";
            txtReaderName.Size = new Size(110, 27);
            txtReaderName.TabIndex = 2;
            txtReaderName.Text = "ФИО читателя:";
            // 
            // cmbBooks
            // 
            cmbBooks.FormattingEnabled = true;
            cmbBooks.Location = new Point(8, 39);
            cmbBooks.Name = "cmbBooks";
            cmbBooks.Size = new Size(151, 28);
            cmbBooks.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 16);
            label1.Name = "label1";
            label1.Size = new Size(106, 20);
            label1.TabIndex = 0;
            label1.Text = "Выбери книгу";
            // 
            // FormMain
            // 
            ClientSize = new Size(915, 395);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCatalog).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTopBooks).EndInit();
            panelTop.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private MenuStrip menuStrip1;
        private TabControl tabControl1;
        private ToolStripMenuItem выходStripMenuItem2;
        
    }
}