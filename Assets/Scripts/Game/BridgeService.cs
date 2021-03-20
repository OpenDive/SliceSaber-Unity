using UnityEngine;
using System;
//using BestHTTP;
//using BestHTTP.SocketIO;
//using SimpleJSON;
using System.Collections.Generic;


public class BridgeService : MonoBehaviour
{
    //#region PUBLIC_VARS

    ////Instance
    //public static BridgeService instance;

    ////Set Namespace For Current Player
    //public string namespaceForCurrentPlayer;

    ////SocketManager Reference
    //public SocketManager socketManagerRef;

    ////Socket Reference
    //public Socket mySocket;

    ////Options
    //SocketOptions options;

    //#endregion

    //#region PRIVATE_VARS

    //#endregion

    //#region UNITY_CALLBACKS

    //// Use this for initialization
    ////  // Start is called before the first frame update
    //void Start()
    //{
    //    Debug.Log("BridgeService started...");
    //    //Instance
    //    instance = this;
    //    Debug.Log("Starting socket connection...");

    //    //Create Socket Ref
    //    CreateSocketRef();
    //}

    ////Leave Room After Quite The Game
    //void OnApplicationQuit()
    //{
    //    LeaveRoomFromServer();
    //    DisconnectMySocket();
    //}

    //#endregion

    //#region PUBLIC_METHODS

    //public void CreateSocketRef()
    //{
    //    TimeSpan miliSecForReconnect = TimeSpan.FromMilliseconds(10);

    //    options = new SocketOptions();
    //    options.ReconnectionAttempts = 3;
    //    options.AutoConnect = true;
    //    options.ReconnectionDelay = miliSecForReconnect;

    //    //Server URI
    //    Debug.Log("Attempting to connect to Socket ...");
    //    socketManagerRef = new SocketManager(new Uri("http://localhost:55555/socket.io/"), options);
    //    Debug.Log("Attempting to connect to Socket [2] ...");
    //    SetNamespaceForSocket("/room1");
    //    instance.SendGameActionDataToServer(10, 2, "11");
    //}

    //public void SetNamespaceForSocket(string socketNamespace)
    //{
    //    Debug.Log("Setting Namespace for Socket....");
    //    namespaceForCurrentPlayer = socketNamespace;
    //    mySocket = socketManagerRef.GetSocket(namespaceForCurrentPlayer);

    //    //Set All Events, When Join The New Room
    //    SetAllEvents();
    //}

    ////=== Emit Events ===//

    ////Send User Action Data
    //public void SendGameActionDataToServer(float objXPos, float objYPos, string senderDeviceUniqueId)
    //{
    //    Dictionary<string, object> gameActionData = new Dictionary<string, object>();
    //    gameActionData.Add("objXPos", objXPos);
    //    gameActionData.Add("objYPos", objYPos);
    //    gameActionData.Add("senderDeviceUniqueId", senderDeviceUniqueId);

    //    mySocket.Emit("action", OnSendEmitDataToServerCallBack, gameActionData);
    //}

    ////Send Leave Room Data
    //public void LeaveRoomFromServer()
    //{
    //    Debug.Log("Leaving Room from Server...");
    //    mySocket.Emit("leave", OnSendEmitDataToServerCallBack);
    //}

    ////Disconnect My Socket
    //public void DisconnectMySocket()
    //{
    //    mySocket.Disconnect();
    //}

    //#endregion

    //#region PRIVATE_METHODS

    //private void SetAllEvents()
    //{

    //    //Get UserAction Data From Server
    //    mySocket.On("action", OnGetActionData);

    //    //Leave Room
    //    mySocket.On("leave", OnLeaveRoomData);

    //    //Connect
    //    mySocket.On("connect", OnConnect);

    //    //Re-Connect
    //    mySocket.On("reconnect", OnReConnect);

    //    //Re-Connecting
    //    mySocket.On("reconnecting", OnReConnecting);

    //    //Re-Connect Attempt
    //    mySocket.On("reconnect_attempt", OnReConnectAttempt);

    //    //Re-Connect Attempt
    //    mySocket.On("reconnect_failed", OnReConnectFailed);

    //    //Disconnect
    //    mySocket.On("disconnect", OnDisconnect);

    //    mySocket.On("wallet_information", OnWalletInformation);
    //}

    ////=== On Event's Methods ===//

    //private void OnConnect(Socket socket, Packet packet, params object[] args)
    //{
    //    Debug.Log("Connect...");
    //}

    //private void OnReConnect(Socket socket, Packet packet, params object[] args)
    //{
    //    Debug.Log("Re-Connect...");
    //}

    //private void OnReConnecting(Socket socket, Packet packet, params object[] args)
    //{
    //    Debug.Log("Re-Connecting...");
    //}

    //private void OnReConnectAttempt(Socket socket, Packet packet, params object[] args)
    //{
    //    Debug.Log("Re-Connect Attempt...");
    //}

    //private void OnReConnectFailed(Socket socket, Packet packet, params object[] args)
    //{
    //    Debug.Log("Re-ConnectFailed...");
    //}

    ////Get User Action Data
    //private void OnGetActionData(Socket socket, Packet packet, params object[] args)
    //{
    //    var res = JSON.Parse(packet.ToString());
    //    //GameManager.instance.RefreshGamePlayerUI(res[1]);
    //}

    ////Leave Room Data
    //private void OnLeaveRoomData(Socket socket, Packet packet, params object[] args)
    //{
    //    Debug.Log("Leave Room");
    //}

    ////Disconnect From Room
    //private void OnDisconnect(Socket socket, Packet packet, params object[] args)
    //{
    //    Debug.Log("Disconnect...");
    //}

    ////=== Emit Event's Methods ===//

    //private void OnSendEmitDataToServerCallBack(Socket socket, Packet packet, params object[] args)
    //{
    //    Debug.Log("Send Packet Data : " + packet.ToString());
    //}

    //// Receive Wallet information
    //private void OnWalletInformation(Socket socket, Packet packet, params object[] args)
    //{

    //}

    //#endregion
    //void Awake()
    //{
    //    Debug.Log("Awake");
    //}
    //// Update is called once per frame
    /////void Update()
    ////{

    ////}
}
