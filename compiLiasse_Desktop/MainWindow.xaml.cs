﻿using System;
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
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableCollection<FilePdf> ObsCollectionFilesFromJson_Wpf { get; set; }

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
			List<FilePdf> ListFilesFromJson_Wpf = GetFilesFromJson(Utilities.parametersPathFile);
			ObsCollectionFilesFromJson_Wpf = new ObservableCollection<FilePdf>(ListFilesFromJson_Wpf);
			lstNames.ItemsSource = ObsCollectionFilesFromJson_Wpf;
			//lstNames.DataContext = listFilesFromJson_Wpf;

			CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lstNames.ItemsSource);
			view.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
			//PropertyGroupDescription groupDescription = new PropertyGroupDescription("FilePath");
			//view.GroupDescriptions.Add(groupDescription);
		}

		private void btnAddFile_Click(object sender, RoutedEventArgs e)
		{
			ObsCollectionFilesFromJson_Wpf.Add(new FilePdf(6, @"C:\Test", "newFile.pdf"));
		}

		private void btnChangeFile_Click(object sender, RoutedEventArgs e)
		{
			if (lstNames.SelectedItem != null)
			{
				(lstNames.SelectedItem as FilePdf).Id = 7;
				(lstNames.SelectedItem as FilePdf).FilePath = @"D:\Modif";
				(lstNames.SelectedItem as FilePdf).FileName = "FileModif.pdf";
			}
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
	}
}
