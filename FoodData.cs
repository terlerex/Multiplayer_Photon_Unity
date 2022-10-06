using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

//Xantos
[CreateAssetMenu(fileName = "FoodData", menuName = "My Game/Food Data")]
[InlineEditor]
public class FoodData : ScriptableObject
{
    [BoxGroup("Basic Infos")]
    [LabelWidth(100)]
    public string foodName;
    [BoxGroup("Basic Infos")]
    [LabelWidth(100)]
    [TextArea]
    public string description;

    [HorizontalGroup("Game Data", 75)]
    [PreviewField(75)]
    [HideLabel]
    public GameObject foodModel;

    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(1, 100)]
    [GUIColor(0.5f, 1f, 0.5f)]
    public int healthRegeneration = 20;
    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(0.5f, 5f)]
    [GUIColor(0.3f, 0.5f, 1f)]
    public float speedRegeneration = 2f;

    [VerticalGroup("Game Data/Stats")]
    [LabelWidth(100)]
    [Range(0, 5)]
    [GUIColor(0.8f, 0.4f, 0.4f)]
    public int damagePoison = 0;
}
