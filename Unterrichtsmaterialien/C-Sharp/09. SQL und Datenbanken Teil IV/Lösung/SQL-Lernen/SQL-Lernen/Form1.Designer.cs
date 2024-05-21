namespace SQL_Lernen
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            InputBox = new RichTextBox();
            btn_executeSQL = new Button();
            dataGridView1 = new DataGridView();
            treeViewDatabase = new TreeView();
            panel1 = new Panel();
            lbl_Info = new Label();
            menuStrip1 = new MenuStrip();
            dateiToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            aufWerkseinstellungToolStripMenuItem = new ToolStripMenuItem();
            sichernBackupToolStripMenuItem = new ToolStripMenuItem();
            wiederherstellenToolStripMenuItem = new ToolStripMenuItem();
            beendenToolStripMenuItem = new ToolStripMenuItem();
            befehleToolStripMenuItem = new ToolStripMenuItem();
            sELECTToolStripMenuItem = new ToolStripMenuItem();
            uPDATEToolStripMenuItem = new ToolStripMenuItem();
            iNSERTToolStripMenuItem = new ToolStripMenuItem();
            dELETEToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // InputBox
            // 
            InputBox.Dock = DockStyle.Bottom;
            InputBox.Location = new Point(0, 55);
            InputBox.Name = "InputBox";
            InputBox.Size = new Size(723, 260);
            InputBox.TabIndex = 0;
            InputBox.Text = "Select * from Table";
            // 
            // btn_executeSQL
            // 
            btn_executeSQL.Location = new Point(0, 12);
            btn_executeSQL.Name = "btn_executeSQL";
            btn_executeSQL.Size = new Size(42, 34);
            btn_executeSQL.TabIndex = 1;
            btn_executeSQL.Text = "|>";
            btn_executeSQL.UseVisualStyleBackColor = true;
            btn_executeSQL.Click += btn_executeSQL_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 33);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(990, 565);
            dataGridView1.TabIndex = 2;
            // 
            // treeViewDatabase
            // 
            treeViewDatabase.Dock = DockStyle.Right;
            treeViewDatabase.Location = new Point(723, 33);
            treeViewDatabase.Name = "treeViewDatabase";
            treeViewDatabase.Size = new Size(267, 565);
            treeViewDatabase.TabIndex = 3;
            treeViewDatabase.Tag = "treeViewDatabase";
            // 
            // panel1
            // 
            panel1.Controls.Add(lbl_Info);
            panel1.Controls.Add(btn_executeSQL);
            panel1.Controls.Add(InputBox);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 283);
            panel1.Name = "panel1";
            panel1.Size = new Size(723, 315);
            panel1.TabIndex = 4;
            // 
            // lbl_Info
            // 
            lbl_Info.AutoSize = true;
            lbl_Info.Location = new Point(173, 16);
            lbl_Info.Name = "lbl_Info";
            lbl_Info.Size = new Size(16, 25);
            lbl_Info.TabIndex = 2;
            lbl_Info.Text = "l";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dateiToolStripMenuItem, befehleToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(990, 33);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            dateiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, beendenToolStripMenuItem });
            dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            dateiToolStripMenuItem.Size = new Size(69, 29);
            dateiToolStripMenuItem.Text = "Datei";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { aufWerkseinstellungToolStripMenuItem, sichernBackupToolStripMenuItem, wiederherstellenToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(200, 34);
            toolStripMenuItem1.Text = "Datenbank";
            // 
            // aufWerkseinstellungToolStripMenuItem
            // 
            aufWerkseinstellungToolStripMenuItem.Name = "aufWerkseinstellungToolStripMenuItem";
            aufWerkseinstellungToolStripMenuItem.Size = new Size(280, 34);
            aufWerkseinstellungToolStripMenuItem.Text = "Auf Werkseinstellung";
            aufWerkseinstellungToolStripMenuItem.Click += aufWerkseinstellungToolStripMenuItem_Click;
            // 
            // sichernBackupToolStripMenuItem
            // 
            sichernBackupToolStripMenuItem.Name = "sichernBackupToolStripMenuItem";
            sichernBackupToolStripMenuItem.Size = new Size(280, 34);
            sichernBackupToolStripMenuItem.Text = "Sichern / Backup";
            sichernBackupToolStripMenuItem.Click += sichernBackupToolStripMenuItem_Click;
            // 
            // wiederherstellenToolStripMenuItem
            // 
            wiederherstellenToolStripMenuItem.Name = "wiederherstellenToolStripMenuItem";
            wiederherstellenToolStripMenuItem.Size = new Size(280, 34);
            wiederherstellenToolStripMenuItem.Text = "Wiederherstellen";
            wiederherstellenToolStripMenuItem.Click += wiederherstellenToolStripMenuItem_Click;
            // 
            // beendenToolStripMenuItem
            // 
            beendenToolStripMenuItem.Name = "beendenToolStripMenuItem";
            beendenToolStripMenuItem.Size = new Size(200, 34);
            beendenToolStripMenuItem.Text = "Beenden";
            beendenToolStripMenuItem.Click += beendenToolStripMenuItem_Click;
            // 
            // befehleToolStripMenuItem
            // 
            befehleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sELECTToolStripMenuItem, uPDATEToolStripMenuItem, iNSERTToolStripMenuItem, dELETEToolStripMenuItem });
            befehleToolStripMenuItem.Name = "befehleToolStripMenuItem";
            befehleToolStripMenuItem.Size = new Size(85, 29);
            befehleToolStripMenuItem.Text = "Befehle";
            // 
            // sELECTToolStripMenuItem
            // 
            sELECTToolStripMenuItem.Name = "sELECTToolStripMenuItem";
            sELECTToolStripMenuItem.Size = new Size(178, 34);
            sELECTToolStripMenuItem.Text = "SELECT";
            sELECTToolStripMenuItem.Click += sELECTToolStripMenuItem_Click;
            // 
            // uPDATEToolStripMenuItem
            // 
            uPDATEToolStripMenuItem.Name = "uPDATEToolStripMenuItem";
            uPDATEToolStripMenuItem.Size = new Size(178, 34);
            uPDATEToolStripMenuItem.Text = "UPDATE";
            uPDATEToolStripMenuItem.Click += uPDATEToolStripMenuItem_Click;
            // 
            // iNSERTToolStripMenuItem
            // 
            iNSERTToolStripMenuItem.Name = "iNSERTToolStripMenuItem";
            iNSERTToolStripMenuItem.Size = new Size(178, 34);
            iNSERTToolStripMenuItem.Text = "INSERT";
            iNSERTToolStripMenuItem.Click += iNSERTToolStripMenuItem_Click;
            // 
            // dELETEToolStripMenuItem
            // 
            dELETEToolStripMenuItem.Name = "dELETEToolStripMenuItem";
            dELETEToolStripMenuItem.Size = new Size(178, 34);
            dELETEToolStripMenuItem.Text = "DELETE";
            dELETEToolStripMenuItem.Click += dELETEToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(990, 598);
            Controls.Add(panel1);
            Controls.Add(treeViewDatabase);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "SQL App";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox InputBox;
        private Button btn_executeSQL;
        private DataGridView dataGridView1;
        private TreeView treeViewDatabase;
        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dateiToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem befehleToolStripMenuItem;
        private ToolStripMenuItem sELECTToolStripMenuItem;
        private ToolStripMenuItem uPDATEToolStripMenuItem;
        private ToolStripMenuItem iNSERTToolStripMenuItem;
        private ToolStripMenuItem dELETEToolStripMenuItem;
        private Label lbl_Info;
        private ToolStripMenuItem aufWerkseinstellungToolStripMenuItem;
        private ToolStripMenuItem sichernBackupToolStripMenuItem;
        private ToolStripMenuItem wiederherstellenToolStripMenuItem;
        private ToolStripMenuItem beendenToolStripMenuItem;
    }
}
