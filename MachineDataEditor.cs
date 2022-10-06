#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
public class MachineDataEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Machine Data")]
    private static void OpenWindow()
    {
        GetWindow<MachineDataEditor>().Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        tree.Add("Create New", new CreateNewMachineData());
        tree.AddAllAssetsAtPath("Machine Data", "Assets/Scripts/ScriptableObject", typeof(MachinData));
        return tree;
    }

    public class CreateNewMachineData
    {
        public CreateNewMachineData()
        {
            MachinData = ScriptableObject.CreateInstance<MachinData>();
            MachinData.machineName = "New Machine Data";
        }

        [InlineEditor(Expanded = true)]
        public MachinData MachinData;

        [Button("Add New Machine SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(MachinData, "Assets/Scripts/ScriptableObject" + MachinData.machineName + ".asset");
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
                MachinData asset = selected.SelectedValue as MachinData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }

        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}
#endif
