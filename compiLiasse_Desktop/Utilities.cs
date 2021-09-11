using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Microsoft.Win32;

using Newtonsoft.Json;

namespace compiLiasse_Desktop
{
	public static class Utilities
	{
		public static string pathBase = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		public const string USERS_LIST_DIR = "compiLiasse";
		public const string APPLICATION_DIR = "Bin";
		public const string CONFIG_DIR = "Config";
		public const string PARAMETERS_DIR = "Parameters";
		public const string PARAMETERS_FILE_NAME = "Parameters.json";
		public static string usersPath = Path.Combine(pathBase, USERS_LIST_DIR);
		public static string applicationPath = Path.Combine(usersPath, APPLICATION_DIR);
		public static string configPath = Path.Combine(applicationPath, CONFIG_DIR);
		public static string parametersPath = Path.Combine(applicationPath, PARAMETERS_DIR);
		public static string parametersPathFile = Path.Combine(parametersPath, PARAMETERS_FILE_NAME);
		public const string URL_NAME = "https://codeavecjonathan.com/res/pizzas2.json";

		internal static void StartTests()
		{
			bool checkParametersFile = File.Exists(parametersPathFile);
			if (!checkParametersFile)
			{
				Directory.CreateDirectory(parametersPath);
				var StartListBase = GetFilesFromCode();
				GenerateJsonFile(parametersPathFile, StartListBase);
				//File.AppendAllText(parametersPathFile, "Contenu dans le fichier" + Environment.NewLine);
			}
		}
		private static List<FilePdf> GetFilesFromCode()
		{
			return new List<FilePdf>()
			{
				new(1, pathBase, "filePdf1.pdf"),
				new(2, pathBase, "filePdf2.pdf"),
				new(3, pathBase, "filePdf3.pdf"),
				new(4, pathBase, "filePdf4.pdf"),
				new(5, pathBase, "filePdf5.pdf")
			};
		}

		private static void GenerateJsonFile(string pFileName, List<FilePdf> pListeFiles)
		{
			File.WriteAllText(pFileName, JsonConvert.SerializeObject(pListeFiles));
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

		internal static void WriteListFilesConsole(List<FilePdf> pListe)
		{
			if (pListe != null)
			{
				foreach (var item in pListe)
				{
					item.ShowFileExistOrNot();
				}
			}
		}

		internal static void SaveListFilePdf(IEnumerable pListFilePdf)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Liste Config (*.config)|*.config";
			saveFileDialog.InitialDirectory = configPath;
			if (saveFileDialog.ShowDialog() == true)
				File.WriteAllText(saveFileDialog.FileName, JsonConvert.SerializeObject(pListFilePdf));
		}

		internal static void SpratchiLaListe(IEnumerable pListFilePdf, string fileName)
		{
			string NameWithExtension = $"{fileName}.config";
			string pathFileComplete = Path.Combine(configPath, NameWithExtension);
			File.WriteAllText(pathFileComplete, JsonConvert.SerializeObject(pListFilePdf));
		}
	}
}
