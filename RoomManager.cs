using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    //Vérifier et fonctionnel avec Photon à ne pas toucher. 
    //terlerex 02/09/21 RoomManager 
    
    
    [SerializeField] private GameObject startButton;
    [SerializeField] private Text roomId;
    
    public static RoomManager Instance;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }
    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    public override void OnJoinedRoom()
    {
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartGames()
    {
        PhotonNetwork.LoadLevel(3);
    }

    private void Start()
    {
        roomId.text = PhotonNetwork.CurrentRoom.Name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 3)
        {
            PhotonNetwork.Instantiate(Path.Combine("SpawnPlayers"), Vector3.zero, Quaternion.identity);
        }
    }
}
