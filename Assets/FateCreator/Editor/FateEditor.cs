using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace FateCreator
{
    public class FateEditor : EditorWindow
    {
        public int TitleIndex = 0;
        public string[] Titles = { "Events", "Choices", "Conditions" };

        //Events Content
        public int EventIndex = 0;
        public static List<string> EventTitles = new List<string>();
        private Vector2 EventScrollVec;
        private EventInfo EventInfo = new EventInfo();
        //-------------------------

        //Choices Content
        public int ChoiceIndex = 0;
        public static List<string> ChoiceTitles = new List<string>();
		private Vector2 ChoiceScrollVec;
		private ChoiceInfo ChoiceInfo = new ChoiceInfo();
        //-------------------------

        //Conditions Content
        public int ConditionIndex = 0;
        public static List<string> ConditionTitles = new List<string>();
        //-------------------------


        [MenuItem("Window/Fate Editor")]
        static void Init()
        {
            //加载本地数据
            Data.Instance.LoadEventData(Application.streamingAssetsPath + "/Events.txt");
            Data.Instance.LoadChoiceData(Application.streamingAssetsPath + "/Choices.txt");
            Data.Instance.LoadConditionData(Application.streamingAssetsPath + "/Conditions.txt");
            //加载显示列表
            EventTitles.AddRange(Data.Instance.GetEditorEventList());
            ChoiceTitles.AddRange(Data.Instance.GetEditorChoiceList());
            ConditionTitles.AddRange(Data.Instance.GetEditorConditionList());

            FateEditor window = (FateEditor)EditorWindow.GetWindow(typeof(FateEditor));
            window.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            {
                //标题选择
                EditorGUILayout.BeginHorizontal();
                {
                    TitleIndex = GUILayout.SelectionGrid(TitleIndex, Titles, 3);
                }
                EditorGUILayout.EndHorizontal();
                //分割线
                EditorGUILayout.Separator();
                //内容区域
                switch (TitleIndex)
                {
                    case 0:
                        EventGUI();
                        break;
                    case 1:
                        ChoiceGUI();
                        break;
                    case 2:
                        ConditionGUI();
                        break;
                }

            }
            EditorGUILayout.EndVertical();
        }


        void EventGUI()
        {
            EditorGUILayout.BeginHorizontal();
            {
                //左侧列表
                EditorGUILayout.BeginVertical();
                {
                    EventScrollVec = EditorGUILayout.BeginScrollView(EventScrollVec);
                    {
                        EventIndex = GUILayout.SelectionGrid(EventIndex, EventTitles.ToArray(), 1);
                    }
                    EditorGUILayout.EndScrollView();
                    //新增按钮
                    if (GUILayout.Button("新事件"))
                    {
                        EventInfo = new EventInfo();
                        EventInfo.ID = "New ID" + EventTitles.Count;
                        if (Data.Instance.AddEventData(EventInfo))
                        {
                            EventTitles.Add(EventInfo.ID + ":" + EventInfo.Title);
                            EventIndex = EventTitles.Count - 1;
                            EditorGUILayout.EndVertical();
                            return;//重新刷新
                        }
                    }
                }
                EditorGUILayout.EndVertical();
				//间隔
				EditorGUILayout.Separator();
                //右侧显示
                EditorGUILayout.BeginVertical();
                {
                    if (EventTitles.Count > 0)
                    {
                        EventInfo = Data.Instance.GetEventData(EventTitles[EventIndex].Split(':')[0]);
                        if (EventInfo != null)
                        {
                            //ID
                            EventInfo.ID = EditorGUILayout.TextField("ID:", EventInfo.ID);
                            //Title
                            EventInfo.Title = EditorGUILayout.TextField("Title:", EventInfo.Title);
                            //Content
                            EventInfo.Content = EditorGUILayout.TextField("Content:", EventInfo.Content);
                            //ChoiceNum
                            EventInfo.ChoiceNum = EditorGUILayout.IntField("ChoiceNum:", EventInfo.ChoiceNum);


                            EditorGUILayout.BeginHorizontal();
                            {
                                //保存数据
                                if (GUILayout.Button("保存"))
                                {
                                    Data.Instance.ReplaceEventData(EventTitles[EventIndex].Split(':')[0], EventInfo);
                                    EventTitles[EventIndex] = EventInfo.ID + ":" + EventInfo.Title;
                                    //往文件写入

                                    EventIndex = EventTitles.Count - 1;
                                    return;//刷新
                                }
                                //删除数据
                                if (GUILayout.Button("删除"))
                                {
                                    Data.Instance.DeleteEventData(EventTitles[EventIndex].Split(':')[0]);
                                    EventTitles.RemoveAt(EventIndex);
                                    //往文件写入

                                    EventIndex = EventTitles.Count - 1;
                                    return;//刷新
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        void ChoiceGUI()
        {
            EditorGUILayout.BeginHorizontal();
            {
                //左侧列表
                EditorGUILayout.BeginVertical();
                {
                    ChoiceScrollVec = EditorGUILayout.BeginScrollView(ChoiceScrollVec);
                    {
                        ChoiceIndex = GUILayout.SelectionGrid(ChoiceIndex, ChoiceTitles.ToArray(), 1);
                    }
                    EditorGUILayout.EndScrollView();
                    //新增按钮
                    if (GUILayout.Button("新选项"))
                    {
                        ChoiceInfo = new ChoiceInfo();
                        ChoiceInfo.ID = "New ID" + ChoiceTitles.Count;
                        if (Data.Instance.AddChoiceData(ChoiceInfo))
                        {
                            ChoiceTitles.Add(ChoiceInfo.ID + ":" + ChoiceInfo.Content);
                            ChoiceIndex = ChoiceTitles.Count - 1;
                            EditorGUILayout.EndVertical();
                            return;//重新刷新
                        }
                    }
                }
                EditorGUILayout.EndVertical();
				//间隔
				EditorGUILayout.Separator();
				//右侧显示
				EditorGUILayout.BeginVertical();
                {
                    if (ChoiceTitles.Count > 0)
                    {
                        ChoiceInfo = Data.Instance.GetChoiceData(ChoiceTitles[ChoiceIndex].Split(':')[0]);
                        if (ChoiceInfo != null)
                        {
                            //ID
                            ChoiceInfo.ID = EditorGUILayout.TextField("ID:", ChoiceInfo.ID);
                            //Content
                            ChoiceInfo.Content = EditorGUILayout.TextField("Content:", ChoiceInfo.Content);


                            EditorGUILayout.BeginHorizontal();
                            {
                                //保存数据
                                if (GUILayout.Button("保存"))
                                {
                                    Data.Instance.ReplaceChoiceData(ChoiceTitles[ChoiceIndex].Split(':')[0], ChoiceInfo);
                                    ChoiceTitles[EventIndex] = ChoiceInfo.ID + ":" + ChoiceInfo.Content;
                                    //往文件写入

                                    ChoiceIndex = ChoiceTitles.Count - 1;
                                    return;//刷新
                                }
                                //删除数据
                                if (GUILayout.Button("删除"))
                                {
                                    Data.Instance.DeleteChoiceData(ChoiceTitles[ChoiceIndex].Split(':')[0]);
                                    ChoiceTitles.RemoveAt(ChoiceIndex);
                                    //往文件写入

                                    ChoiceIndex = ChoiceTitles.Count - 1;
                                    return;//刷新
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();
        }

        void ConditionGUI()
        {

        }
    }
}