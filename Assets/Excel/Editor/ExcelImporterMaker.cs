using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
//using NPOI.SS.UserModel;
//using NPOI.HSSF.UserModel;
//using NPOI.XSSF.UserModel;
//using Excel;
//using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.CodeDom.Compiler;
using System.IO;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
/// <summary>
///   excel基本结构：
///   row0：注释
///   row1：字段类型
///   row2：字段名字
///   row3~：数据
/// </summary>
/// 我只处理xlsx类型的文件
public class ExcelProcesser : EditorWindow
{
    private string filePath = string.Empty;
    private string fileName = string.Empty;
    private static string s_key_prefix = "excel-importer-maker.";

    //string fileName;
    string scriptableObjectName
    {
        get
        {
            return fileName + "_list";
        }
    }
    private Vector2 curretScroll = Vector2.zero;


    void OnGUI()
    {
        GUILayout.Label("makeing importer", EditorStyles.boldLabel);
        //className = EditorGUILayout.TextField("class name", className);
        EditorGUILayout.LabelField("class name", scriptableObjectName);
        if (GUILayout.Button("Create"))
        {
            EditorPrefs.SetString(s_key_prefix + fileName + ".className", fileName);
            ExportEntity();
            ExportImporter();

            AssetDatabase.ImportAsset(filePath);
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            Close();
        }


        EditorGUILayout.LabelField("parameter settings");
        curretScroll = EditorGUILayout.BeginScrollView(curretScroll);
        EditorGUILayout.BeginVertical("box");
        string lastCellName = string.Empty;
        foreach (ExcelRowParameter cell in typeList)
        {
            GUILayout.BeginHorizontal();
            cell.type = EditorGUILayout.TextField(cell.type);
            cell.name = EditorGUILayout.TextField(cell.name);
            EditorGUILayout.LabelField(cell.comment);
            GUILayout.EndHorizontal();

            //EditorGUILayout.EndToggleGroup();
            //lastCellName = cell.name;
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();

    }

    [MenuItem("Assets/Excel XLSX Import Settings...")]
    static void ExportExcelToAssetbundle()
    {
        foreach (Object obj in Selection.objects)
        {
            var window = ScriptableObject.CreateInstance<ExcelProcesser>();
            window.filePath = AssetDatabase.GetAssetPath(obj);
            window.fileName = Path.GetFileNameWithoutExtension(window.filePath);
            using (FileStream stream = File.Open(window.filePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(stream);
                string line1 = sr.ReadLine();
                //Debug.Log(line);
                string[] splits1 = line1.Split('\t');
                string line2 = sr.ReadLine();
                string[] splits2 = line2.Split('\t');
                string line3 = sr.ReadLine();
                string[] splits3 = line3.Split('\t');

                string data = sr.ReadLine();
                string[] dataSplit = data.Split('\t');
                for (int i = 0; i < splits1.Length; i++)
                {
                    ExcelRowParameter parser = new ExcelRowParameter();
                    parser.comment = splits1[i];
                    parser.type = splits2[i];
                    bool isArray = dataSplit[i].Split('|').Length > 1;
                    bool isFloat = dataSplit[i].Contains('.');
                    switch (parser.type)
                    {
                        case "int"://策划有可能填错
                            if (isArray && isFloat)
                                parser.type = "float[]";
                            else if (isArray)
                                parser.type = "int[]";
                            else if (isFloat)
                                parser.type = "float";
                            break;
                        case "float":
                            if (isArray)
                                parser.type = "float[]";
                            break;
                        case "string":
                            if (isArray)
                                parser.type = "string[]";
                            break;
                    }
                    parser.name = splits3[i];

                    window.typeList.Add(parser);
                }
            }

            window.Show();

        }
    }
    private List<ExcelRowParameter> typeList = new List<ExcelRowParameter>();

    void ExportEntity()
    {
        string entityTemplate = ExcelEditorSettings.entityTemplate;
        StringBuilder builder = new StringBuilder();
        foreach (ExcelRowParameter row in typeList)
        {
            builder.AppendLine();
            builder.AppendFormat("		public {0} {1};//{2}", row.type.ToLower(), row.name, row.comment);
        }

        entityTemplate = entityTemplate.Replace("$Types$", builder.ToString());
        entityTemplate = entityTemplate.Replace("$Class$", fileName);
        entityTemplate = entityTemplate.Replace("$ScriptableObject$", scriptableObjectName);

        Directory.CreateDirectory("Assets/Excel/GenerateClasses/");
        File.WriteAllText("Assets/Excel/GenerateClasses/" + scriptableObjectName + ".cs", entityTemplate);
    }
    void ExportImporter()
    {
        string importerTemplate = ExcelEditorSettings.exportTemplate;

        StringBuilder builder = new StringBuilder();
        StringBuilder sheetListbuilder = new StringBuilder();
        string tab = "					";

        int rowCount = 0;
        foreach (ExcelRowParameter row in typeList)
        {
            builder.AppendLine();
            switch (row.type)
            {
                case "bool":
                    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCell<bool>(splits,{1});", row.name, rowCount);
                    break;
                //case "double":
                //    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCell<bool>(splits,{1});", row.name, rowCount);
                //    //builder.AppendFormat(tab + "if (!string.IsNullOrEmpty(splits[{1}])) p.{0} = double.Parse(splits[{1}]);", row.name, rowCount);
                //    //builder.AppendFormat(tab + "cell = row.GetCell({1}); cell.SetCellType(CellType.Numeric);p.{0} = (cell == null ? 0.0 : cell.NumericCellValue);", row.name, rowCount);
                //    break;
                case "int":
                    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCell<int>(splits,{1});", row.name, rowCount);
                    //builder.AppendFormat(tab + "if (!string.IsNullOrEmpty(splits[{1}])) p.{0} = int.Parse(splits[{1}]);", row.name, rowCount);
                    //builder.AppendFormat(tab + "cell = row.GetCell({1}); cell.SetCellType(CellType.Numeric);p.{0} = (int)(cell == null ? 0 : cell.NumericCellValue);", row.name, rowCount);
                    break;
                case "float":
                    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCell<float>(splits,{1});", row.name, rowCount);
                    //builder.AppendFormat(tab + "if (!string.IsNullOrEmpty(splits[{1}])) p.{0} = float.Parse(splits[{1}]);", row.name, rowCount);
                    //builder.AppendFormat(tab + "cell = row.GetCell({1}); cell.SetCellType(CellType.Numeric);p.{0} = (float)(cell == null ? 0 : cell.NumericCellValue);", row.name, rowCount);
                    break;
                case "string":
                    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCell<string>(splits,{1});", row.name, rowCount);
                    //builder.AppendFormat(tab + "if (!string.IsNullOrEmpty(splits[{1}])) p.{0} = splits[{1}];", row.name, rowCount);
                    //builder.AppendFormat(tab + "cell = row.GetCell({1}); cell.SetCellType(CellType.String);p.{0} = (cell == null ? \"\" : cell.StringCellValue);", row.name, rowCount);
                    break;
                case "bool[]":
                    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCellBoolArray(splits,{1});", row.name, rowCount);
                    break;
                case "int[]":
                    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCellIntArray(splits,{1});", row.name, rowCount);
                    break;
                case "float[]":
                    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCellFloatArray(splits,{1});", row.name, rowCount);
                    break;
                case "string[]":
                    builder.AppendFormat(tab + "p.{0} =ExcelEditorTools.GetDataCellStringArray(splits,{1});", row.name, rowCount);
                    break;
            }
            rowCount += 1;
        }

        importerTemplate = importerTemplate.Replace("$ExcelPath$", filePath);
        importerTemplate = importerTemplate.Replace("$ScriptableObject$", scriptableObjectName);
        importerTemplate = importerTemplate.Replace("$ClassName$", fileName);
        importerTemplate = importerTemplate.Replace("$FileName$", fileName);
        importerTemplate = importerTemplate.Replace("$EXPORT_DATA$", builder.ToString());

        Directory.CreateDirectory("Assets/Excel/GenerateClasses/Editor/");
        File.WriteAllText("Assets/Excel/GenerateClasses/Editor/" + fileName + "_importer.cs", importerTemplate);
    }

    private class ExcelRowParameter
    {
        public string comment;
        public string type;
        public string name;
    }
}
