using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    //Vérifier et fonctionnel avec Photon à ne pas toucher. 
    //terlerex 01/09/21 ConnectToServer
    
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("MenuScene");
        PhotonNetwork.NickName = "Player" + Random.Range(0, 10000).ToString("00000");
        Debug.Log(PhotonNetwork.NickName);
    }
}
