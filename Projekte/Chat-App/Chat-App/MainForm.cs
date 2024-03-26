using ConnectionObject;
using System.Net.Sockets;

namespace Chat_App
{
    public partial class MainForm : Form
    {
        private ChatClient _client;
        public MainForm(ChatClient client)
        {
            _client = client;
            _client.Connection.OnNewData += new Connection.NewDataEventHandler(OnNewData);
            _client.OnNewMessage += new ChatClient.NewMessageEventHandler(OnNewMessage);
            _client.OnNewUserlistChanged += new ChatClient.OnUserlistChangedEventHandler(OnUserlistChanged);
            InitializeComponent();
        }

        private void OnUserlistChanged(List<string> userlist)
        {
            lb_UserList.Invoke(ReloadUserlist, userlist);
        }

        private void ReloadUserlist(List<string> userlist)
        {
            lb_UserList.Items.Clear();
            foreach (string user in userlist)
            {
                lb_UserList.Items.Add(user);
            }
        }

        private void OnNewMessage(string sender, string message)
        {
            txt_ChatHistory.Invoke(ChatHistoryAppendText, sender, message);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "Chat beigetreten als: " + _client.GetUsername();
        }

        private void OnNewData(TcpClient srcClient, byte[] data)
        {
            //Debug info:
           // txt_ChatHistory.Invoke(ChatHistoryAppendText,"server", message);
            
            _client.DeserializeObject(data);
        }
        private void ChatHistoryAppendText(string sender ,string message)
        {
            txt_ChatHistory.AppendText(sender + ":"+message);
            txt_ChatHistory.Text += Environment.NewLine;
        }
        private void btn_Send_Click(object sender, EventArgs e)
        {
            ChatMessage chatMessage = new ChatMessage();
            chatMessage.sender = _client.GetUsername();
            chatMessage.senderUUID = _client.Connection.GetUUID();
            chatMessage.receiver = "All";
            chatMessage.message = txt_Input.Text;
            _client.Connection.SendChatMessage(chatMessage);
            txt_Input.Clear();
        }
        private void txt_Input_KeyUp(object sender, KeyEventArgs e)
        {
            //Sends the message on Enterkey
            if (e.KeyCode == Keys.Enter)
            {
                btn_Send_Click(sender, e);
            }
        }
        private void btn_Refresh_UserList_Click(object sender, EventArgs e)
        {
            //Asking the Server for the current Userlist
            Query query = new Query();
            query.question = "Userlist";
            query.sender = _client.Connection.GetUUID();
            _client.Connection.Query(query);
        }
    }
}
