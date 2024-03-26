using System.Net.Sockets;
using System.Net;
using ConnectionObject;
using System.Text;
using System.IO;

namespace Chat_Server
{
    class ChatServer
    {
        private int _port;
        private TcpListener server;
        private List<Connection> connectedClients;
        private ConnectionObject.ConnectionObject _ConnectionObject;
        public ChatServer(int Port)
        {
            _port = Port;
            _ConnectionObject = new ConnectionObject.ConnectionObject();
        }
        /// <summary>
        /// Starting the Chatserver
        /// </summary>
        public void Start()
        {
            server  = new TcpListener(IPAddress.Any, _port);
            connectedClients = new List<Connection>();
            server.Start();
            CheckForNewConnection();            
        }
        private void CheckForNewConnection()
        {
            server.BeginAcceptTcpClient(HandleNewClient, server);
        }
      
        private async void HandleNewClient(IAsyncResult result)
        {
            //check again if the is a new connection
            CheckForNewConnection();
            //create a new client
            TcpClient client = server.EndAcceptTcpClient(result);
            //create a new connection for this client
            Connection newConnection  = new Connection(client);            
            //waiting 1 till the client is ready
            //Because this connection is establish when client in Loginwindow
            //Now the client opens the Mainform of the app and is maybe not ready.
            await Task.Delay(1000);
            //Generating a new UUID for this client
            string newUUID = Guid.NewGuid().ToString();
            //Sending the UUID to the new client
            Response sendUUID = new Response();
            sendUUID.stringToTransfer = newUUID;
            sendUUID.purpose = "UUID";
            newConnection.Response(sendUUID);
            //saves the uuid in our connection object
            newConnection.SetUUID(newUUID);            
            //Subscibe to the NewMessage Event
            newConnection.OnNewData += NewDataHandler;
            //Add the new Client to the Connected Clients list
            connectedClients.Add(newConnection);
            
            Console.WriteLine($"Client connected: {newConnection._client.Client.RemoteEndPoint}");
        }
        /// <summary>
        /// Getting Data from any connected Client
        /// </summary>
        /// <param name="srcClient"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        private void NewDataHandler(TcpClient srcClient, byte[] data)
        {
            DeserializeObject(srcClient, data);
        }

        private void HandleNewChatMessage(TcpClient srcClient, ChatMessage chatMessage)
        {
            if (chatMessage.receiver == "All")//Broadcast to all
            {
                foreach (Connection clientConnection in connectedClients)
                {
                    clientConnection.SendChatMessage(chatMessage);
                }
            }
        }
        /// <summary>
        /// Sending the current userlist 
        /// </summary>
        /// <param name="userlist"></param>
        /// <param name="destClient"></param>
        private void SendUserlist(Userlist userlist, TcpClient destClient)
        {
            byte[] data = Encoding.UTF8.GetBytes(_ConnectionObject.Serialize<Userlist>(userlist));
            destClient.GetStream().WriteAsync(data).ConfigureAwait(false);
        }
        /// <summary>
        /// Deserializes data to an Object
        /// </summary>
        /// <param name="srcClient"></param>
        /// <param name="data"></param>
        private void DeserializeObject(TcpClient srcClient, byte[] data)
        {
            string xmlString = Encoding.UTF8.GetString(data);
            string xmlRoot = _ConnectionObject.GetTypeFromXML(xmlString);
            //Type anyConnetionManagerType = Type.GetType("ConnectionManager."+xmlRoot);

            if (xmlRoot == "ChatMessage")
            {
                ChatMessage newChatMessage = _ConnectionObject.Deserialize<ChatMessage>(xmlString);
                HandleNewChatMessage(srcClient, newChatMessage);

            }
            if (xmlRoot == "Query")
            {
                Query newQuery = _ConnectionObject.Deserialize<Query>(xmlString);
                if (newQuery.question == "Userlist")//Send the requesting Client the userlist
                {
                    List<string> userlist = new List<string>();
                    foreach (Connection conn in connectedClients)
                    {
                        userlist.Add(conn.GetUUID());
                    }

                    Userlist ulist = new Userlist();
                    ulist.userlist = userlist;

                    SendUserlist(ulist, srcClient);
                }
            }
            if (xmlRoot == "SendString")
            {
                Response newSendString = _ConnectionObject.Deserialize<Response>(xmlString);
            }

        }


    }
}
