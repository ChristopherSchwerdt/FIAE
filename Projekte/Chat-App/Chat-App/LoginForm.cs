using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using ConnectionObject;

namespace Chat_App
{
    public partial class LoginForm : Form
    {
        public bool userSuccessfullyAuthenticated { get; private set; }
        private ChatClient  _client;
        public ChatClient GetClientConnection()
        {
            return _client;
        }
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress serverIP = IPAddress.Parse(txt_Server_IP.Text);
                int port = 50000;
                TcpClient client = new TcpClient();
                client.Connect(serverIP, port);

                //Verbindung erfolgreich
                //Clientobjekt für die weitere Verwendung erstellen
                Connection thisClient = new Connection(client);
                ChatClient thisChatClient = new ChatClient(txt_Name.Text, thisClient);
                _client = thisChatClient;
                userSuccessfullyAuthenticated = true;
                Close();


            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}
