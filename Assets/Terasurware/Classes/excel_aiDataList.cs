
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class excel_aiDataList : ScriptableObject
{	
    public List<excel_ai> dataList = new List<excel_ai>();

    [System.SerializableAttribute]
    public class excel_ai
    {
        
		public int aiid;//AI编号
		public int type;//触发类型
		public int value;//触发条件
		public string note;//对话内容
		public int skillid;//技能id
    }
}
