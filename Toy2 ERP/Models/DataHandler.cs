﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toy2_ERP.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Toy2_ERP.Models
{
	public class DataHandler
	{
		// installer EPPlus i Package Manager Console
		// View -> Other Windows -> Package Manager Console
		// Text til konsollen: Install-Package EPPlus
		public void SaveToExcel(List<Connectors> storageList, List<Order> orders, List<Customer> customers, string filePath)
		{
			FileInfo file = new FileInfo(filePath);
			ExcelPackage package;
			if (file.Exists)
			{
				using (package = new ExcelPackage(file))
				{
					// Updatere eksisterende Excel file
					UpdateExcel(package, storageList, orders, customers);
					package.Save();
				}
			}
			else
			{
				// Opretter ny Excel file hvis den ikke eksisterer.
				package = new ExcelPackage(file);
				SaveDataToExcel(package, storageList, orders, customers);
				package.Save();
			}
		}
		private void UpdateExcel(ExcelPackage package, List<Connectors> storageList, List<Order> orders, List<Customer> customers)
		{
			ExcelWorksheet storageSheet = package.Workbook.Worksheets["StorageList"];
			ExcelWorksheet ordersSheet = package.Workbook.Worksheets["Orders"];
			ExcelWorksheet customersSheet = package.Workbook.Worksheets["Customers"];

			// Updaterer eksisterende data
			SaveStorageList(storageSheet, storageList);
			SaveOrders(ordersSheet, orders);
			SaveCustomers(customersSheet, customers);
		}
		private void SaveDataToExcel(ExcelPackage package, List<Connectors> storageList, List<Order> orders, List<Customer> customers)
		{
			// Opretter worksheets
			ExcelWorksheet storageSheet = package.Workbook.Worksheets.Add("StorageList");
			ExcelWorksheet ordersSheet = package.Workbook.Worksheets.Add("Orders");
			ExcelWorksheet customersSheet = package.Workbook.Worksheets.Add("Customers");

			// Gemmer data til de forskellige worksheets
			SaveStorageList(storageSheet, storageList);
			SaveOrders(ordersSheet, orders);
			SaveCustomers(customersSheet, customers);
		}

		private void SaveStorageList(ExcelWorksheet storageSheet, List<Connectors> storageList)
		{
			storageSheet.Cells[1, 1].Value = "Name";
			storageSheet.Cells[1, 2].Value = "Product ID";
			storageSheet.Cells[1, 3].Value = "Cost";
			storageSheet.Cells[1, 4].Value = "Amount";

			int storageRow = 2;
			foreach (var connector in storageList)
			{
				storageSheet.Cells[storageRow, 1].Value = connector.Name;
				storageSheet.Cells[storageRow, 2].Value = connector.ProductId;
				storageSheet.Cells[storageRow, 3].Value = connector.Cost;
				storageSheet.Cells[storageRow, 4].Value = connector.Amount;
				storageRow++;
			}
		}

		private void SaveOrders(ExcelWorksheet ordersSheet, List<Order> orders)
		{
			ordersSheet.Cells[1, 1].Value = "Order ID";
			ordersSheet.Cells[1, 2].Value = "Customer ID";
			ordersSheet.Cells[1, 3].Value = "BoxSet";
			ordersSheet.Cells[1, 4].Value = "Amount";

			int ordersRow = 2;
			foreach (var order in orders)
			{
				foreach (var boxSet in order.BoxSets)
				{
					ordersSheet.Cells[ordersRow, 1].Value = order.OrderID;
					ordersSheet.Cells[ordersRow, 2].Value = order.CustomerID;
					ordersSheet.Cells[ordersRow, 3].Value = boxSet.boxSet.Name;
					ordersSheet.Cells[ordersRow, 4].Value = boxSet.amount;
					ordersRow++;
				}
			}
		}

		private void SaveCustomers(ExcelWorksheet customersSheet, List<Customer> customers)
		{
			customersSheet.Cells[1, 1].Value = "Customer ID";
			customersSheet.Cells[1, 2].Value = "Company Name";
			customersSheet.Cells[1, 3].Value = "Contact Person";
			customersSheet.Cells[1, 4].Value = "Telephone";
			customersSheet.Cells[1, 5].Value = "Email";

			int customersRow = 2;
			foreach (var customer in customers)
			{
				customersSheet.Cells[customersRow, 1].Value = customer.CustomerID;
				customersSheet.Cells[customersRow, 2].Value = customer.CompanyName;
				customersSheet.Cells[customersRow, 3].Value = customer.ContactPerson;
				customersSheet.Cells[customersRow, 4].Value = customer.Telephone;
				customersSheet.Cells[customersRow, 5].Value = customer.Email;
				customersRow++;
			}
		}
		public void ReadFromExcel(string filePath)
		{
			FileInfo file = new FileInfo(filePath);
			if (!file.Exists)
			{
				Console.WriteLine("Filen eksisterer ikke.");
				return;
			}

			using (ExcelPackage package = new ExcelPackage(file))
			{
				ExcelWorksheet storageSheet = package.Workbook.Worksheets["StorageList"];
				ExcelWorksheet ordersSheet = package.Workbook.Worksheets["Orders"];
				ExcelWorksheet customersSheet = package.Workbook.Worksheets["Customers"];

				if (storageSheet != null)
				{
					int rowCount = storageSheet.Dimension.Rows;
					int columnCount = storageSheet.Dimension.Columns;

					// Kører over hver linje i arket.
					for (int row = 2; row <= rowCount; row++) // Første row indeholder attribut navne
					{
						string name = storageSheet.Cells[row, 1].GetValue<string>();
						int productId = storageSheet.Cells[row, 2].GetValue<int>();
						double cost = storageSheet.Cells[row, 3].GetValue<double>();
						int amount = storageSheet.Cells[row, 4].GetValue<int>();

						// instantiere connector ud fra den læste data.
						Connectors connector = new Connectors
						{
							Name = name,
							ProductId = productId,
							Cost = cost,
							Amount = amount
						};

						// Tilføjer connector til lagerlisten
						Connectors.StorageList.Add(connector);
					}
				}
				else
				{
					Console.WriteLine("Storage worksheet ikke fundet.");
				}

				if (ordersSheet != null)
				{
					int rowCount = ordersSheet.Dimension.Rows;
					int columnCount = ordersSheet.Dimension.Columns;

					// Kører over hver linje i arket.
					for (int row = 2; row <= rowCount; row++) // Første row indeholder attribut navne
					{
						int orderId = ordersSheet.Cells[row, 1].GetValue<int>();
						int customerId = ordersSheet.Cells[row, 2].GetValue<int>();
						string boxSetName = ordersSheet.Cells[row, 3].GetValue<string>();
						int amount = ordersSheet.Cells[row, 4].GetValue<int>();

						// instantiere order ud fra den læste data.
						Order order = new Order(customerId);
						BoxSet boxSet = new BoxSet { Name = boxSetName };
						order.AddBoxSet(boxSet, amount);

						// Tilføjer order til orders listen
						Order.orders.Add(order);
					}
				}
				else
				{
					Console.WriteLine("Orders worksheet ikke fundet.");
				}


				if (customersSheet != null)
				{
					int rowCount = customersSheet.Dimension.Rows;
					int columnCount = customersSheet.Dimension.Columns;

					// Kører over hver linje i arket.
					for (int row = 2; row <= rowCount; row++) // Første row indeholder attribut navne
					{
						int customerId = customersSheet.Cells[row, 1].GetValue<int>();
						string companyName = customersSheet.Cells[row, 2].GetValue<string>();
						string contactPerson = customersSheet.Cells[row, 3].GetValue<string>();
						int telephone = customersSheet.Cells[row, 4].GetValue<int>();
						string email = customersSheet.Cells[row, 5].GetValue<string>();

						// instantiere customer ud fra den læste data.
						Customer customer = new Customer
						{
							CustomerID = customerId,
							CompanyName = companyName,
							ContactPerson = contactPerson,
							Telephone = telephone,
							Email = email
						};

						// Tilføjer customer til customerListen
						Customer.customers.Add(customer);
					}
				}
				else
				{
					Console.WriteLine("Customers worksheet ikke funder.");
				}
			}
		}
	}
}
