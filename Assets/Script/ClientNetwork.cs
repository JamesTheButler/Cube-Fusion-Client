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
    public DeleteQueue deleteQueue;

    // The id we use to identify our messages and register the handler
    const short MESSAGE_ID = 1000;
    const short QUEUE_ID = 1001;
    const short SETUP_ID = 1002;

    // The network client
    NetworkClient client;

    void Start()
    {
        CreateClient();
    }

    void CreateClient()
    {
        uiMgr.EnablePlay(false);
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
        client.RegisterHandler(SETUP_ID, SetupPlayer);
        client.RegisterHandler(MESSAGE_ID, OnMessageReceived);
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
    }

    void SetupPlayer(NetworkMessage netMessage)
    {
        var objectMessage = netMessage.ReadMessage<DataMessage>();
        if (objectMessage.message == "1")
        {
            uiMgr.CubeColor(Color.green);
        }
        else if (objectMessage.message == "2")
        {
            uiMgr.CubeColor(Color.yellow);
        }
        else if (objectMessage.message == "3")
        {
            uiMgr.EnablePlay(true);
        }
        else if (objectMessage.message == "4")
        {
            uiMgr.EnablePlay(true);
            deleteQueue.deleteQueue();
        }
        else if (objectMessage.message == "0")
        {
            uiMgr.CubeColor(Color.white);
        }
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
        uiMgr.CubeColor(Color.white);
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