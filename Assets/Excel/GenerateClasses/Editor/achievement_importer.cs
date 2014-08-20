
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class achievement_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Excel/TxtData/achievement.txt";
    static readonly string exportName = "achievement_list";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(stream);
                var exportPath = "Assets/Excel/GenerateSo/" + "achievement_list" + ".asset";

                // check scriptable object
                var data = (achievement_list)AssetDatabase.LoadAssetAtPath(exportPath, typeof(achievement_list));
                if (data == null)
                {
                    data = ScriptableObject.CreateInstance<achievement_list>();
                    AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                    data.hideFlags = HideFlags.NotEditable;
                }
                data.dataList.Clear();

                sr.ReadLine();//注释
                sr.ReadLine();//类型
                sr.ReadLine();//名称

                string lineData=sr.ReadLine();
                while (lineData != null)
                {
                    var p = new achievement_list.achievement();
                    string[] splits = lineData.Split('\t');


					p.id =ExcelEditorTools.GetDataCell<int>(splits,0);
					p.icon =ExcelEditorTools.GetDataCell<string>(splits,1);
					p.lv =ExcelEditorTools.GetDataCell<int>(splits,2);
					p.taskname =ExcelEditorTools.GetDataCell<string>(splits,3);
					p.describe =ExcelEditorTools.GetDataCell<string>(splits,4);
					p.type =ExcelEditorTools.GetDataCell<int>(splits,5);
					p.condition =ExcelEditorTools.GetDataCell<string>(splits,6);
					p.gold =ExcelEditorTools.GetDataCell<int>(splits,7);
					p.silver =ExcelEditorTools.GetDataCell<int>(splits,8);
					p.vigour =ExcelEditorTools.GetDataCell<int>(splits,9);
					p.Stamina =ExcelEditorTools.GetDataCell<int>(splits,10);
					p.exp =ExcelEditorTools.GetDataCell<int>(splits,11);
					p.rewards =ExcelEditorTools.GetDataCellStringArray(splits,12);
					p.chainid =ExcelEditorTools.GetDataCell<int>(splits,13);
					p.chainseq =ExcelEditorTools.GetDataCell<int>(splits,14);

                    data.dataList.Add(p);
                    lineData = sr.ReadLine();
                }
                // save scriptable object
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty(obj);
            }
        }
    }
}
