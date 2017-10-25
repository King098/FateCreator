using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FateCreator
{
    public class CircleInfo
    {
        public EventInfo Info;
        public int ChoiceIndex;
        public int ToIDsIndex;

        public CircleInfo(EventInfo info)
        {
            Info = info;
            ChoiceIndex = 0;
            ToIDsIndex = 0;
        }

        //获取下一个需要检测的事件对象(外层只需要设置ToIDsIndex增长就可以了)
        public EventInfo GetNextEvent()
        {
            if (ChoiceIndex < Info.Choice.Count)
            {
                ChoiceInfo choice = Data.Instance.GetChoiceData(Info.Choice[ChoiceIndex]);
                if (ToIDsIndex < choice.ToIDs.Count)
                {
                    return Data.Instance.GetEventData(choice.ToIDs[ToIDsIndex]);
                }
                else
                {
                    ChoiceIndex++;
                    ToIDsIndex = 0;
                    return GetNextEvent();
                }
            }
            else
            {
                return null;
            }
        }
    }
    public class FateCircle
    {
        private static FateCircle m_self = null;
        public static FateCircle Instance
        {
            get
            {
                if (m_self == null)
                {
                    m_self = new FateCircle();
                }
                return m_self;
            }
        }

        public Stack<CircleInfo> infos = new Stack<CircleInfo>();

        public bool CheckHasEvent(EventInfo info)
        {
            foreach (CircleInfo i in infos)
            {
                if (i.Info.ID == info.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public void LogErrorInfos()
        {
            foreach (CircleInfo i in infos)
            {
                Debug.LogError("输出栈数据:" + i.Info.ID);
            }
        }

        public bool Check(EventInfo start)
        {
            CircleInfo info = new CircleInfo(start);
            infos.Push(info);
            EventInfo next = info.GetNextEvent();
            while (next != null)
            {
                if (CheckHasEvent(next))
                {
                    Debug.LogError("失败处:" + next.ID);
                    LogErrorInfos();
                    return false;
                }
                info = new CircleInfo(next);
                infos.Push(info);
                info.ToIDsIndex++;
                next = info.GetNextEvent();
                while (next == null)
                {
                    if (infos.Count > 0)
                    {
                        info = infos.Pop();
                        info.ToIDsIndex++;
                        next = info.GetNextEvent();
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }
    }
}