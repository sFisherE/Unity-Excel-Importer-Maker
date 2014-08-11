using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using System.Xml.Serialization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class param1_importer : AssetPostprocessor {
	private static readonly string filePath = "Assets/ExcelData/param1.xls";
	private static readonly string exportPath = "Assets/ExcelData/param1.asset";
	private static readonly string[] sheetNames = { "item1", };
	
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		foreach (string asset in importedAssets) {
			if (!filePath.Equals (asset))
				continue;
				
			param1 data = (param1)AssetDatabase.LoadAssetAtPath (exportPath, typeof(param1));
			if (data == null) {
				data = ScriptableObject.CreateInstance<param1> ();
				AssetDatabase.CreateAsset ((ScriptableObject)data, exportPath);
				data.hideFlags = HideFlags.NotEditable;
			}
			
			data.sheets.Clear ();
			using (FileStream stream = File.Open (filePath, FileMode.Open, FileAccess.Read)) {
				IWorkbook book = new HSSFWorkbook (stream);
				
				foreach(string sheetName in sheetNames) {
					ISheet sheet = book.GetSheet(sheetName);
					if( sheet == null ) {
						Debug.LogError("[QuestData] sheet not found:" + sheetName);
						continue;
					}

					param1.Sheet s = new param1.Sheet ();
					s.name = sheetName;
				
					for (int i=1; i< sheet.LastRowNum; i++) {
						IRow row = sheet.GetRow (i);
						ICell cell = null;
						
						param1.Param p = new param1.Param ();
						
					cell = row.GetCell(0); p.ID = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(1); p.string_data = (cell == null ? "" : cell.StringCellValue);
					cell = row.GetCell(2); p.int_data = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(3); p.double_data = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(4); p.bool_data = (cell == null ? false : cell.BooleanCellValue);
					cell = row.GetCell(5); p.math_1 = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(6); p.math_2 = (cell == null ? "" : cell.StringCellValue);
					p.array = new double[2];
					cell = row.GetCell(7); p.array[0] = (cell == null ? 0.0 : cell.NumericCellValue);
					cell = row.GetCell(8); p.array[1] = (cell == null ? 0.0 : cell.NumericCellValue);
						s.list.Add (p);
					}
					data.sheets.Add(s);
				}
			}

			ScriptableObject obj = AssetDatabase.LoadAssetAtPath (exportPath, typeof(ScriptableObject)) as ScriptableObject;
			EditorUtility.SetDirty (obj);
		}
	}
}
