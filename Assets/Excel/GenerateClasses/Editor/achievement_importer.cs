
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using ConfigData;
public class achievement_importer : TxtImporter
{
    private static readonly string filePath = "Assets/Excel/TxtData/achievement.txt";
    static readonly string exportName = "achievement_list";

    public static void Import()
    {

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
                    var p = new ConfigData.achievement();
                    string[] splits = lineData.Split('\t');

			p.id =ExcelTools.GetDataCell<int>(splits,0,"id");
			p.icon =ExcelTools.GetDataCell<string>(splits,1,"icon");
			p.lv =ExcelTools.GetDataCell<int>(splits,2,"lv");
			p.taskname =ExcelTools.GetDataCell<string>(splits,3,"taskname");
			p.describe =ExcelTools.GetDataCell<string>(splits,4,"describe");
			p.type =ExcelTools.GetDataCell<int>(splits,5,"type");
			p.condition =ExcelTools.GetDataCell<string>(splits,6,"condition");
			p.gold =ExcelTools.GetDataCell<int>(splits,7,"gold");
			p.silver =ExcelTools.GetDataCell<int>(splits,8,"silver");
			p.vigour =ExcelTools.GetDataCell<int>(splits,9,"vigour");
			p.Stamina =ExcelTools.GetDataCell<int>(splits,10,"Stamina");
			p.exp =ExcelTools.GetDataCell<int>(splits,11,"exp");
			p.rewards =ExcelTools.GetDataCell<string>(splits,12,"rewards");
			p.chainid =ExcelTools.GetDataCell<int>(splits,13,"chainid");
			p.chainseq =ExcelTools.GetDataCell<int>(splits,14,"chainseq");

                    data.dataList.Add(p);
                    lineData = sr.ReadLine();
                }
                // save scriptable object
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty(obj);
            }
        
    }
}