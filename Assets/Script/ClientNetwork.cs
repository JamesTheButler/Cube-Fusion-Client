using System;
using UnityEngine;
using UnityEngine.Networking;


public class ClientNetwork : MonoBehaviour
{
    public class DataMessage : MessageBase
    {
        public string message;
    }

    int port = 5555;
    public string address;

    // The id we use to identify our messages and register the handler
    short messageID = 1000;
    short queueID = 1001;

    // The network client
    NetworkClient client;

    void Start()
    {
        CreateClient();
    }

    void CreateClient()
    {
        client = new NetworkClient();

        // Configuration
        var config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableFragmented);
        config.AddChannel(QosType.UnreliableFragmented);
        client.Configure(config, 1);

        // Register handlers
        RegisterHandlers();

        // Connect
        client.Connect(address, port);
    }

    // Register the handlers
    void RegisterHandlers()
    {
        client.RegisterHandler(messageID, OnMessageReceived);
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
    }

    void OnConnected(NetworkMessage message)
    {
        Debug.Log("Connected to server");
        DataMessage msg = new DataMessage();
        msg.message = "Hello server!";
        
        client.Send(messageID, msg);
    }

    //Disconnected from server
    void OnDisconnected(NetworkMessage message)
    {
        Debug.Log("Disconnected from server");
    }

    // Message from the server
    void OnMessageReceived(NetworkMessage netMessage)
    {
        var objectMessage = netMessage.ReadMessage<DataMessage>();

        Debug.Log("Message received: " + objectMessage.message);
    }

    public void SendQueue(string queue)
    {
        DataMessage msg = new DataMessage();
        msg.message = queue;
        client.Send(queueID, msg);
    }
}