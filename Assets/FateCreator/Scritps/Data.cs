using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FateCreator
{
    public class Data
    {
        private static Data m_self = null;
        public static Data Instance
        {
            get
            {
                if (m_self == null)
                {
                    m_self = new Data();
                }
                return m_self;
            }
        }

        private Dictionary<string, EventInfo> EventDatas = new Dictionary<string, EventInfo>();
        private Dictionary<string, ChoiceInfo> ChoiceDatas = new Dictionary<string, ChoiceInfo>();
        private Dictionary<string, ChoiceCondition> ConditionDatas = new Dictionary<string, ChoiceCondition>();

        private List<string> Chars = new List<string>()
        {"0","1","2","3","4","5","6","7","8","9",
        "a","b","c","d","e","f","g","h","i","j","k",
        "l","m","n","o","p","q","r","s","t","u","v",
        "w","x","y","z","A","B","C","D","E","F","G",
        "H","I","J","K","L","M","N","O","P","Q","R",
        "S","T","U","V","W","X","Y","Z"
        };

        public string[] LoadDataFromUrl(string url)
        {
            string content = System.IO.File.ReadAllText(url);
            return content.Trim().Replace("\r", "").Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        }

        public void LoadEventData(string url)
        {
            string[] contents = LoadDataFromUrl(url);
            for (int i = 0; i < contents.Length; i++)
            {
                AddEventData(new EventInfo(contents[i].Split('\t')));
            }
        }

        public void LoadChoiceData(string url)
        {
            string[] contents = LoadDataFromUrl(url);
            for (int i = 0; i < contents.Length; i++)
            {
                AddChoiceData(new ChoiceInfo(contents[i].Split('\t')));
            }
        }

        public void LoadConditionData(string url)
        {
            string[] contents = LoadDataFromUrl(url);
            for (int i = 0; i < contents.Length; i++)
            {
                AddConditionData(new ChoiceCondition(contents[i].Split('\t')));
            }
        }

        public List<string> GetEditorEventList()
        {
            List<string> result = new List<string>();
            foreach (EventInfo info in EventDatas.Values)
            {
                result.Add(info.ID + ":" + info.Title);
            }
            return result;
        }

        public List<string> GetEditorChoiceList()
        {
            List<string> result = new List<string>();
            foreach (ChoiceInfo info in ChoiceDatas.Values)
            {
                result.Add(info.ID + ":" + info.Content);
            }
            return result;
        }

        public List<string> GetEditorConditionList()
        {
            List<string> result = new List<string>();
            foreach (ChoiceCondition info in ConditionDatas.Values)
            {
                result.Add(info.ID + ":" + info.Parm + " " + info.Type.ToString());
            }
            return result;
        }

        public bool AddEventData(EventInfo info)
        {
            if (!EventDatas.ContainsKey(info.ID))
            {
                EventDatas.Add(info.ID, info);
                return true;
            }
            return false;
        }

        public void DeleteEventData(string ID)
        {
            if (EventDatas.ContainsKey(ID))
            {
                EventDatas.Remove(ID);
            }
        }

        public void ReplaceEventData(string ID, EventInfo info)
        {
            if (EventDatas.ContainsKey(ID))
            {
                EventDatas.Remove(ID);
				EventDatas.Add(info.ID, info);
            }
        }

        public EventInfo GetEventData(string ID)
        {
            if (EventDatas.ContainsKey(ID))
            {
                return EventDatas[ID];
            }
            return null;
        }

        public bool AddChoiceData(ChoiceInfo info)
        {
            if (!ChoiceDatas.ContainsKey(info.ID))
            {
                ChoiceDatas.Add(info.ID, info);
                return true;
            }
            return false;
        }

        public void DeleteChoiceData(string ID)
        {
            if (ChoiceDatas.ContainsKey(ID))
            {
                ChoiceDatas.Remove(ID);
            }
        }

        public void ReplaceChoiceData(string ID, ChoiceInfo info)
        {
            if (ChoiceDatas.ContainsKey(ID))
            {
                ChoiceDatas.Remove(ID);
                ChoiceDatas.Add(info.ID, info);
            }
        }

        public ChoiceInfo GetChoiceData(string ID)
        {
            if (ChoiceDatas.ContainsKey(ID))
            {
                return ChoiceDatas[ID];
            }
            return null;
        }

        public bool AddConditionData(ChoiceCondition info)
        {
            if (!ConditionDatas.ContainsKey(info.ID))
            {
                ConditionDatas.Add(info.ID, info);
                return true;
            }
            return false;
        }

        public void DeleteConditionData(string ID)
        {
            if (ConditionDatas.ContainsKey(ID))
            {
                ConditionDatas.Remove(ID);
            }
        }

        public void ReplaceConditionData(string ID, ChoiceCondition info)
        {
            if (ConditionDatas.ContainsKey(ID))
            {
                ConditionDatas.Remove(ID);
                ConditionDatas.Add(info.ID, info);
            }
        }

        public ChoiceCondition GetConditionData(string ID)
        {
            if (!ConditionDatas.ContainsKey(ID))
            {
                return ConditionDatas[ID];
            }
            return null;
        }
    }
}