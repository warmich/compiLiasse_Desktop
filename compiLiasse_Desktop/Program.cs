using System.Collections.Generic;
using System.Text;

using static compiLiasse_Desktop.Utilities;

namespace compiLiasse_Desktop
{
	public static class Program
	{
		private static List<FilePdf> listeFilesFromJson_Con;

		public static List<FilePdf> ListeFilesFromJson_Con { get => listeFilesFromJson_Con; set => listeFilesFromJson_Con = value; }

		static void MainConsole(string[] args)
		{
			StartingMethode();
		}

		public static void StartingMethode()
		{
			//Console.OutputEncoding = Encoding.UTF8; // vérifier si l'affichage fonctionne toujours
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			PdfCore.NewPdf();

			StartTests();

			List<FilePdf> ListeFilesFromJson = GetFilesFromJson(parametersPathFile);

			WriteListFilesConsole(ListeFilesFromJson);

			PdfCore.MergePdfs(ListeFilesFromJson);

			PdfCore.creatingPdf();

			//try
			//{
			//	string resultatLecture = File.ReadAllText(parametersPathFile);
			//	Console.WriteLine(resultatLecture);
			//	var listeLecture = File.ReadAllLines(parametersPathFile);
			//	foreach (var item in listeLecture)
			//	{
			//		Console.WriteLine(item);
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine($"Erreur ce fichier n'existe pas : {ex.Message}");
			//}
		}

		// Pour l'affichage utiliser une ListView (affichage type excel)
		#region Méthodes Non utilisées

		//private static List<FilePdf> GetFilesFromUrl(string pUrlName)
		//{
		//	string listeFilePdfFromUrl;
		//	try
		//	{
		//		var WebClient = new WebClient();
		//		listeFilePdfFromUrl = WebClient.DownloadString(pUrlName);
		//	}
		//	catch (WebException ex)
		//	{
		//		if (ex.Response != null)
		//		{
		//			var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
		//			if (statusCode == HttpStatusCode.NotFound)
		//			{
		//				Console.WriteLine($"Erreur 0 : Les données json n'existent pas à l'adresse : {pUrlName}");
		//			}
		//			else
		//			{
		//				Console.WriteLine("ERREUR RESEAU 1 : " + statusCode);
		//			}
		//		}
		//		else
		//		{
		//			Console.WriteLine("ERREUR RESEAU 2 : " + ex.Message);
		//		}
		//		return null;
		//	}
		//	List<FilePdf> listeFiles;
		//	try
		//	{
		//		listeFiles = JsonConvert.DeserializeObject<List<FilePdf>>(listeFilePdfFromUrl);
		//	}
		//	catch (Exception)
		//	{
		//		Console.WriteLine("Erreur : Les données json ne sont pas valide");
		//		return null;
		//	}
		//	return listeFiles;
		//}

		//private static void VerifierSiIngredient(List<FilePdf> pListeFilesFromJson, string pIngredientCherche)
		//{
		//	pListeFilesFromJson = pListeFilesFromJson.Where(f => f.ContientCetIngredient(pIngredientCherche)).ToList();
		//}

		#endregion

	}
}
