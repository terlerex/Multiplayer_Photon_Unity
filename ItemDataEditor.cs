#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
public class ItemDataEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Item Data")]
    private static void OpenWindow()
    {
        GetWindow<ItemDataEditor>().Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        tree.Add("Create New", new CreateNewItemData());
        tree.AddAllAssetsAtPath("Item Data", "Assets/Scripts/ScriptableObject", typeof(ItemData));
        return tree;
    }

    public class CreateNewItemData
    {
        public CreateNewItemData()
        {
            itemData = ScriptableObject.CreateInstance<ItemData>();
            itemData.ItemName = "New Item Data";
        }

        [InlineEditor(Expanded = true)]
        public ItemData itemData;

        [Button("Add New Item SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(itemData, "Assets/Scripts/ScriptableObject" + itemData.ItemName + ".asset");
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
                ItemData asset = selected.SelectedValue as ItemData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }

        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}
#endif
