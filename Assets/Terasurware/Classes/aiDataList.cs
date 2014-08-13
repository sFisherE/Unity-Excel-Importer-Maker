
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class aiDataList : ScriptableObject
{	
    public List<ai> dataList = new List<ai>();

    [System.SerializableAttribute]
    public class ai
    {
        
		public int aiid;//AI编号
		public int type;//触发类型
		public int value;//触发条件
		public string note;//对话内容
		public int skillid;//技能id
    }
}
