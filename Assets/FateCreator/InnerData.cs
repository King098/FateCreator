using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FateCreator;

public class InnerData : MonoBehaviour 
{
	public List<EventInfo> EventInfos;
	public List<ChoiceInfo> ChoiceInfos;
	public List<ChoiceCondition> ChoiceConditions;
	public PlayerInfo Player;

	void Awake()
	{
		//先生成Player信息
		Player = new PlayerInfo("Jack");
		FateManager.Instance.Player = Player;
		//存储信息
		for(int i=0;i<EventInfos.Count;i++)
		{
			Data.Instance.AddEventData(EventInfos[i]);
		}
		for(int i =0;i<ChoiceInfos.Count;i++)
		{
			Data.Instance.AddChoiceData(ChoiceInfos[i]);
		}
		for(int i = 0;i<ChoiceConditions.Count;i++)
		{
			Data.Instance.AddConditionData(ChoiceConditions[i]);
		}
	}
	void Start()
	{
		//随机第一个事件
		EventInfo start = FateManager.Instance.GetStartEvent();
		Debug.Log(start);
		List<ChoiceInfo> choices = FateManager.Instance.GetEventChoice(start);
		Debug.Log("title:" + start.Title);
		Debug.Log("content:" + start.Content);
		for(int i = 0;i<choices.Count;i++)
		{
			Debug.Log(i+":" +choices[i].Content + "  To:" + choices[i].ToIDs[Random.Range(0,choices[i].ToIDs.Count)]);
		}
		Debug.Log("Result:" + FateCircle.Instance.Check(start));
	}
}
