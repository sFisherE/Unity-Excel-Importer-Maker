
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class heros_list : ScriptableObject
{	
    public List<heros> dataList = new List<heros>();

    [System.SerializableAttribute]
    public class heros
    {
        
		public int id;//英雄编号
		public string hname;//英雄名称
		public string icon;//英雄ICON
		public string cardpic;//英雄卡牌图片
		public string modelprefab;//英雄模型预制
		public int countlevel;//稀有度
		public int htype;//英雄类型
		public int tavern;//酒馆
		public int camp;//阵营
		public int hp;//生命
		public int mp;//魔法
		public int attack;//攻击
		public int defence;//护甲
		public int speed;//速度
		public float hpgrow;//生命成长
		public float mpgrow;//魔法成长
		public float attackgrow;//攻击成长
		public float defencegrow;//护甲成长
		public float speedgrow;//速度成长
		public float skillmodi;//技能概率修正
		public int hit;//命中
		public int dodge;//闪避
		public int critattack;//暴击
		public int defcrit;//韧性
		public int subdef;//穿透
		public int avoiddamage;//免伤
		public int hitgrow;//命中成长
		public int dodgegrow;//闪避成长
		public int critattackgrow;//暴击成长
		public int defcritgrow;//韧性成长
		public int subdefgrow;//穿透成长
		public int avoiddamagegrow;//免伤成长
		public int talentskill;//天赋技能
		public string likedweapon;//最喜爱的武器
		public string[] likedep;//最喜爱的装备
		public string likedskill;//最喜爱的技能
		public string[] likedpartner;//最喜爱的队友
		public string[] fearedhero;//受克制英雄
		public string des;//描述140字以内
		public int attackdistance;//近战远程
		public string cardpicback;//英雄卡牌图片背景
		public string namepic;//名字图片
		public int soulnum;//重复抽取时获取英魂数量
		public int skilltype;//大招类型
    }
}
