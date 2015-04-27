using System;
using System.Collections.Generic;



public class ExcelTools
{
    public static bool[] GetDataCellBoolArray(string[] cells, int index, string field = "")
    {
        if (cells.Length <= index)
        {
            return new bool[0];
        }

        string str = cells[index];
        str = str.TrimEnd('|');

        string[] splits = str.Split('|');
        bool[] rtn = new bool[splits.Length];
        for (int i = 0; i < rtn.Length; i++)
        {
            rtn[i] = bool.Parse(splits[i]);
        }
        return rtn;
    }
    public static int[] GetDataCellIntArray(string[] cells, int index, string field = "")
    {
        if (cells.Length <= index)
        {
            return new int[0];
        }

        string str = cells[index];
        str = str.TrimEnd('|');

        string[] splits = str.Split('|');
        int[] rtn = new int[splits.Length];
        for (int i = 0; i < rtn.Length; i++)
        {
            try
            {
                if (!string.IsNullOrEmpty(splits[i]))
                    rtn[i] = int.Parse(splits[i]);

            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError("解析字段 " + field + " 错误");
            }
        }
        return rtn;
    }
    public static float[] GetDataCellFloatArray(string[] cells, int index, string field = "")
    {
        if (cells.Length <= index)
        {
            return new float[0];
        }

        string str = cells[index];
        str = str.TrimEnd('|');

        string[] splits = str.Split('|');
        float[] rtn = new float[splits.Length];
        for (int i = 0; i < rtn.Length; i++)
        {
            try
            {
                rtn[i] = float.Parse(splits[i]);
            }
            catch
            {
                UnityEngine.Debug.LogError("解析字段 " + field + " 错误");
            }
        }
        return rtn;
    }

    public static string[] GetDataCellStringArray(string[] cells, int index, string field = "")
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

    public static T GetDataCell<T>(string[] cells, int index,string field="")
    {
        if (cells.Length <= index)
        {
            return default(T);
        }
		//UnityEngine.Debug.Log("GetDataCell:"+cells[index]);
        T rtn = default(T);
        try
        {
            rtn = (T)Convert.ChangeType(cells[index], typeof(T));
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("解析字段 " + field+ " cells[index]:"+cells[index]+" 错误");
        }

        return rtn;
    }

}
