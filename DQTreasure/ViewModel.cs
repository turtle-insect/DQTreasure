using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DQTreasure
{
	internal class ViewModel
	{
		public CommandAction FileSaveCommand { get; private set; }
		public CommandAction FileSaveAsCommand { get; private set; }
		public CommandAction ImportCommand { get; private set; }
		public CommandAction ExportCommand { get; private set; }

		public Player Player { get; private set; }
		public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Treasure> Treasures { get; private set; } = new ObservableCollection<Treasure>();

		public ViewModel()
		{
			FileSaveCommand = new CommandAction(FileSave);
			FileSaveAsCommand = new CommandAction(FileSaveAs);
			ImportCommand = new CommandAction(Import);
			ExportCommand = new CommandAction(Export);

			var json = SaveData.Instance().Json;
			if (json == null) return;

			Player = new Player();
			foreach (var obj in json["SaveData"]["BelongingsItem"]["ItemList"])
			{
				Items.Add(new Item((JObject)obj));
			}

			foreach (var obj in json["SaveData"]["History"]["TreasureQuality"])
			{
				Treasures.Add(new Treasure((JObject)obj));
			}
		}

		private void FileSave(object? parameter)
		{
			SaveData.Instance().Save();
		}

		private void FileSaveAs(object? parameter)
		{
			var dlg = new Microsoft.Win32.SaveFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().SaveAs(dlg.FileName);
		}

		private void Import(object? parameter)
		{
			var dlg = new Microsoft.Win32.OpenFileDialog();
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Import(dlg.FileName);
		}

		private void Export(object? parameter)
		{
			var dlg = new Microsoft.Win32.SaveFileDialog();
			dlg.DefaultExt= "json";
			dlg.Filter= "json|*json";
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Export(dlg.FileName);
		}
	}
}
