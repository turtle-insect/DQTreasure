using System;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQTreasure
{
	internal class ViewModel
	{
		public CommandAction FileSaveCommand { get; private set; }
		public CommandAction FileSaveAsCommand { get; private set; }
		public CommandAction ImportCommand { get; private set; }
		public CommandAction ExportCommand { get; private set; }

		public Player Player { get; private set; }

		public ViewModel()
		{
			FileSaveCommand = new CommandAction(FileSave);
			FileSaveAsCommand = new CommandAction(FileSaveAs);
			ImportCommand = new CommandAction(Import);
			ExportCommand = new CommandAction(Export);

			if (SaveData.Instance().Json == null) return;

			Player = new Player();
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
			if (dlg.ShowDialog() == false) return;

			SaveData.Instance().Export(dlg.FileName);
		}
	}
}
