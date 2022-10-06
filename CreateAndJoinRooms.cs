using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    //Vérifier et fonctionnel avec Photon à ne pas toucher. 
    //terlerex 01/09/21 CreateAndJoinRooms
    
    public InputField joinInput;
    
    public void CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.PublishUserId = true;
        PhotonNetwork.CreateRoom(RoomCode, options);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text); 
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("LobbyScene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.OfflineMode = true;
        PhotonNetwork.CreateRoom(RoomCode);
    }

    public void Singleplayer()
    {
        PhotonNetwork.Disconnect();
    }
    

    public string roomCode = "BlackHoles";
    public string RoomCode
    {
        get
        {
            int value = Random.Range(0, 10);
            int value2 = Random.Range(0, 10);
            return roomCode + value.ToString() + value2.ToString();
        }
    }
}
