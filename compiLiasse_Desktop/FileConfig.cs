using System.IO;

namespace compiLiasse_Desktop
{
	public class FileConfig
	{
		private static int nbObjects = 0;
		public int Id { get; set; }
		public string FilePathName { get; set; }
		public string Name { get; set; }
		public FileConfig(string filePathName)
		{
			Id = nbObjects++;
			FilePathName = filePathName;
			Name = Path.GetFileNameWithoutExtension(FilePathName);
		}

		public int getId()
		{
			return Id;
		}
	}
}
