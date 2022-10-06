using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

//terlerex 07/09/2021

[ExecuteAlways]
public class DayNightCycle : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset _preset;
    [SerializeField, UnityEngine.Range(0, 24)] public float timeOfDay;
    [SerializeField] private Text hoursWatch;
    [SerializeField] private GameObject nightBackground;
    [SerializeField] private Text hoursAnim;

    //Singleton
    public static DayNightCycle instance;
    
    private void Awake() {
        if(instance != null)
        {
            Debug.LogWarning("Il y à plus d'une instance de DayNightCycle dans le jeux");
            return;
        }

        instance = this;
    }
    
    //Commencer la partie à 10h
    private void Start()
    {
        hoursAnim.enabled = false;
        nightBackground.SetActive(false);
        timeOfDay = 10;
    }
    
    private void Update()
    {
        if (_preset == null)
                return;
        
        if (Application.isPlaying && SleepSystem.isSleep == false)
        {
            timeOfDay += Time.deltaTime / 60;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay / 24f);
        }
        
        else if(SleepSystem.isSleep)
        {
            nightBackground.SetActive(true);
            hoursAnim.enabled = true;
            timeOfDay += Time.deltaTime;
            timeOfDay %= 24;
            UpdateLighting(timeOfDay / 24f);
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }
            
            
        if (timeOfDay > 7 && timeOfDay < 8)
        {
            SleepSystem.isSleep = false;
            hoursAnim.enabled = false;
            nightBackground.SetActive(false);   
        }
    }
    
    
    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = _preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = _preset.FogColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = _preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) -90f, 170f, 0));
        }
    }
    
    private void OnValidate()
    {
        if(directionalLight != null)
            return;
        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
    
    //Sync timeOfDay with Photon
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(timeOfDay);
        }
        else
        {
            timeOfDay = (float)stream.ReceiveNext();
        }
    }
}
