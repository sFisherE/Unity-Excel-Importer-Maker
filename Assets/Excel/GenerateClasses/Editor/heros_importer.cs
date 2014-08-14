
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

public class heros_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Excel/TxtData/heros.txt";
    static readonly string exportName = "heros_list";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                StreamReader sr = new StreamReader(stream);
                var exportPath = "Assets/Excel/GenerateSo/" + "heros_list" + ".asset";

                // check scriptable object
                var data = (heros_list)AssetDatabase.LoadAssetAtPath(exportPath, typeof(heros_list));
                if (data == null)
                {
                    data = ScriptableObject.CreateInstance<heros_list>();
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
                    var p = new heros_list.heros();
                    string[] splits = lineData.Split('\t');


					p.id =ExcelEditorTools.GetDataCell<int>(splits,0);
					p.hname =ExcelEditorTools.GetDataCell<string>(splits,1);
					p.icon =ExcelEditorTools.GetDataCell<string>(splits,2);
					p.cardpic =ExcelEditorTools.GetDataCell<string>(splits,3);
					p.modelprefab =ExcelEditorTools.GetDataCell<string>(splits,4);
					p.countlevel =ExcelEditorTools.GetDataCell<int>(splits,5);
					p.htype =ExcelEditorTools.GetDataCell<int>(splits,6);
					p.tavern =ExcelEditorTools.GetDataCell<int>(splits,7);
					p.camp =ExcelEditorTools.GetDataCell<int>(splits,8);
					p.hp =ExcelEditorTools.GetDataCell<int>(splits,9);
					p.mp =ExcelEditorTools.GetDataCell<int>(splits,10);
					p.attack =ExcelEditorTools.GetDataCell<int>(splits,11);
					p.defence =ExcelEditorTools.GetDataCell<int>(splits,12);
					p.speed =ExcelEditorTools.GetDataCell<int>(splits,13);
					p.hpgrow =ExcelEditorTools.GetDataCell<float>(splits,14);
					p.mpgrow =ExcelEditorTools.GetDataCell<float>(splits,15);
					p.attackgrow =ExcelEditorTools.GetDataCell<float>(splits,16);
					p.defencegrow =ExcelEditorTools.GetDataCell<float>(splits,17);
					p.speedgrow =ExcelEditorTools.GetDataCell<float>(splits,18);
					p.skillmodi =ExcelEditorTools.GetDataCell<float>(splits,19);
					p.hit =ExcelEditorTools.GetDataCell<int>(splits,20);
					p.dodge =ExcelEditorTools.GetDataCell<int>(splits,21);
					p.critattack =ExcelEditorTools.GetDataCell<int>(splits,22);
					p.defcrit =ExcelEditorTools.GetDataCell<int>(splits,23);
					p.subdef =ExcelEditorTools.GetDataCell<int>(splits,24);
					p.avoiddamage =ExcelEditorTools.GetDataCell<int>(splits,25);
					p.hitgrow =ExcelEditorTools.GetDataCell<int>(splits,26);
					p.dodgegrow =ExcelEditorTools.GetDataCell<int>(splits,27);
					p.critattackgrow =ExcelEditorTools.GetDataCell<int>(splits,28);
					p.defcritgrow =ExcelEditorTools.GetDataCell<int>(splits,29);
					p.subdefgrow =ExcelEditorTools.GetDataCell<int>(splits,30);
					p.avoiddamagegrow =ExcelEditorTools.GetDataCell<int>(splits,31);
					p.talentskill =ExcelEditorTools.GetDataCell<int>(splits,32);
					p.likedweapon =ExcelEditorTools.GetDataCell<string>(splits,33);
					p.likedep =ExcelEditorTools.GetDataCellStringArray(splits,34);
					p.likedskill =ExcelEditorTools.GetDataCell<string>(splits,35);
					p.likedpartner =ExcelEditorTools.GetDataCellStringArray(splits,36);
					p.fearedhero =ExcelEditorTools.GetDataCellStringArray(splits,37);
					p.des =ExcelEditorTools.GetDataCell<string>(splits,38);
					p.attackdistance =ExcelEditorTools.GetDataCell<int>(splits,39);
					p.cardpicback =ExcelEditorTools.GetDataCell<string>(splits,40);
					p.namepic =ExcelEditorTools.GetDataCell<string>(splits,41);
					p.soulnum =ExcelEditorTools.GetDataCell<int>(splits,42);
					p.skilltype =ExcelEditorTools.GetDataCell<int>(splits,43);

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
