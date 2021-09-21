using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using compiLiasse_Desktop.BLL;
using compiLiasse_Desktop.Views;

using Newtonsoft.Json;

namespace compiLiasse_Desktop.ViewModel
{
	internal class MainWindowVM : INotifyPropertyChanged
	{
		private string mWTitle;

		public string MWTitle
		{
			get => mWTitle;
			set => mWTitle = "compiLiasse_desktop";
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public ObservableCollection<FilePdf> ObsCollectionFilesFromJson_Wpf { get; set; }

		public FilePdf SelectedPDF { get; set; }


		//// Get file faire une injection de dépendance => le projet ID de Khun
		//internal static List<FilePdf> GetFilesFromJson(string pFileName)
		//{
		//	string listFilesJson;
		//	try
		//	{
		//		listFilesJson = File.ReadAllText(pFileName);
		//	}
		//	catch (Exception)
		//	{
		//		Console.WriteLine($"Erreur de lecture du fichier {pFileName}");
		//		return null;
		//	}
		//	Console.WriteLine(listFilesJson);

		//	List<FilePdf> listFilesPdf;
		//	try
		//	{
		//		listFilesPdf = JsonConvert.DeserializeObject<List<FilePdf>>(listFilesJson);
		//	}
		//	catch (Exception)
		//	{
		//		Console.WriteLine("Erreur : Les données json ne sont pas valide");
		//		return null;
		//	}
		//	return listFilesPdf;
		//}

		//private void BtnCreateFile_Click(object sender, RoutedEventArgs e)
		//{
		//	FilePdf filePdfAdded = new();
		//	CreateFileWindow createFileWindow = new CreateFileWindow(filePdfAdded);
		//	createFileWindow.Owner = this;
		//	bool? result = createFileWindow.ShowDialog();
		//	if (result == true)
		//	{
		//		ObsCollectionFilesFromJson_Wpf.Add(filePdfAdded);
		//	}
		//	else
		//	{
		//		//MessageBox.Show("Rien n'a changé");
		//	}
		//}

		//private void BtnChangeFile_Click(object sender, RoutedEventArgs e)
		//{
		//	var copy = SelectedPDF;
		//	SelectedPDF = null;
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedPDF"));
		//	if (copy != null)
		//	{
		//		UpdateFileWindow updateFileWindow = new(copy);
		//		updateFileWindow.Owner = this;

		//		bool? result = updateFileWindow.ShowDialog();

		//		if (result == true)
		//		{
		//			SelectedPDF = copy;
		//		}
		//		else
		//		{
		//			//MessageBox.Show("Rien n'a changé");
		//		}
		//	}
		//}

		//private void BtnDeleteFile_Click(object sender, RoutedEventArgs e)
		//{
		//	if (lstNames.SelectedItem != null)
		//	{
		//		ObsCollectionFilesFromJson_Wpf.Remove(lstNames.SelectedItem as FilePdf);
		//	}
		//}

		//private void Window_Loaded(object sender, RoutedEventArgs e)
		//{
		//	StartByDefault.StartingMethode();
		//	Cbox_Initialisation();
		//	LstNames_Initialisation(Utilities.parametersPathFile);
		//}

		//private void LstNames_Initialisation(string pathFileConfig)
		//{
		//	List<FilePdf> ListFilesFromJson_Wpf = GetFilesFromJson(pathFileConfig);
		//	ObsCollectionFilesFromJson_Wpf = new ObservableCollection<FilePdf>(ListFilesFromJson_Wpf);
		//	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ObsCollectionFilesFromJson_Wpf"));

		//	CollectionView view = CollectionViewSource.GetDefaultView(lstNames.ItemsSource) as CollectionView;
		//	view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
		//}

		//private void Cbox_Initialisation(int pId = 1)
		//{
		//	List<string> changerCetteDaube = new List<string>();
		//	changerCetteDaube.Add(Utilities.parametersPathFile);
		//	changerCetteDaube.Add(Utilities.configPath);
		//	List<FileAppConfig> filesConfig = FilesInDirectory.ProcessRecursiveFile(changerCetteDaube);
		//	cboxConfigs.ItemsSource = filesConfig;
		//	cboxConfigs.DisplayMemberPath = "Name";
		//	cboxConfigs.SelectedValuePath = "Id";
		//	cboxConfigs.SelectedValue = pId;
		//}

		//private void BtnSaveList_Click(object sender, RoutedEventArgs e)
		//{
		//	Utilities.SaveListFilePdf(ObsCollectionFilesFromJson_Wpf);
		//	Cbox_Initialisation();
		//}

		//private void BtnPrevious_Click(object sender, RoutedEventArgs e)
		//{
		//	if (cboxConfigs.SelectedIndex > 0)
		//		cboxConfigs.SelectedIndex = cboxConfigs.SelectedIndex - 1;
		//}

		//private void BtnNext_Click(object sender, RoutedEventArgs e)
		//{
		//	if (cboxConfigs.SelectedIndex < cboxConfigs.Items.Count - 1)
		//		cboxConfigs.SelectedIndex = cboxConfigs.SelectedIndex + 1;
		//}

		//private void BtnBlue_Click(object sender, RoutedEventArgs e)
		//{
		//	FileAppConfig pathFileConfig = cboxConfigs.SelectedItem as FileAppConfig;
		//	LstNames_Initialisation(pathFileConfig.FilePathName);
		//}

		//private void CboxConfigs_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{

		//}

		//private void BtnSpratchList_Click(object sender, RoutedEventArgs e)
		//{
		//	FileAppConfig pathFileConfig = cboxConfigs.SelectedItem as FileAppConfig;
		//	Utilities.SpratchiLaListe(ObsCollectionFilesFromJson_Wpf, pathFileConfig.Name);
		//	Cbox_Initialisation();
		//}

		//private void Window_Closed(object sender, EventArgs e)
		//{

		//}
	}
}
