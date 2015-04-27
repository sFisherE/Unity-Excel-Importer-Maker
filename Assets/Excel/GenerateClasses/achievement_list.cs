
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
namespace ConfigData
{
    [System.SerializableAttribute]
    public class achievement
    {
		public int id;//成就编号
		public string icon;//成就ICON
		public int lv;//等级需求
		public string taskname;//成就名称
		public string describe;//成就描述
		public int type;//任务类型
		public string condition;//完成条件
		public int gold;//金币
		public int silver;//银币
		public int vigour;//活力
		public int Stamina;//体力
		public int exp;//经验
		public string rewards;//掉落
		public int chainid;//任务链 id
		public int chainseq;//任务链顺序

    }

    public class achievement_list : ScriptableObject
    {	
        public List<achievement> dataList = new List<achievement>();
        public static List<achievement> Read(string filePath)
        {
            List<achievement> rnt = new List<achievement>();
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(stream);
            sr.ReadLine();//注释
            sr.ReadLine();//类型
            sr.ReadLine();//名称
            string lineData = sr.ReadLine();
            while (lineData != null)
            {
                var p = new achievement();
                string[] splits = lineData.Split('\t');
    
            p.id =ExcelTools.GetDataCell<int>(splits,0);
            p.icon =ExcelTools.GetDataCell<string>(splits,1);
            p.lv =ExcelTools.GetDataCell<int>(splits,2);
            p.taskname =ExcelTools.GetDataCell<string>(splits,3);
            p.describe =ExcelTools.GetDataCell<string>(splits,4);
            p.type =ExcelTools.GetDataCell<int>(splits,5);
            p.condition =ExcelTools.GetDataCell<string>(splits,6);
            p.gold =ExcelTools.GetDataCell<int>(splits,7);
            p.silver =ExcelTools.GetDataCell<int>(splits,8);
            p.vigour =ExcelTools.GetDataCell<int>(splits,9);
            p.Stamina =ExcelTools.GetDataCell<int>(splits,10);
            p.exp =ExcelTools.GetDataCell<int>(splits,11);
            p.rewards =ExcelTools.GetDataCell<string>(splits,12);
            p.chainid =ExcelTools.GetDataCell<int>(splits,13);
            p.chainseq =ExcelTools.GetDataCell<int>(splits,14);

                rnt.Add(p);
                lineData = sr.ReadLine();
            }
            return rnt;
        }
    }
}