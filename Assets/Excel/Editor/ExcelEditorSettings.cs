using System;
using System.Collections.Generic;

public class ExcelEditorSettings
{
    //
    public static string exportSoPath;
    //
    public static string excelPath;
    //导出的脚本的目录
    //public const string exportScriptPath = "Assets/Terasurware/Classes/";

    //导出的编辑器脚本的目录
    //public static string exportEditorScriptPath = "Assets/Terasurware/Classes/Editor/";

    public const string entityTemplate = @"
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace ConfigData
{
    [System.SerializableAttribute]
    public class $Class$
    {
$Types$
    }

    public class $ScriptableObject$ : ScriptableObject
    {	
        public List<$Class$> dataList = new List<$Class$>();
        public static List<$Class$> Read(string filePath)
        {
            List<$Class$> rnt = new List<$Class$>();
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(stream);
            sr.ReadLine();//注释
            sr.ReadLine();//类型
            sr.ReadLine();//名称
            string lineData = sr.ReadLine();
            while (lineData != null)
            {
                var p = new $Class$();
                string[] splits = lineData.Split('\t');
    $EXPORT_DATA$

                rnt.Add(p);
                lineData = sr.ReadLine();
            }
            return rnt;
        }
    }
}";

    public const string exportTemplate = @"
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using ConfigData;
public class $FileName$_importer : TxtImporter
{
    private static readonly string filePath = ""$ExcelPath$"";
    static readonly string exportName = ""$ScriptableObject$"";

    public static void Import()
    {

            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(stream);
                var exportPath = ""Assets/Excel/GenerateSo/"" + ""$ScriptableObject$"" + "".asset"";

                // check scriptable object
                var data = ($ScriptableObject$)AssetDatabase.LoadAssetAtPath(exportPath, typeof($ScriptableObject$));
                if (data == null)
                {
                    data = ScriptableObject.CreateInstance<$ScriptableObject$>();
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
                    var p = new ConfigData.$ClassName$();
                    string[] splits = lineData.Split('\t');
$EXPORT_DATA$

                    data.dataList.Add(p);
                    lineData = sr.ReadLine();
                }
                // save scriptable object
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty(obj);
            }
        
    }
}";


}
