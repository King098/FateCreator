using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FateCreator
{
	public enum ChoiceConditionType
	{
		Less = 1,
		LessEqual = 2,
		Equal = 3,
		GreaterEqual = 4,
		Greater = 5,
		BoolTrue = 6,
		BoolFalse = 7,
	}
	[System.Serializable]
    public class ChoiceCondition
    {
		public string ID;
		public string Parm;
		public ChoiceConditionType Type;
		public int Var;

		public ChoiceCondition(){}

		public ChoiceCondition(string[] contents)
		{
			int offset = 0;
			ID = contents[offset].ToString();offset++;
			Parm = contents[offset].ToString();offset++;
			Type = (ChoiceConditionType)int.Parse(contents[offset]);offset++;
			Var = int.Parse(contents[offset]);offset++;
		}
    }
}