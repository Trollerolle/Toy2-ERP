using OfficeOpenXml.FormulaParsing.Excel.Functions.Finance.FinancialDayCount;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Toy2_ERP.Models;
using Toy2_ERP.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Toy2_ERP
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DataHandler _dataHandler = new DataHandler();

		public MainWindow()
		{
			
			InitializeComponent();
			string filepath = "Toy2Lager";
			DataHandler.ReadFromExcel(filepath);
		}

		private void OpretOrdre_Clicked(object sender, RoutedEventArgs e)
		{
			DataContext = new Order();
		}

		private void Indkøb_Clicked(object sender, RoutedEventArgs e)
		{
			DataContext = new Connectors();
        }
	}
}