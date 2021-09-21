using System.Windows;

namespace compiLiasse_Desktop.Views
{
	/// <summary>
	/// Logique d'interaction pour UpdateFileWindow.xaml
	/// </summary>
	public partial class UpdateFileWindow : Window
	{
		private FilePdf filePdf;
		public UpdateFileWindow(FilePdf pFilePdf)
		{
			InitializeComponent();
			DataContext = this;
			this.TxtBox_Id.Text = pFilePdf.Id.ToString();
			this.TxtBox_filePath.Text = pFilePdf.FilePath;
			this.TxtBox_fileName.Text = pFilePdf.FileName;
			this.TxtBox_searchKey.Text = pFilePdf.SearchKey;
			this.TxtBox_tableContentsName.Text = pFilePdf.TableContentsName;
			this.filePdf = pFilePdf;
		}



		private void btnFileModifCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void btnFileModifOK_Click(object sender, RoutedEventArgs e)
		{
			filePdf.Id = int.Parse(TxtBox_Id.Text);
			filePdf.FilePath = TxtBox_filePath.Text;
			filePdf.FileName = TxtBox_fileName.Text;
			filePdf.SearchKey = TxtBox_searchKey.Text;
			filePdf.TableContentsName = TxtBox_tableContentsName.Text;
			DialogResult = true;
		}
	}
}
