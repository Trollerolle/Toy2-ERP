using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toy2_ERP.ViewModels
{
	public class Customer
	{
		public string CompanyName { get; set; }
		public int CustomerID { get; set; }
		public string ContactPerson { get; set; }
		public int Telephone { get; set; }
		public string Email { get; set; }
		public static List<Customer> customers = new List<Customer>();

	}
}
