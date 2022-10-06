#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
public class FoodDataEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Food Data")]
    private static void OpenWindow()
    {
        GetWindow<FoodDataEditor>().Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        tree.Add("Create New", new CreateNewFoodData());
        tree.AddAllAssetsAtPath("Food Data", "Assets/Scripts/ScriptableObject", typeof(FoodData));
        return tree;
    }

    public class CreateNewFoodData
    {
        public CreateNewFoodData()
        {
            foodData = ScriptableObject.CreateInstance<FoodData>();
            foodData.foodName = "New Food Data";
        }

        [InlineEditor(Expanded = true)]
        public FoodData foodData;

        [Button("Add New Food SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(foodData, "Assets/Scripts/ScriptableObject" + foodData.foodName + ".asset");
            AssetDatabase.SaveAssets();
        }
    }

    protected override void OnBeginDrawEditors()
    {
        OdinMenuTreeSelection selected = this.MenuTree.Selection;

        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();

            if (SirenixEditorGUI.ToolbarButton("Delete Current"))
            {
                FoodData asset = selected.SelectedValue as FoodData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }

        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}
#endif