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
		public Info Info { get; private set; } = Info.Instance();

		public CommandAction FileSaveCommand { get; private set; }
		public CommandAction FileSaveAsCommand { get; private set; }
		public CommandAction ImportCommand { get; private set; }
		public CommandAction ExportCommand { get; private set; }

		public CommandAction ChoiceItemCommand { get; private set; }
		public CommandAction ChoiceMonsterCommand { get; private set; }
		public CommandAction ChoiceTreasureCommand { get; private set; }

		public General General { get;private set; }
		public Player Player { get; private set; }
		public ObservableCollection<Item> Items { get; private set; } = new ObservableCollection<Item>();
		public ObservableCollection<Treasure> Treasures { get; private set; } = new ObservableCollection<Treasure>();
		public ObservableCollection<Monster> Monsters { get; private set; } = new ObservableCollection<Monster>();
		public ObservableCollection<Coin> Coins { get; private set; } = new ObservableCollection<Coin>();

		public ViewModel()
		{
			FileSaveCommand = new CommandAction(FileSave);
			FileSaveAsCommand = new CommandAction(FileSaveAs);
			ImportCommand = new CommandAction(Import);
			ExportCommand = new CommandAction(Export);

			ChoiceItemCommand = new CommandAction(ChoiceItem);
			ChoiceMonsterCommand = new CommandAction(ChoiceMonster);
			ChoiceTreasureCommand = new CommandAction(ChoiceTreasure);

			var json = SaveData.Instance().Json;
			if (json == null) return;

			General = new General();
			Player = new Player();
			foreach (var obj in json["SaveData"]["BelongingsItem"]["ItemList"])
			{
				Items.Add(new Item((JObject)obj));
			}

			foreach (var obj in json["SaveData"]["History"]["TreasureQuality"])
			{
				Treasures.Add(new Treasure((JObject)obj));
			}

			foreach (var obj in json["SaveData"]["StockMonster"]["StockMonsters"])
			{
				Monsters.Add(new Monster((JObject)obj));
			}

			foreach (var obj in json["SaveData"]["BelongingsCoin"]["Coins"])
			{
				var param = obj["Param"];
				Coins.Add(new Coin((JObject)param));
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

		private void ChoiceItem(object? parameter)
		{
			var item = parameter as Item;
			if (item == null) return;

			item.ID = CreateChoiceWindow(item.ID, ChoiceWindow.eType.eItem);
		}

		private void ChoiceMonster(object? parameter)
		{
			var item = parameter as Monster;
			if (item == null) return;

			item.ID = CreateChoiceWindow(item.ID, ChoiceWindow.eType.eMonster);
		}

		private void ChoiceTreasure(object? parameter)
		{
			var item = parameter as Treasure;
			if (item == null) return;

			item.ID = CreateChoiceWindow(item.ID, ChoiceWindow.eType.eTreasure);
		}

		private uint CreateChoiceWindow(uint id, ChoiceWindow.eType type)
		{
			var window = new ChoiceWindow();
			window.ID = id;
			window.Type= type;
			window.ShowDialog();
			return window.ID;
		}
	}
}
