
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace compiLiasse_Desktop
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		internal static List<FilePdf> GetFilesFromJson(string pFileName)
		{
			string listFilesJson;
			try
			{
				listFilesJson = File.ReadAllText(pFileName);
			}
			catch (Exception)
			{
				Console.WriteLine($"Erreur de lecture du fichier {pFileName}");
				return null;
			}
			Console.WriteLine(listFilesJson);

			List<FilePdf> listFilesPdf;
			try
			{
				listFilesPdf = JsonConvert.DeserializeObject<List<FilePdf>>(listFilesJson);
			}
			catch (Exception)
			{
				Console.WriteLine("Erreur : Les données json ne sont pas valide");
				return null;
			}
			return listFilesPdf;
		}

		private void BtnAjouterNom_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
			{
				lstNames.Items.Add(txtName.Text);
				txtName.Clear();
			}
		}

		private void lstNames_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var b = lstNames.SelectedItem as FilePdf;
			//MessageBox.Show(b.ToString());
			DetailFileWindow detailFileWindow = new();
			detailFileWindow.FilePdfTxtBox.Text = b.FileName;
			detailFileWindow.ShowDialog();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Program.StartingMethode();
			List<FilePdf> listFilesFromJson_Wpf = GetFilesFromJson(Utilities.parametersPathFile);
			lstNames.ItemsSource = listFilesFromJson_Wpf;
			//lstNames.DataContext = listFilesFromJson_Wpf;
		}
	}
}
