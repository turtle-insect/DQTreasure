using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class Treasure
	{
		private readonly JObject mObject;
		public ObservableCollection<TreasureQuality> Qualities { get; private set; } = new ObservableCollection<TreasureQuality>();
		public Treasure(JObject obj)
		{
			mObject = obj;

			foreach (var quality in obj["QualityCount"])
			{
				Qualities.Add(new TreasureQuality((JObject)quality));
			}
		}

		public int ID
		{
			get { return (int)mObject["TreasureID"]; }
			set { mObject["TreasureID"] = value; }
		}
	}
}
