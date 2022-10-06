using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//Xantos
[CreateAssetMenu(fileName = "MachineData", menuName = "My Game/Machine Data")]
[InlineEditor]
public class MachinData : ScriptableObject
{
    [BoxGroup("Basic Infos", centerLabel:true)]
    [LabelWidth(100)]
    public string machineName;
    [BoxGroup("Basic Infos", centerLabel: true)]
    [LabelWidth(100)]
    [TextArea]
    public string description;

    [HorizontalGroup("Game Data", 75)]
    [PreviewField(75)]
    [HideLabel]
    public GameObject machineModel;

    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(1, 100)]
    [GUIColor(0.5f, 1f, 0.5f)]
    public int energyNeed = 20;

    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(0.5f, 5f)]
    [GUIColor(0.3f, 0.5f, 1f)]
    public float consommation = 2f;

    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(0, 5)]
    [GUIColor(0.5f, 1f, 1f)]
    public int LevelMachine = 0;

    [HorizontalGroup("Game Data Item", 75)]
    [PreviewField(75)]
    [HideLabel]
    public GameObject productItem;

    [VerticalGroup("Game Data Item/Stats")]
    [LabelWidth(100)]
    [Range(0, 5)]
    [GUIColor(0.8f, 0.4f, 0.4f)]
    public int speedWorking = 0;

    [VerticalGroup("Game Data Item/Stats")]
    [LabelWidth(100)]
    [GUIColor(0.5f, 1f, 1f)]
    public Transform[] pointSpawn;
}
