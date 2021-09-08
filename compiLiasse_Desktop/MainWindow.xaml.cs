using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

using Newtonsoft.Json;

namespace compiLiasse_Desktop
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = this;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<FilePdf> ObsCollectionFilesFromJson_Wpf { get; set; }
		public FilePdf SelectedPDF { get; set; }

		// Get file faire une injection de dépendance sur le projet ID de Khun
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

		private void btnAddFile_Click(object sender, RoutedEventArgs e)
		{
			ObsCollectionFilesFromJson_Wpf.Add(new FilePdf(6, @"C:\Test", "newFile.pdf"));
		}

		private void btnChangeFile_Click(object sender, RoutedEventArgs e)
		{
			var copy = SelectedPDF;
			SelectedPDF = null;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedPDF"));
			if (copy != null)
			{
				//copy.Id = 3;
				//copy.FilePath = @"D:\Modif";
				//copy.FileName = "FileModif.pdf";
				DetailFileWindow detailFileWindow = new(copy);
				detailFileWindow.Owner = this;
				detailFileWindow.ShowDialog();
			}


			//lstNames.
		}

		private void btnDeleteFile_Click(object sender, RoutedEventArgs e)
		{
			if (lstNames.SelectedItem != null)
			{
				ObsCollectionFilesFromJson_Wpf.Remove(lstNames.SelectedItem as FilePdf);
				// cfr => Répondre aux changement de la liste source de données
				// https://wpf-tutorial.com/fr/38/data-binding/repondre-aux-changements/
			}
		}

		private void lstNames_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var b = lstNames.SelectedItem as FilePdf;
			DetailFileWindow detailFileWindow = new(b);
			detailFileWindow.FilePdfTxtBox.Text = b.FileName;
			//https://docs.microsoft.com/fr-fr/dotnet/desktop/wpf/windows/how-to-open-window-dialog-box?view=netdesktop-5.0
			detailFileWindow.Owner = this;
			detailFileWindow.ShowDialog();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Program.StartingMethode();
			List<FilePdf> ListFilesFromJson_Wpf = GetFilesFromJson(Utilities.parametersPathFile);
			ObsCollectionFilesFromJson_Wpf = new ObservableCollection<FilePdf>(ListFilesFromJson_Wpf);
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ObsCollectionFilesFromJson_Wpf"));

			CollectionView view = CollectionViewSource.GetDefaultView(lstNames.ItemsSource) as CollectionView;
			view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
		}
	}
}
