using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class SaveData
	{
		private static SaveData mThis = null;
		private String mFileName = null;
		private Byte[] mHeader = null;
		private Byte[] mFooter = null;
		public Newtonsoft.Json.Linq.JObject Json { get; private set; } = null;
		public uint Adventure { private get; set; } = 0;
		


		private SaveData()
		{ }

		public static SaveData Instance()
		{
			if (mThis == null) mThis = new SaveData();
			return mThis;
		}

		public bool Open(String filename)
		{
			if (System.IO.File.Exists(filename) == false) return false;

			Byte[] buffer = System.IO.File.ReadAllBytes(filename);
			Byte[] decomp = new Byte[0];
			for (int index = 0; index < buffer.Length;)
			{
				int size = BitConverter.ToInt32(buffer, index + 0x10);
				Byte[] comp = new Byte[size];
				Array.Copy(buffer, index + 0x30, comp, 0, comp.Length);
				try
				{
					Byte[] tmp = Ionic.Zlib.ZlibStream.UncompressBuffer(comp);
					Array.Resize(ref decomp, decomp.Length + tmp.Length);
					Array.Copy(tmp, 0, decomp, decomp.Length - tmp.Length, tmp.Length);
					if (BitConverter.ToInt32(buffer, index + 0x18) != 0x20000) break;
					index += size + 0x30;
				}
				catch
				{
					return false;
				}
			}

			// UTF-16のJsonで構成されている
			// {"SaveData"ではじまり
			// }}}で閉まる
			String header = "{\"SaveData\"";
			int headerIndex = 0;
			for (int index = 0; index < decomp.Length; index++)
			{
				if (header[0] == decomp[index])
				{
					bool isFind = true;
					for (int i = 0; i < header.Length; i++)
					{
						if (header[i] != decomp[index + i * 2])
						{
							isFind = false;
							break;
						}
					}

					if (isFind)
					{
						headerIndex = index;
						mHeader = new Byte[index];
						Array.Copy(decomp, 0, mHeader, 0, mHeader.Length);
						break;
					}
				}
			}

			if (mHeader == null)
			{
				return false;
			}

			String footer = "}}}";
			int footerIndex = 0;
			for (int index = decomp.Length - 1; index >= 0; index--)
			{
				if (footer[0] == decomp[index])
				{
					bool isFind = true;
					for (int i = 0; i < footer.Length; i++)
					{
						if (footer[i] != decomp[index + i * 2])
						{
							isFind = false;
							break;
						}
					}

					if (isFind)
					{
						footerIndex = index + footer.Length * 2;
						mFooter = new Byte[decomp.Length - footerIndex];
						Array.Copy(decomp, footerIndex, mFooter, 0, mFooter.Length);
						break;
					}
				}
			}

			if (mFooter == null)
			{
				return false;
			}

			Byte[] body = new Byte[footerIndex - headerIndex];
			Array.Copy(decomp, headerIndex, body, 0, body.Length);

			String text = System.Text.Encoding.Unicode.GetString(body);
			Json = Newtonsoft.Json.Linq.JObject.Parse(text);

			mFileName = filename;
			Backup();
			return true;
		}

		public bool Save()
		{
			if (mFileName == null || Json == null) return false;

			String text = Json.ToString(Newtonsoft.Json.Formatting.None);
			Byte[] body = System.Text.Encoding.Unicode.GetBytes(text);
			Byte[] buffer = new Byte[mHeader.Length + body.Length + mFooter.Length];
			Array.Copy(mHeader, buffer, mHeader.Length);
			Array.Copy(body, 0, buffer, mHeader.Length, body.Length);
			Array.Copy(mFooter, 0, buffer, mHeader.Length + body.Length, mFooter.Length);
			Array.Copy(BitConverter.GetBytes(buffer.Length - 4), 0, buffer, 0, 4);
			Array.Copy(BitConverter.GetBytes(body.Length + 6), 0, buffer, mHeader.Length - 13, 4);
			Array.Copy(BitConverter.GetBytes(UInt32.MaxValue - (UInt32)body.Length / 2), 0, buffer, mHeader.Length - 4, 4);

			Byte[] output = new Byte[0];
			for (int index = 0; index < buffer.Length; index += 0x20000)
			{
				int size = 0x20000;
				if (index + size > buffer.Length) size = buffer.Length - index;

				Byte[] decomp = new Byte[size];
				Array.Copy(buffer, index, decomp, 0, decomp.Length);
				Byte[] tmp = Ionic.Zlib.ZlibStream.CompressBuffer(decomp);
				int length = output.Length;
				Array.Resize(ref output, length + tmp.Length + 0x30);
				Array.Copy(BitConverter.GetBytes(0x9E2A83C1), 0, output, length, 4);
				Array.Copy(BitConverter.GetBytes(0x20000), 0, output, length + 8, 4);
				Array.Copy(BitConverter.GetBytes(tmp.Length), 0, output, length + 0x10, 4);
				Array.Copy(BitConverter.GetBytes(size), 0, output, length + 0x18, 4);
				Array.Copy(BitConverter.GetBytes(tmp.Length), 0, output, length + 0x20, 4);
				Array.Copy(BitConverter.GetBytes(size), 0, output, length + 0x28, 4);
				Array.Copy(tmp, 0, output, length + 0x30, tmp.Length);
			}


			System.IO.File.WriteAllBytes(mFileName, output);
			return true;
		}

		public bool SaveAs(String filename)
		{
			if (mFileName == null || Json == null) return false;
			mFileName = filename;
			return Save();
		}

		public void Import(String filename)
		{
			if (mFileName == null) return;

			String text = System.IO.File.ReadAllText(filename);
			Json = Newtonsoft.Json.Linq.JObject.Parse(text);
		}

		public void Export(String filename)
		{
			System.IO.File.WriteAllText(filename, Json.ToString());
		}

		private void Backup()
		{
			DateTime now = DateTime.Now;
			String path = System.IO.Directory.GetCurrentDirectory();
			path = System.IO.Path.Combine(path, "backup");
			if (!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}
			path = System.IO.Path.Combine(path, $"{now:yyyy-MM-dd HH-mm-ss} {System.IO.Path.GetFileName(mFileName)}");
			System.IO.File.Copy(mFileName, path, true);
		}
	}
}
