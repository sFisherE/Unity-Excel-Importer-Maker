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

    public static bool[] GetDataCellBoolArray(string[] cells, int index)
    {
        if (cells.Length <= index)
        {
            return new bool[0];
        }
        string[] splits=cells[index].Split('|');
        bool[] rtn=new bool[splits.Length];
        for (int i = 0; i < rtn.Length;i++ )
        {
            rtn[i] = bool.Parse(splits[i]);
        }
        return rtn;
    }
    public static int[] GetDataCellIntArray(string[] cells, int index)
    {
        if (cells.Length <= index)
        {
            return new int[0];
        }
        string[] splits = cells[index].Split('|');
        int[] rtn = new int[splits.Length];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = int.Parse(splits[i]);
        }
        return rtn;
    }
    public static float[] GetDataCellFloatArray(string[] cells, int index)
    {
        if (cells.Length <= index)
        {
            return new float[0];
        }
        string[] splits = cells[index].Split('|');
        float[] rtn = new float[splits.Length];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = float.Parse(splits[i]);
        }
        return rtn;
    }

    public static string[] GetDataCellStringArray(string[] cells, int index)
    {
        if (cells.Length <= index)
        {
            return new string[0];
        }
        string[] splits = cells[index].Split('|');
        string[] rtn = new string[splits.Length];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = splits[i];
        }
        return rtn;
    }

    public static T GetDataCell<T>(string[] cells, int index)
    {
        if (cells.Length<=index)
        {
            return default(T);
        }
        //Debug.Log(cells[index] + " " + typeof(T));

        return (T)Convert.ChangeType(cells[index], typeof(T));


        //if (typeof(T) == typeof(int))
        //{
        //        return (T)Convert.ChangeType(cells[index], typeof(T));
        //}
        //else if (typeof(T) == typeof(float))
        //{
        //    return (T)Convert.ChangeType(cells[index], typeof(T));
        //}
        //else if (typeof(T) == typeof(string))
        //{
        //    return (T)Convert.ChangeType(cells[index], typeof(T));
        //}
        //else if (typeof(T) == typeof(int[]))
        //{
        //    return (T)Convert.ChangeType(cells[index], typeof(T));
        //}


    }
}
