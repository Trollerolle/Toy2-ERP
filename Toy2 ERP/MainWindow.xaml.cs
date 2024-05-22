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
using System.IO;

namespace Toy2_ERP
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public DataHandler _dataHandler = new DataHandler();

		public MainWindow()
		{
			string fileName = "Toy2Lager.xlsx";
			string Dir = Directory.GetCurrentDirectory();
			string filePath = System.IO.Path.Combine(Dir, fileName);

			_dataHandler.ReadFromExcel(filePath);
			InitializeComponent();
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