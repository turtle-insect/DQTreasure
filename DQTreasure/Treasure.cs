using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class Treasure : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

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

		public uint ID
		{
			get { return (uint)mObject["TreasureID"]; }
			set
			{
				mObject["TreasureID"] = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
			}
		}
	}
}
