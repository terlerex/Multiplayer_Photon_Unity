using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//Xantos
[CreateAssetMenu(fileName = "ItemData", menuName = "My Game/Item Data")]
[InlineEditor]
public class ItemData : ScriptableObject
{
    [BoxGroup("Basic Infos", centerLabel: true)]
    [LabelWidth(100)]
    public string ItemName;
    [BoxGroup("Basic Infos", centerLabel: true)]
    [LabelWidth(100)]
    [TextArea]
    public string description;

    [HorizontalGroup("Game Data", 75)]
    [PreviewField(75)]
    [HideLabel]
    public GameObject ItemModel;

    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(0, 100)]
    [GUIColor(0.5f, 1f, 0.5f)]
    public int IdItem = 0;
}
