namespace Chat_App
{
    partial class MainForm
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
            txt_Input = new TextBox();
            panel1 = new Panel();
            txt_ChatHistory = new RichTextBox();
            btn_Send = new Button();
            panel2 = new Panel();
            btn_Refresh_UserList = new Button();
            lb_UserList = new ListBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // txt_Input
            // 
            txt_Input.Location = new Point(3, 420);
            txt_Input.Name = "txt_Input";
            txt_Input.Size = new Size(484, 23);
            txt_Input.TabIndex = 1;
            txt_Input.KeyUp += txt_Input_KeyUp;
            // 
            // panel1
            // 
            panel1.Controls.Add(txt_ChatHistory);
            panel1.Controls.Add(btn_Send);
            panel1.Controls.Add(txt_Input);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 450);
            panel1.TabIndex = 2;
            // 
            // txt_ChatHistory
            // 
            txt_ChatHistory.Location = new Point(6, 1);
            txt_ChatHistory.Name = "txt_ChatHistory";
            txt_ChatHistory.Size = new Size(564, 410);
            txt_ChatHistory.TabIndex = 3;
            txt_ChatHistory.Text = "";
            // 
            // btn_Send
            // 
            btn_Send.Location = new Point(493, 417);
            btn_Send.Name = "btn_Send";
            btn_Send.Size = new Size(77, 27);
            btn_Send.TabIndex = 2;
            btn_Send.Text = "Senden";
            btn_Send.UseVisualStyleBackColor = true;
            btn_Send.Click += btn_Send_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(btn_Refresh_UserList);
            panel2.Controls.Add(lb_UserList);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(576, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(224, 450);
            panel2.TabIndex = 3;
            // 
            // btn_Refresh_UserList
            // 
            btn_Refresh_UserList.Location = new Point(60, 415);
            btn_Refresh_UserList.Name = "btn_Refresh_UserList";
            btn_Refresh_UserList.Size = new Size(87, 23);
            btn_Refresh_UserList.TabIndex = 1;
            btn_Refresh_UserList.Text = "aktualisieren";
            btn_Refresh_UserList.UseVisualStyleBackColor = true;
            btn_Refresh_UserList.Click += btn_Refresh_UserList_Click;
            // 
            // lb_UserList
            // 
            lb_UserList.FormattingEnabled = true;
            lb_UserList.ItemHeight = 15;
            lb_UserList.Items.AddRange(new object[] { "none" });
            lb_UserList.Location = new Point(3, 3);
            lb_UserList.Name = "lb_UserList";
            lb_UserList.Size = new Size(218, 409);
            lb_UserList.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "MainForm";
            Text = "Chat";
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TextBox txt_Input;
        private Panel panel1;
        private Panel panel2;
        private Button btn_Send;
        private ListBox lb_UserList;
        private RichTextBox txt_ChatHistory;
        private Button btn_Refresh_UserList;
    }
}
