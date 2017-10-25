using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FateCreator
{
	public enum PlayerSex
	{
		Male = 1,
		FeMale = 2,
		Unknown = 3,
	}
	[System.Serializable]
    public class PlayerInfo
    {
        public Dictionary<string,object> Player = new Dictionary<string, object>();

		public PlayerInfo(string name)
		{
			Player.Add("ID",System.DateTime.Now.ToString());
			Player.Add("Name",name);
			Player.Add("Sex",(PlayerSex)Random.Range(1,4));
			Player.Add("Age",1);
			Player.Add("HP",Random.Range(5,100));
			Player.Add("MP",Random.Range(5,100));
			Player.Add("Speed",Random.Range(5,100));
			Player.Add("Lucky",Random.Range(5,100));
		}		
    }
}