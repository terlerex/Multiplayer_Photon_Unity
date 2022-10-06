using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class TextLookAt : MonoBehaviour
{
    [SerializeField] private Transform _text;
    
    private void Update()
    {
        _text.forward = Camera.main.transform.forward;
        // _text.transform.localRotation = Quaternion.LookRotation((_text.position - Camera.main.transform.position).normalized);
    }
}
