
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class achievement_list : ScriptableObject
{	
    public List<achievement> dataList = new List<achievement>();

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
		public string[] rewards;//掉落
		public int chainid;//任务链 id
		public int chainseq;//任务链顺序
    }
}
