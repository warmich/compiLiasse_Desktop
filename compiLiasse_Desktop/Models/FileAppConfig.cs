using System.IO;

namespace compiLiasse_Desktop
{
	public class FileAppConfig
	{
		#region Propriétés

		private static int nbObjects = 0;
		public int Id { get; set; }
		public string FilePathName { get; set; }
		public string Name { get; set; }

		#endregion

		#region Constructeurs

		public FileAppConfig(string filePathName)
		{
			Id = nbObjects++;
			FilePathName = filePathName;
			Name = Path.GetFileNameWithoutExtension(FilePathName);
		}

		#endregion

		#region Méthodes

		public int getId()
		{
			return Id;
		}

		#endregion
	}
}
