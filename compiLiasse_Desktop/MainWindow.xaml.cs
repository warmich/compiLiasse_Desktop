using System.Windows;

namespace compiLiasse_Desktop
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void BtnAjouterNom_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(txtName.Text) && !lstNames.Items.Contains(txtName.Text))
			{
				lstNames.Items.Add(txtName.Text);
				txtName.Clear();
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			compiLiasse_Console.Program.StartingMethode();
		}
	}
}
