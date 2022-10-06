using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//Xantos
[CreateAssetMenu(fileName = "WorkbenchData", menuName = "My Game/Workbench Data")]
[InlineEditor]
public class WorkbenchData : ScriptableObject
{
    [BoxGroup("Basic Info")]
    [LabelWidth(100)]
    public string WorkbenchName;
    [BoxGroup("Basic Info")]
    [LabelWidth(100)]
    [TextArea]
    public string description;

    [HorizontalGroup("Game Data", 75)]
    [PreviewField(75)]
    [HideLabel]
    public GameObject WorkbenchModel;

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
    public GameObject[] productItem;

    [VerticalGroup("Game Data Item/Stats")]
    [LabelWidth(100)]
    [Range(0, 5)]
    [GUIColor(0.8f, 0.4f, 0.4f)]
    public int speedWorking = 0;

    [VerticalGroup("Game Data Item/Stats")]
    [LabelWidth(100)]
    [GUIColor(0.5f, 1f, 1f)]
    public Transform[] pointSpawn;

    [HorizontalGroup("Game Data Item use/Stats", 75)]
    [PreviewField(75)]
    [HideLabel]
    public ItemData[] ItemUse;
}
