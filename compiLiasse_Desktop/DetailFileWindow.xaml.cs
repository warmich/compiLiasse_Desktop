using System.Windows;

namespace compiLiasse_Desktop
{
	/// <summary>
	/// Logique d'interaction pour DetailFileWindow.xaml
	/// </summary>
	public partial class DetailFileWindow : Window
	{

		private FilePdf filePdf;
		public DetailFileWindow(FilePdf filePdf)
		{
			InitializeComponent();
			DataContext = this;
			this.FilePdfTxtBox.Text = filePdf.FileName;
			this.filePdf = filePdf;
		}

		private void btnFileModifCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void btnFileModifOK_Click(object sender, RoutedEventArgs e)
		{
			filePdf.FileName = FilePdfTxtBox.Text;
			DialogResult = true;
		}
	}
}
