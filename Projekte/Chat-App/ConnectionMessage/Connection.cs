
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;


namespace ConnectionObject
{
     public class Connection
    {
        //Event fires every time when this client/server recieves new data
        public event NewDataEventHandler OnNewData;
        public delegate void NewDataEventHandler(TcpClient srcclient, byte[] data);
        
        public readonly TcpClient _client;

        private readonly NetworkStream _stream;
        private readonly Task _task;
        private readonly CancellationTokenSource _cts;
        private readonly ConnectionObject _connectionObject;
        
        private string _UUID = "none";
        private const int bufferSize = ushort.MaxValue; // 65536

        public Connection(TcpClient client)
        {
            _client = client;
            _stream = _client.GetStream();
            _cts = new CancellationTokenSource();
            _task = Receive(_cts.Token);
            _connectionObject = new ConnectionObject();                      
        }
        public void SetUUID(string uuid) { _UUID = uuid; }
        public string GetUUID() { return _UUID; }
        /// <summary>
        /// sends data to this specific stream
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task SendStream(byte[] data)
        {
            await _stream.WriteAsync(data).ConfigureAwait(false);
        }
        /// <summary>
        /// sends a Chatmessage object to this specific stream
        /// </summary>
        /// <param name="chatMessage"></param>
        /// <returns></returns>
        public async Task SendChatMessage(ChatMessage chatMessage)
        {
            byte[] data = Encoding.UTF8.GetBytes(_connectionObject.Serialize<ChatMessage>(chatMessage));
            await _stream.WriteAsync(data).ConfigureAwait(false);
        }
        /// <summary>
        /// sends a Query object to this specific stream
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task Query(Query query)
        {
            byte[] data = Encoding.UTF8.GetBytes(_connectionObject.Serialize<Query>(query));
            await _stream.WriteAsync(data).ConfigureAwait(false);
        }
        /// <summary>
        /// sends a SendString object to this specific stream
        /// </summary>
        /// <param name="sendString"></param>
        /// <returns></returns>
        public async Task Response(Response sendString)
        {
            byte[] data = Encoding.UTF8.GetBytes(_connectionObject.Serialize<Response>(sendString));
            await _stream.WriteAsync(data).ConfigureAwait(false);
        }
        /// <summary>
        /// receive data from this stream
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task Receive(CancellationToken token)
        {
            byte[] buffer = new byte[bufferSize];
            try
            {
                while (true)
                {
                    await _stream.ReadAsync(buffer, token).ConfigureAwait(false);
                    //int size = BitConverter.ToUInt16(buffer, 0);
                    //var data = Encoding.UTF8.GetString(buffer.AsSpan(0, size));
                    OnNewData?.Invoke(_client,buffer);
                    buffer = new byte[bufferSize];
                }
            }
            catch (OperationCanceledException)
            {
                if (_client.Connected)
                {
                    _stream.Close();
                    Console.WriteLine($"Connection to {_client.Client.RemoteEndPoint} closed.");
                    //Todo:Add a event to notify the Chatserver class and removing this client from connected clients list.
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                throw;
                //Todo:Add a event to notify the Chatserver class and removing this client from connected clients list.
            }
        }
        /// <summary>
        /// close this connection
        /// </summary>
        public void Close()
        {
            _cts.Cancel();
            _task.Wait();
        }
    }
}
