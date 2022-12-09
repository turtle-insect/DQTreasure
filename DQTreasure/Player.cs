using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class Player : INotifyCollectionChanged
	{
		public event NotifyCollectionChangedEventHandler? CollectionChanged;

		public uint Lv
		{
			get { return (uint)SaveData.Instance().Json["SaveData"]["Player"]["Level"]; }
			set { SaveData.Instance().Json["SaveData"]["Player"]["Level"] = value; }
		}

		public uint Exp
		{
			get { return (uint)SaveData.Instance().Json["SaveData"]["Player"]["TotalExp"]; }
			set { SaveData.Instance().Json["SaveData"]["Player"]["TotalExp"] = value; }
		}

		public uint HP
		{
			get { return (uint)SaveData.Instance().Json["SaveData"]["Player"]["HP"]; }
			set { SaveData.Instance().Json["SaveData"]["Player"]["HP"] = value; }
		}

		public uint MP
		{
			get { return (uint)SaveData.Instance().Json["SaveData"]["Player"]["MP"]; }
			set { SaveData.Instance().Json["SaveData"]["Player"]["MP"] = value; }
		}

		public uint BP
		{
			get { return (uint)SaveData.Instance().Json["SaveData"]["Player"]["BP"]; }
			set { SaveData.Instance().Json["SaveData"]["Player"]["BP"] = value; }
		}
	}
}
