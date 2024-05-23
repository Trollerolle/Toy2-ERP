using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toy2_ERP.Models;

namespace Toy2_ERP.ViewModels
{
	public class Customer
	{
		public DataHandler DataHandler = new DataHandler();

		public string CompanyName { get; set; }
		public int CustomerId { get; set; }
		public string ContactPerson { get; set; }
		public int Telephone { get; set; }
		public string Email { get; set; }

		public Customer(string companyName, int customerId, string contactPerson, int telephone, string email)
		{
			CompanyName = companyName;
			CustomerId = customerId;
			ContactPerson = contactPerson;
			Telephone = telephone;
			Email = email;

			AddtoCustomerList(this);
		}
		public void AddtoCustomerList(Customer customer)
		{
			DataHandler.customers.Add(customer);
		}
	}
}
