using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace compiLiasse_Desktop.BLL
{
	// For Directory.GetFiles and Directory.GetDirectories
	// For File.Exists, Directory.Exists
	// https://docs.microsoft.com/fr-fr/dotnet/api/system.io.directory.getfiles?view=net-5.0

	// ==> Move to Utilities ???

	public static class FilesInDirectory
	{
		public static List<FileAppConfig> ListFilesCongigCatched { get; set; }

		public static List<FileAppConfig> ProcessRecursiveFile(List<string> pListFiles)
		{
			ListFilesCongigCatched = new List<FileAppConfig>();
			foreach (string path in pListFiles)
			{
				if (File.Exists(path))
				{
					// This path is a file
					ProcessFile(path);
				}
				else if (Directory.Exists(path))
				{
					// This path is a directory
					ProcessDirectory(path);
				}
				else
				{
					throw new FileNotFoundException("{0} is not a valid file or directory.", path);
				}
			}
			return ListFilesCongigCatched;
		}

		// Process all files in the directory passed in, recurse on any directories
		// that are found, and process the files they contain.
		public static void ProcessDirectory(string targetDirectory)
		{
			// Process the list of files found in the directory.
			IEnumerable fileEntries = Directory.GetFiles(targetDirectory);
			foreach (string fileName in fileEntries)
				ProcessFile(fileName);

			// Recurse into subdirectories of this directory.
			IEnumerable subdirectoryEntries = Directory.GetDirectories(targetDirectory);
			foreach (string subdirectory in subdirectoryEntries)
				ProcessDirectory(subdirectory);
		}

		// Insert logic for processing found files here.
		public static void ProcessFile(string path)
		{
			ListFilesCongigCatched.Add(new FileAppConfig(path)); // Path.GetFileNameWithoutExtension(path)
		}
	}
}
