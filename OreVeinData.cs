using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//Xantos
[CreateAssetMenu(fileName = "OreVeinData", menuName = "My Game/OreVein Data")]
[InlineEditor]
public class OreVeinData : ScriptableObject
{
    [BoxGroup("Basic Info")]
    [LabelWidth(100)]
    public string OreVeinName;
    [BoxGroup("Basic Info")]
    [LabelWidth(100)]
    [TextArea]
    public string description;

    [HorizontalGroup("Game Data", 75)]
    [PreviewField(75)]
    [HideLabel]
    public GameObject OreVeinModel;

    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(150, 10000)]
    [GUIColor(0.5f, 1f, 0.5f)]
    public int Kg = 20;
    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(0.0f, 1f)]
    [GUIColor(0.3f, 0.5f, 1f)]
    public float MiningEfficiency = 0.2f;
}
