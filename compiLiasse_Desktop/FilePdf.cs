using System;
using System.Collections.Generic;
using System.Linq;

namespace compiLiasse_Desktop
{
	public class FilePdf : IEquatable<FilePdf>, IComparable
	{
		#region Paramètres et Constructeurs

		int id;
		string filePath;
		string fileName;
		string searchKey;
		string tableContentsName;
		bool fileExist;

		public int Id { get => id; set => id = value; }
		public string FilePath { get => filePath; set => filePath = value; }
		public string FileName { get => fileName; set => fileName = value; }
		public string SearchKey { get => searchKey; set => searchKey = value; }
		public string TableContentsName { get => tableContentsName; set => tableContentsName = value; }
		public bool FileExist { get => fileExist; set => fileExist = value; }

		public FilePdf(int id, string filePath, string fileName, string searchKey = null, string tableContentsName = null, bool fileExist = false)
		{
			Id = id;
			FilePath = (filePath != null) ? filePath : throw new ArgumentNullException(nameof(filePath));
			FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
			SearchKey = searchKey;
			TableContentsName = tableContentsName;
			FileExist = fileExist;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as FilePdf);
		}

		public bool Equals(FilePdf other)
		{
			return other != null &&
					Id == other.Id &&
					FilePath == other.FilePath &&
					FileName == other.FileName &&
					SearchKey == other.SearchKey &&
					TableContentsName == other.TableContentsName &&
					FileExist == other.FileExist;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Id, FilePath, FileName, SearchKey, TableContentsName, FileExist);
		}

		public override string ToString() => $"{Id} - {FilePath}{FileName}";

		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}

			if (obj is FilePdf x)
			{
				return CompareTo(x);
			}

			throw new ArgumentException("", nameof(obj));
		}

		public static bool operator ==(FilePdf left, FilePdf right)
		{
			return EqualityComparer<FilePdf>.Default.Equals(left, right);
		}

		public static bool operator !=(FilePdf left, FilePdf right)
		{
			return !(left == right);
		}

		#endregion

		internal static void ShowListOfExistingFiles(List<FilePdf> pListFiles)
		{
			pListFiles = pListFiles.Where(f => f.FileExist).ToList();
			Console.WriteLine("Liste des Fichiers Existants :");
			Console.WriteLine();
			foreach (var item in pListFiles)
			{
				item.ShowFileExistOrNot();
			}
		}

		internal void ShowFileExistOrNot()
		{
			//string nomFormate = FormaterNom(FileName);
			//var ingredientsFormates = Ingredients.ConvertAll(item => FormaterNom(item));
			string existe = FileExist ? " (V)" : null;
			Console.WriteLine($"- {Id} {FileName}{existe}");
			//Console.WriteLine(String.Join(", ", ingredientsFormates));
			Console.WriteLine();
		}

		internal static void SortFilesById(List<FilePdf> pListFiles)
		{
			pListFiles = pListFiles.OrderBy(p => p.Id).ToList();
		}

		internal static void SortFilesByDescendingId(List<FilePdf> pListFiles)
		{
			pListFiles = pListFiles.OrderByDescending(p => p.Id).ToList();
		}

		private static string FormaterNom(string pString) => $"{pString[0].ToString().ToUpper()}{pString[1..].ToLower()}";

		// Méthodes Obsolètes
		//internal bool ContientCetIngredient(string pIngredient)
		//{
		//	return Ingredients.Where(ingredient => ingredient.Contains(pIngredient, StringComparison.OrdinalIgnoreCase)).ToList().Count() > 0;
		//}

		//internal static void AfficherPizzaPlusMoinsCher(List<Pizza> pListePizzas)
		//{
		//	Pizza laPlusCher = pListePizzas[0];
		//	Pizza laMoinsCher = pListePizzas[0];
		//	for (int i = 1; i < pListePizzas.Count; i++)
		//	{
		//		if (pListePizzas[i].Prix < laMoinsCher.Prix)
		//		{
		//			laMoinsCher = pListePizzas[i];
		//		}
		//		else if (pListePizzas[i].Prix >= laPlusCher.Prix)
		//		{
		//			laPlusCher = pListePizzas[i];
		//		}
		//	}
		//	Console.WriteLine("La Pizza la moins cher est la :");
		//	laMoinsCher.Afficher();
		//	Console.WriteLine("La Pizza la plus cher est la :");
		//	laPlusCher.Afficher();
		//}
	}

	//Class dérivée
	//internal class PizzaPersonnalisee : Pizza
	//{
	//	static int numeroPizzaPerso = 0;
	//	public PizzaPersonnalisee() : base("Personnalisée", 5f, false, null)
	//	{
	//		Ingredients = new List<string>();
	//		numeroPizzaPerso++;
	//		Nom = $"{Nom} {numeroPizzaPerso}";
	//		while (true)
	//		{
	//			Console.Write($"Rentrez un ingrédient pour la Pizza {Nom} (ENTER pour terminer) : ");
	//			var ingredientPerso = Console.ReadLine();
	//			if (String.IsNullOrWhiteSpace(ingredientPerso))
	//			{
	//				Console.WriteLine();
	//				break;
	//			}
	//			if (Ingredients.Contains(ingredientPerso))
	//			{
	//				Console.WriteLine($"Vous avez déjà choisi {ingredientPerso} !");
	//			}
	//			else
	//			{
	//				Ingredients.Add(ingredientPerso);
	//				Prix += 1.5f;
	//				Console.WriteLine("Vous avez choisi : " + String.Join(", ", Ingredients));
	//			}
	//			Console.WriteLine();
	//		}
	//	}
	//}
}