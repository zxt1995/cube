using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class Loadlobby : Photon.PunBehaviour
{
    private int maxNumPlayerPerRoom = 4;
    public static Mutex mutex;
    private PhotonPeer peer;
    public static int ID = 1;
    public InputField realName;
    public static string room = "room";
    public Text context;
   // public string[] a = { "adam", "bill" };
    // Use this for initialization
    void Start()
    {
       // PhotonNetwork.ConnectUsingSettings("0.0.1");
        mutex = new Mutex();
        //PhotonNetwork.ConnectToMaster("127.0.0.1", 5055, "00c931a2-d860-4946-95f2-8f80849356f5", "0.9");
       // peer = new PhotonPeer(this, ConnectionProtocol.Udp);//Udp又快又稳
       // peer.Connect("127.0.0.1:5055", "LoadBalancing");
       PhotonNetwork.ConnectUsingSettings("0.0.1");
        //PhotonNetwork.connected()
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartMatchingClick()
    {
        PhotonNetwork.playerName = UnityEngine.Random.Range(1, 100000).ToString();
        //int number = PhotonNetwork.playerList.Length;
        //Debug.Log("StartMatchingClick numbers" + number);
      //  Debug.Log("StartMatchingClick " + NUM + " room " + room);
        PhotonNetwork.JoinOrCreateRoom(room, new RoomOptions { MaxPlayers = Convert.ToByte(maxNumPlayerPerRoom) }, null);



        
    }
    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        //PhotonNetwork.CreateRoom(Random.Range(1, 100000).ToString(), roomOptions, TypedLobby.Default);
        mutex.WaitOne();
        ++ID;
        room = room + ID;
        mutex.ReleaseMutex();
        PhotonNetwork.JoinOrCreateRoom(room);

//        int number = PhotonNetwork.playerList.Length;

//        Debug.Log("OnPhotonRandomJoinFailed numbers " + number);
        //base.OnPhotonRandomJoinFailed(codeAndMsg);
    }

    public override void OnCreatedRoom()
    {
        context.text = "Create a new room ...";
        Debug.Log("create room");
        //int number = PhotonNetwork.playerList.Length;

        //Debug.Log("OnCreatedRoom numbers " + number);
        //base.OnCreatedRoom();
    }

    public override void OnJoinedRoom()
    {
        context.text = "waiting for match";
        Debug.Log("join room");
        PhotonNetwork.automaticallySyncScene = true;
        //base.OnJoinedRoom();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        int number = PhotonNetwork.playerList.Length;
        Debug.Log("OnPhotonPlayerConnected numbers" + number);
        if (number % maxNumPlayerPerRoom == 0)
        {
            if (PhotonNetwork.isMasterClient)
            {
                PhotonNetwork.LoadLevel("Launcher");
            }
        }
    }

    public void OnDestroy()
    {
       // PhotonNetwork.LeaveRoom();
        //PhotonNetwork.LeaveLobby();
    }
}
