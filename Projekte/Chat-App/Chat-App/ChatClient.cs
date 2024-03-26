using ConnectionObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectionObject;
using System.Net.Sockets;
namespace Chat_App
{
    public class ChatClient
    {
        public Connection Connection { get; private set; }
        //Events:
        public event NewMessageEventHandler OnNewMessage;
        public delegate void NewMessageEventHandler(string sender, string message);

        public event OnUserlistChangedEventHandler OnNewUserlistChanged;
        public delegate void OnUserlistChangedEventHandler(List<string> Userlist);

        private string _username = "none";
        private readonly ConnectionObject.ConnectionObject _ConnectionObject; 

        public ChatClient(string username, Connection connection)
        {
            _username = username;
            Connection = connection;
            _ConnectionObject = new ConnectionObject.ConnectionObject();
        }
        /// <summary>
        /// Gets the current Username
        /// </summary>
        /// <returns></returns>
        public string GetUsername() { return _username; }
        /// <summary>
        /// Sets the current Username
        /// </summary>
        /// <param name="username"></param>
        public void SetUsername(string username) { _username = username; }

        /// <summary>
        /// Deserializes data to an Object
        /// </summary>
        /// <param name="data"></param>
        public void DeserializeObject(byte[] data)
        {
            //Getting the objecttype from the root node of the XML-String
            string xmlString = Encoding.UTF8.GetString(data);
            string xmlRoot = _ConnectionObject.GetTypeFromXML(xmlString);

            if (xmlRoot == "ChatMessage")
            {
                ChatMessage newChatMessage = _ConnectionObject.Deserialize<ChatMessage>(xmlString);
                OnNewMessage?.Invoke(newChatMessage.sender, newChatMessage.message);
            }
            if (xmlRoot == "Query")
            {
                Query newQuery = _ConnectionObject.Deserialize<Query>(xmlString);
                if (newQuery.question == "Username")
                {
                    Connection.Response(new Response(_username, "Username"));
                }
            }
            if (xmlRoot == "SendString")
            {
                Response newSendString = _ConnectionObject.Deserialize<Response>(xmlString);
                if (newSendString.purpose == "UUID")
                {
                    Connection.SetUUID(newSendString.stringToTransfer);
                }
            }
            if(xmlRoot == "Userlist")
            {
                Userlist newUserlist = _ConnectionObject.Deserialize<Userlist>(xmlString);
                OnNewUserlistChanged?.Invoke(newUserlist.userlist);

            }

        }
    }
}
