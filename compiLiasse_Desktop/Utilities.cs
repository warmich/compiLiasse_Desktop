using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;

namespace compiLiasse_Desktop
{
	public static class Utilities
	{
		public static string pathBase = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		public const string usersListsDir = "compiLiasse";
		public const string parametersDir = "Bin";
		public const string parametersFileName = "Parameters.json";
		public static string usersPath = Path.Combine(pathBase, usersListsDir);
		public static string parametersPath = Path.Combine(usersPath, parametersDir);
		public static string parametersPathFile = Path.Combine(parametersPath, parametersFileName);
		public const string urlName = "https://codeavecjonathan.com/res/pizzas2.json";

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

	}
}
