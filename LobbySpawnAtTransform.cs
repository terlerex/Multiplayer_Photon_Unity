using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LobbySpawnAtTransform : MonoBehaviour, IInRoomCallbacks
{
    private List<GameObject> currentList = new List<GameObject>();
    [SerializeField] private GameObject lobbyPlayerPrefab;
    [SerializeField] private List<GameObject> lstLobbyTransform = new List<GameObject>(4);
    
    private void Awake()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Start()
    {
        UpdatePlayers();
    }
    
    void UpdatePlayers()
    {
        foreach (var obj in currentList)
        {
            Destroy(obj);
        }
        
        currentList.Clear();
        
        for (var index = 0; index < PhotonNetwork.PlayerList.Length; index++)
        {
            var _player = PhotonNetwork.PlayerList[index];
            var obj = Instantiate(lobbyPlayerPrefab, lstLobbyTransform[index].transform.position, Quaternion.identity);
            currentList.Add(obj);
        }
    }
    
    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayers();
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayers();
    }

    public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {
        
    }
}
