using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class Item : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private readonly JObject mObject;
		public Item(JObject obj)
		{
			mObject = obj;
		}

		public uint ID
		{
			get { return (uint)mObject["ID"]; }
			set
			{
				mObject["ID"] = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ID)));
			}
		}

		public uint Count
		{
			get { return (uint)mObject["Count"]; }
			set
			{
				mObject["Count"] = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
			}
		}

		public bool IsNew
		{
			get { return (bool)mObject["IsNew"]; }
			set { mObject["IsNew"] = value; }
		}
	}
}
