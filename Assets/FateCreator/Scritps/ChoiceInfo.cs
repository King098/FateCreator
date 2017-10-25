using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FateCreator
{
	[System.Serializable]
    public class ChoiceInfo
    {
        public string ID;
        public string Content;
        public List<string> ToIDs;
        public Dictionary<int, List<string>> Conditions;//ChoiceCondition的ID的列表

		public ChoiceInfo(){}

        public ChoiceInfo(string[] contents)
        {
            int offset = 0;
            ID = contents[offset].ToString(); offset++;
            Content = contents[offset].ToString(); offset++;
            ToIDs = new List<string>();
            string[] ids = contents[offset].ToString().Split('|'); offset++;
            for (int i = 0; i < ids.Length; i++)
            {
                if (!ToIDs.Contains(ids[i].Trim()) && ids[i].Trim() != "")
                {
                    ToIDs.Add(ids[i].Trim());
                }
            }
            Conditions = new Dictionary<int, List<string>>();
            string[] conditions = contents[offset].ToString().Split(';'); offset++;
            for (int i = 0; i < conditions.Length; i++)
            {
                string[] condition = conditions[i].ToString().Split('|');;
                for (int j = 0; j < condition.Length; j++)
                {
					if(Conditions.ContainsKey(i))
					{
						if (!Conditions[i].Contains(condition[j].Trim()) && condition[j].Trim() != "")
						{
							Conditions[i].Add(condition[j].Trim());
						}
					}
                    else
					{
						Conditions.Add(i,new List<string>(){condition[j].Trim()});
					}
                }
            }
        }
    }
}