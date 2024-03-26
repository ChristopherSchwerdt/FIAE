namespace Chat_App
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txt_Server_IP = new TextBox();
            lbl_IP = new Label();
            txt_Name = new TextBox();
            lbl_name = new Label();
            label1 = new Label();
            txt_Passwort = new TextBox();
            btn_Login = new Button();
            lbl_New_Account = new LinkLabel();
            SuspendLayout();
            // 
            // txt_Server_IP
            // 
            txt_Server_IP.Location = new Point(75, 12);
            txt_Server_IP.Name = "txt_Server_IP";
            txt_Server_IP.PlaceholderText = "127.0.0.1";
            txt_Server_IP.Size = new Size(168, 23);
            txt_Server_IP.TabIndex = 0;
            txt_Server_IP.Text = "127.0.0.1";
            txt_Server_IP.TextAlign = HorizontalAlignment.Center;
            // 
            // lbl_IP
            // 
            lbl_IP.AutoSize = true;
            lbl_IP.Location = new Point(12, 15);
            lbl_IP.Name = "lbl_IP";
            lbl_IP.Size = new Size(42, 15);
            lbl_IP.TabIndex = 1;
            lbl_IP.Text = "Server:";
            // 
            // txt_Name
            // 
            txt_Name.Location = new Point(75, 41);
            txt_Name.Name = "txt_Name";
            txt_Name.PlaceholderText = "Bob";
            txt_Name.Size = new Size(168, 23);
            txt_Name.TabIndex = 2;
            txt_Name.Text = "CSchwerdt";
            txt_Name.TextAlign = HorizontalAlignment.Center;
            // 
            // lbl_name
            // 
            lbl_name.AutoSize = true;
            lbl_name.Location = new Point(12, 44);
            lbl_name.Name = "lbl_name";
            lbl_name.Size = new Size(42, 15);
            lbl_name.TabIndex = 3;
            lbl_name.Text = "Name:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 73);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 5;
            label1.Text = "Passwort:";
            // 
            // txt_Passwort
            // 
            txt_Passwort.Location = new Point(75, 70);
            txt_Passwort.Name = "txt_Passwort";
            txt_Passwort.PasswordChar = '*';
            txt_Passwort.Size = new Size(168, 23);
            txt_Passwort.TabIndex = 4;
            txt_Passwort.Text = "nichts";
            txt_Passwort.TextAlign = HorizontalAlignment.Center;
            // 
            // btn_Login
            // 
            btn_Login.Location = new Point(168, 99);
            btn_Login.Name = "btn_Login";
            btn_Login.Size = new Size(75, 23);
            btn_Login.TabIndex = 6;
            btn_Login.Text = "Login";
            btn_Login.UseVisualStyleBackColor = true;
            btn_Login.Click += btn_Login_Click;
            // 
            // lbl_New_Account
            // 
            lbl_New_Account.AutoSize = true;
            lbl_New_Account.Location = new Point(12, 107);
            lbl_New_Account.Name = "lbl_New_Account";
            lbl_New_Account.Size = new Size(137, 15);
            lbl_New_Account.TabIndex = 7;
            lbl_New_Account.TabStop = true;
            lbl_New_Account.Text = "Neuen Account erstellen";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(263, 129);
            Controls.Add(lbl_New_Account);
            Controls.Add(btn_Login);
            Controls.Add(label1);
            Controls.Add(txt_Passwort);
            Controls.Add(lbl_name);
            Controls.Add(txt_Name);
            Controls.Add(lbl_IP);
            Controls.Add(txt_Server_IP);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt_Server_IP;
        private Label lbl_IP;
        private TextBox txt_Name;
        private Label lbl_name;
        private Label label1;
        private TextBox txt_Passwort;
        private Button btn_Login;
        private LinkLabel lbl_New_Account;
    }
}