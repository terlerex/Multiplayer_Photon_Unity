using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;
using  Photon.Pun;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    //Vérifier et fonctionnel avec Photon à ne pas toucher. 
    //terlerex 01/09/21 SpawnPlayer
    
    private PhotonView _view; 
    private void Awake()
    {
        _view = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (_view.IsMine)
            CreateController();
    }
    
    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("Player"), Vector3.zero, Quaternion.identity);
    }
    
    
}
