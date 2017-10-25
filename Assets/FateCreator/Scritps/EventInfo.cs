using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FateCreator
{
	[System.Serializable]
    public class EventInfo
    {
		public string ID;
		public string Title;
		public string Content;
		public int ChoiceNum;
		public List<string> Choice;//对应ChoiceInfo的ID列表

		public EventInfo(){}

		public EventInfo(string[] contents)
		{
			int offset = 0;
			ID = contents[offset].ToString();offset++;
			Title = contents[offset].ToString();offset++;
			Content = contents[offset].ToString();offset++;
			ChoiceNum = int.Parse(contents[offset].ToString());offset++;
			Choice = new List<string>();
			string[] c = contents[offset].ToString().Split('|');offset++;
			for(int i = 0;i<c.Length;i++)
			{
				if(!Choice.Contains(c[i].Trim()) && c[i].Trim() != "")
				{
					Choice.Add(c[i].Trim());
				}
			}
		}
    }
}