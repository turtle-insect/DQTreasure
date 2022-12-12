using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class Monster
	{
		private readonly JObject mObject;
		public Monster(JObject obj)
		{
			mObject = obj;
		}

		public int ID
		{
			get { return (int)mObject["mKindId"]; }
			set { mObject["mKindId"] = value; }
		}

		public int Level
		{
			get { return (int)mObject["mLevel"]; }
			set { mObject["mLevel"] = value; }
		}

		public int Exp
		{
			get { return (int)mObject["mTotalExp"]; }
			set { mObject["mTotalExp"] = value; }
		}

		public int HP
		{
			get { return (int)mObject["mHp"]; }
			set { mObject["mHp"] = value; }
		}

		public int MP
		{
			get { return (int)mObject["mMp"]; }
			set { mObject["mMp"] = value; }
		}
	}
}
