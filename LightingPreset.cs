using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//terlerex 07/09/2021

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Scriptable/Lighting Preset", order = 1)]
public class LightingPreset : ScriptableObject
{
    public Gradient AmbientColor;
    public Gradient DirectionalColor;
    public Gradient FogColor;
}
