using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class Item
	{
		private readonly JObject mObject;
		public Item(JObject obj)
		{
			mObject = obj;
		}

		public int ID
		{
			get { return (int)mObject["ID"]; }
			set { mObject["ID"] = value; }
		}

		public int Count
		{
			get { return (int)mObject["Count"]; }
			set { mObject["Count"] = value; }
		}

		public bool IsNew
		{
			get { return (bool)mObject["IsNew"]; }
			set { mObject["IsNew"] = value; }
		}
	}
}
