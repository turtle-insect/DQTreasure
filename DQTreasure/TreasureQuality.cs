using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class TreasureQuality
	{
		private readonly JObject mObject;
		public TreasureQuality(JObject obj)
		{
			mObject = obj;
		}

		public int ID
		{
			get { return (int)mObject["QualityID"]; }
			set { mObject["QualityID"] = value; }
		}

		public int Num
		{
			get { return (int)mObject["QualityNum"]; }
			set { mObject["QualityNum"] = value; }
		}
	}
}
