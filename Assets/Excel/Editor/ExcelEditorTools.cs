using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.CodeDom.Compiler;
using System.IO;
using System.Diagnostics;
using System.Reflection;
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
    [MenuItem("Assets/Excel/Import Selected txt")]
    static void ImportSelectedTxt()
    {
        //bool importSingleTxt = false;
        //string fileName = "";
        //UnityEngine.Object selected = Selection.activeObject;
        //if (selected!=null)
        //{
        //    string path = AssetDatabase.GetAssetPath(selected);
        //    fileName = Path.GetFileName(path);
        //    string ext = Path.GetExtension(path);
        //    fileName = fileName.Substring(0, fileName.Length - ext.Length);
        //    Debug.Log(fileName + " " + path);
        //    if (ext == ".txt")
        //    {
        //        importSingleTxt = true;
        //    }
        //}
        List<string> classNames = new List<string>();
        foreach (var item in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(item);
            string fileName = Path.GetFileName(path);
            string ext = Path.GetExtension(path);

            if (ext == ".txt")
            {
                //importSingleTxt = true;
                fileName = fileName.Substring(0, fileName.Length - ext.Length);
                //Debug.Log(fileName + " " + path);
                classNames.Add(fileName + "_importer");
            }
        }

        foreach (var item in classNames)
        {
            Debug.Log(item);
        }

        Assembly[]  assemblies=AppDomain.CurrentDomain.GetAssemblies();
        Assembly find = null;
        foreach (var item in assemblies)
        {
            if (item.FullName.Contains("Assembly-CSharp-Editor"))
            {
                find = item;
                break;
            }
        }
        Type[] types = find.GetTypes();

        //if (!string.IsNullOrEmpty(fileName))
        //{
            //string className = fileName + "_importer";

            foreach (var item in types)
            {
                if (item.IsSubclassOf(typeof(TxtImporter)))
                {
                    if (classNames.Contains(item.Name))
                    {
                    //}
                    //if (item.Name == className)
                    //{
                        Debug.Log("InvokeMember " + item.Name);
                        item.InvokeMember("Import", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new object[] { });
                    }
                }
            }
        //}


        //if (!importSingleTxt)
        //{
        //    foreach (var item in types)
        //    {
        //        if (item.IsSubclassOf(typeof(TxtImporter)))
        //            item.InvokeMember("Import", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new object[] { });
        //    }
        //}
        //else
        //{
        //    foreach (var item in types)
        //    {
        //        if (item.IsSubclassOf(typeof(TxtImporter)))
        //        {

        //            item.InvokeMember("Import", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new object[] { });
        //        }
        //    }
        //}

    }
    [MenuItem("Assets/Excel/Import All txt")]
    static void ImportTxt()
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        Assembly find = null;
        foreach (var item in assemblies)
        {
            if (item.FullName.Contains("Assembly-CSharp-Editor"))
            {
                find = item;
                break;
            }
        }
        Type[] types = find.GetTypes();

        foreach (var item in types)
        {
            if (item.IsSubclassOf(typeof(TxtImporter)))
                item.InvokeMember("Import", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, new object[] { });
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
