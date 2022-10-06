using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using static Photon.Pun.PhotonNetwork;

//terlerex 09/09/21 SleepSystem

public class SleepSystem : MonoBehaviour
{
    [Header("Sleep logics")]
    [SerializeField] private KeyCode _sleepInput = KeyCode.E;
    public static bool isSleep;
    public GameObject _pressText;
    
    [Header("Text")]
    [SerializeField] private Text cantSleep;
    [SerializeField] private Text cantSleepNotMasterClient;
    
    [Header("Photon")]
    private static Player _player = LocalPlayer;

    private void Start()
    {
        //Disable text at start
        cantSleepNotMasterClient.enabled = false;
        cantSleep.enabled = false;
        isSleep = false;
        _pressText.SetActive(false);
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _pressText.SetActive(true);
            if (Input.GetKey(_sleepInput) && !isSleep && DayNightCycle.instance.timeOfDay < 7 && _player.IsMasterClient
                || DayNightCycle.instance.timeOfDay > 19 && Input.GetKey(_sleepInput) && !isSleep  && _player.IsMasterClient)
            {
                Debug.Log(DayNightCycle.instance.timeOfDay + " H, il est l'heure de dormir");
                isSleep = true;
            }
            else if (Input.GetKey(_sleepInput) && !isSleep && DayNightCycle.instance.timeOfDay > 7
                     || DayNightCycle.instance.timeOfDay < 19 && Input.GetKey(_sleepInput) && !isSleep)
            {
                //Check if the player is a Masterclient (Host)
                if(_player.IsMasterClient)
                    StartCoroutine(CantSleep());
                
                if (!_player.IsMasterClient)
                    StartCoroutine(CantSleepNotMasterClient());

            }
        }
    }
    
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
            _pressText.SetActive(false);
    }

    
    //Error messages (Can't sleep)
    IEnumerator CantSleep()
    {
        cantSleep.enabled = true;
        yield return new WaitForSeconds(3);
        cantSleep.enabled = false;
    }
    
    IEnumerator CantSleepNotMasterClient()
    {
        cantSleepNotMasterClient.enabled = true;
        yield return new WaitForSeconds(3);
        cantSleepNotMasterClient.enabled = false;
    }
    
}
