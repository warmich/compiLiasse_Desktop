using System.Windows;

namespace compiLiasse_Desktop.Views
{
	/// <summary>
	/// Logique d'interaction pour CreateFileWindow.xaml
	/// </summary>
	public partial class CreateFileWindow : Window
	{
		private FilePdf filePdfAdded;
		public CreateFileWindow(FilePdf pFilePdfAdded)
		{
			InitializeComponent();
			DataContext = this;
			this.filePdfAdded = pFilePdfAdded;
		}

		private void CreateFileCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void CreateFileOk_Click(object sender, RoutedEventArgs e)
		{
			filePdfAdded.Id = int.Parse(TxtBox_Id.Text);
			filePdfAdded.FilePath = TxtBox_filePath.Text;
			filePdfAdded.FileName = TxtBox_fileName.Text;
			filePdfAdded.SearchKey = TxtBox_searchKey.Text;
			filePdfAdded.TableContentsName = TxtBox_tableContentsName.Text;
			DialogResult = true;
		}
	}
}
