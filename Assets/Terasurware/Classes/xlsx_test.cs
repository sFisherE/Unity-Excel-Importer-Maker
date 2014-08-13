using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class xlsx_test : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		
		public double ID;
		public string string_data;
		public double int_data;
		public double double_data;
		public bool bool_data;
		public double math_1;
		public string math_2;
		public double[] array;
	}
}