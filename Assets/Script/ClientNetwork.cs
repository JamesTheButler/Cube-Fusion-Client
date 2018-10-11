using System;
using UnityEngine;
using UnityEngine.Networking;

public class ClientNetwork : MonoBehaviour
{
    public class DataMessage : MessageBase {
        public string message;
    }

    int port = 5555;
    public string ipAdress = "192.168.43.238";
    public int clientId;
    public UIManager uiMgr;

    // The id we use to identify our messages and register the handler
    const short MESSAGE_ID = 1000;
    const short QUEUE_ID = 1001;

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

        // set connection image
        uiMgr.setIsConnected(false);

        // Connect
        Debug.Log("ClientNetwork :: Trying to connect to server ("+ipAdress+", "+port+")");
        client.Connect(ipAdress, port);

    }

    // Register the handlers
    void RegisterHandlers()
    {
        client.RegisterHandler(MESSAGE_ID, OnMessageReceived);
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
    }

    void OnConnected(NetworkMessage message)
    {
        Debug.Log("Connected to server");
        DataMessage msg = new DataMessage();
        msg.message = "Hello server!";
        uiMgr.setIsConnected(true);

        client.Send(MESSAGE_ID, msg);
    }

    //Disconnected from server
    void OnDisconnected(NetworkMessage message)
    {
        Debug.Log("Disconnected from server");
        uiMgr.setIsConnected(false);

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
        client.Send(QUEUE_ID, msg);
    }
}