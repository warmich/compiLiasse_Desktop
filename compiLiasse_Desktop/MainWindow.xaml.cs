using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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


		// Get file faire une injection de dépendance => le projet ID de Khun
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
				DetailFileWindow detailFileWindow = new(copy);
				detailFileWindow.Owner = this;

				bool? result = detailFileWindow.ShowDialog();

				if (result == true)
				{
					SelectedPDF = copy;
				}
				else
				{
					//MessageBox.Show("Rien n'a changé");
				}
			}
		}

		private void btnDeleteFile_Click(object sender, RoutedEventArgs e)
		{
			if (lstNames.SelectedItem != null)
			{
				ObsCollectionFilesFromJson_Wpf.Remove(lstNames.SelectedItem as FilePdf);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Program.StartingMethode();
			Cbox_Initialisation();
			LstNames_Initialisation(Utilities.parametersPathFile);
		}

		private void LstNames_Initialisation(string pathFileConfig)
		{
			List<FilePdf> ListFilesFromJson_Wpf = GetFilesFromJson(pathFileConfig);
			ObsCollectionFilesFromJson_Wpf = new ObservableCollection<FilePdf>(ListFilesFromJson_Wpf);
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ObsCollectionFilesFromJson_Wpf"));

			CollectionView view = CollectionViewSource.GetDefaultView(lstNames.ItemsSource) as CollectionView;
			view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
		}

		private void Cbox_Initialisation()
		{
			List<string> changerCetteDaube = new List<string>();
			changerCetteDaube.Add(Utilities.parametersPathFile);
			changerCetteDaube.Add(Utilities.configPath);
			List<FileConfig> filesConfig = FilesInDirectory.ProcessRecursiveFile(changerCetteDaube);
			cboxConfigs.ItemsSource = filesConfig;
			cboxConfigs.DisplayMemberPath = "Name";
			cboxConfigs.SelectedValuePath = "Id";
			cboxConfigs.SelectedValue = "1";
		}

		private void btnSaveList_Click(object sender, RoutedEventArgs e)
		{
			Utilities.SaveListFilePdf(ObsCollectionFilesFromJson_Wpf);
			Cbox_Initialisation();
		}

		private void btnPrevious_Click(object sender, RoutedEventArgs e)
		{
			if (cboxConfigs.SelectedIndex > 0)
				cboxConfigs.SelectedIndex = cboxConfigs.SelectedIndex - 1;
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			if (cboxConfigs.SelectedIndex < cboxConfigs.Items.Count - 1)
				cboxConfigs.SelectedIndex = cboxConfigs.SelectedIndex + 1;
		}

		private void btnBlue_Click(object sender, RoutedEventArgs e)
		{
			FileConfig pathFileConfig = cboxConfigs.SelectedItem as FileConfig;
			LstNames_Initialisation(pathFileConfig.FilePathName);
		}

		private void cboxConfigs_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void btnSpratchList_Click(object sender, RoutedEventArgs e)
		{
			FileConfig pathFileConfig = cboxConfigs.SelectedItem as FileConfig;
			Utilities.SpratchiLaListe(ObsCollectionFilesFromJson_Wpf, pathFileConfig.Name);
			Cbox_Initialisation();
		}
	}
}
