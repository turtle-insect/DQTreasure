using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class Coin : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private readonly JObject mObject;
		public Coin(JObject obj)
		{
			mObject = obj;
		}

		public uint ID
		{
			get { return (uint)mObject["MonsterKindId"]; }
			set
			{
				mObject["MonsterKindId"] = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
			}
		}
	}
}
