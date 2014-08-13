using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.CodeDom.Compiler;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;
class ExcelEditorTools
{
    static string projectPath
    {
        get
        {
            return Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length);
        }
    }
    [MenuItem("Assets/Excel/Gen txt")]
    static void GenTxt()
    {
        foreach (Object obj in Selection.objects)
        {
            string filePath = AssetDatabase.GetAssetPath(obj);
            if (Path.GetExtension(filePath) == ".xlsx")
            {
                string path = Path.Combine(projectPath, "Excel/NPOI_Txt.exe");
                Process proc = new Process();
                proc.StartInfo.FileName = path;
                string argu = projectPath + "/" + filePath+" " + Application.dataPath + "/Excel/TxtData";
                proc.StartInfo.Arguments = argu;
                proc.Start();
            }
        }
    }
}
