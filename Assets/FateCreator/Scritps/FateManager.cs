using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FateCreator
{
    public class FateManager
    {
        private static FateManager m_self = null;
        public static FateManager Instance
        {
            get
            {
                if (m_self == null)
                {
                    m_self = new FateManager();
                }
                return m_self;
            }
        }
        public PlayerInfo Player;
        public string[] startIDs = new string[] { "1", "2", "3" };
        ///获取起始事件
        public EventInfo GetStartEvent()
        {
            return Data.Instance.GetEventData(startIDs[Random.Range(0, startIDs.Length)]);
        }

        ///检测条件是否达成
        public bool CheckCondition(ChoiceCondition condition)
        {
            bool result = false;
            switch (condition.Type)
            {
                case ChoiceConditionType.Less:
                    if ((int)Player.Player[condition.Parm] < condition.Var)
                        result = true;
                    break;
                case ChoiceConditionType.LessEqual:
                    if ((int)Player.Player[condition.Parm] <= condition.Var)
                        result = true;
                    break;
                case ChoiceConditionType.Equal:
                    if ((int)Player.Player[condition.Parm] == condition.Var)
                        result = true;
                    break;
                case ChoiceConditionType.GreaterEqual:
                    if ((int)Player.Player[condition.Parm] >= condition.Var)
                        result = true;
                    break;
                case ChoiceConditionType.Greater:
                    if ((int)Player.Player[condition.Parm] > condition.Var)
                        result = true;
                    break;
                case ChoiceConditionType.BoolTrue:
                    if ((bool)Player.Player[condition.Parm] == true)
                        result = true;
                    break;
                case ChoiceConditionType.BoolFalse:
                    if ((bool)Player.Player[condition.Parm] == false)
                        result = true;
                    break;
            }
            return result;
        }

        ///检测选项是否可用
        public bool CheckChoiceInfo(ChoiceInfo info)
        {
            if (info.Conditions == null || info.Conditions.Count == 0)
                return true;
            bool result = false;
            foreach (List<string> conditions in info.Conditions.Values)
            {
                if (!result)
                {
                    result = true;
                    for (int i = 0; i < conditions.Count; i++)
                    {
                        ChoiceCondition con = Data.Instance.GetConditionData(conditions[i]);
                        if (con != null && !CheckCondition(con))
                        {
                            result = false;
                            break;
                        }
                    }
                    if (result)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        ///获取一个事件的固定数量的选项
        public List<ChoiceInfo> GetEventChoice(EventInfo info)
        {
            List<ChoiceInfo> result = new List<ChoiceInfo>();
            for (int i = 0; i < info.Choice.Count; i++)
            {
                ChoiceInfo choice = Data.Instance.GetChoiceData(info.Choice[i]);
                if (choice != null)
                {
                    Debug.Log("1选项" + choice.ID);
                    if (CheckChoiceInfo(choice))
                    {
                        Debug.Log("2选项" + choice.ID);
                        result.Add(choice);
                    }
                }
            }
            if (result.Count > info.ChoiceNum)
            {
                int count = result.Count - info.ChoiceNum;
                for (int i = 0; i < count; i++)
                {
                    result.RemoveAt(Random.Range(0, result.Count));
                }
            }
            return result;
        }
    }
}