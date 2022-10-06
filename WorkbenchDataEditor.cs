#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
public class WorkbenchDataEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Workbench Data")]
    private static void OpenWindow()
    {
        GetWindow<WorkbenchDataEditor>().Show();
    }

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;

        tree.Add("Create New", new CreateNewWorkbenchData());
        tree.AddAllAssetsAtPath("Workbench Data", "Assets/Scripts/ScriptableObject", typeof(WorkbenchData));
        return tree;
    }

    public class CreateNewWorkbenchData
    {
        public CreateNewWorkbenchData()
        {
            WorkbenchData = ScriptableObject.CreateInstance<WorkbenchData>();
            WorkbenchData.WorkbenchName = "New Workbench Data";
        }

        [InlineEditor(Expanded = true)]
        public WorkbenchData WorkbenchData;

        [Button("Add New Workbench SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(WorkbenchData, "Assets/Scripts/ScriptableObject" + WorkbenchData.WorkbenchName + ".asset");
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
                WorkbenchData asset = selected.SelectedValue as WorkbenchData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }

        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }
}
#endif
