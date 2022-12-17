using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class General
	{
		public uint Gold
		{
			get { return (uint)SaveData.Instance().Json["SaveData"]["Player"]["Gold"]; }
			set { SaveData.Instance().Json["SaveData"]["Player"]["Gold"] = value; }
		}
	}
}
