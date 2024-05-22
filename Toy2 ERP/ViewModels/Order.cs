using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toy2_ERP.Models;

namespace Toy2_ERP.ViewModels
{
	public class Order
	{
		DataHandler DataHandeler = new DataHandler();
		private static int nextOrderId = 1;
		public int OrderID { get; }
		public int CustomerID { get; }
		public ObservableCollection<(BoxSet boxSet, int amount)> BoxSets { get; }
		
		// Listen skal laves om til en ObservableCollection fra vores model til ViewModel laget.
		/* ObservableCollection<Order> Orders = new ObservableCollection<Order>();
		
		public void GetOrders(List<Order> orders)
		{
			ObservableCollection<Order> x = new ObservableCollection<Order>(orders);
			Orders = x;
		}
		*/

		public Order(int customerID)
		{
			OrderID = nextOrderId++;
			CustomerID = customerID;
			BoxSets = new ObservableCollection<(BoxSet, int)>();
		}

		public Order()
		{
		}

		public void AddBoxSet(BoxSet boxSet, int amount)
		{
			foreach (var connector in BoxSet.BoxProductList)
			{
				bool found = false;
				foreach (var storedConnector in Connectors.StorageList)
				{
					if (storedConnector.ProductId == connector.ProductId)
					{
						if (storedConnector.Amount < connector.Amount * amount)
						{
							Console.WriteLine($"Ikke nok {connector.Name} connectors på lager.");
							return;
						}
						else
						{
							storedConnector.Amount -= connector.Amount * amount;
							found = true;
							break;
						}
					}
				}

				if (!found)
				{
					Console.WriteLine($"Connector med ID {connector.ProductId} ikke fundet på lageret.");
					return;
				}
			}

			BoxSets.Add((boxSet, amount));
		}

		public void RemoveBoxSet(BoxSet boxSet, int amount)
		{
			var boxSetToRemove = default((BoxSet, int));
			foreach (var boxSetItem in BoxSets)
			{
				if (boxSetItem.boxSet == boxSet)
				{
					boxSetToRemove = boxSetItem;
					break;
				}
			}

			if (boxSetToRemove.Equals(default((BoxSet, int))))
			{
				Console.WriteLine("Denne box er ikke på ordren.");
				return;
			}

			// Tilføjer connectors tilbage til lageret
			foreach (var connector in BoxSet.BoxProductList)
			{
				foreach (var storedConnector in Connectors.StorageList)
				{
					if (storedConnector.ProductId == connector.ProductId)
					{
						storedConnector.Amount += connector.Amount * amount;
						break;
					}
				}
			}
			BoxSets.Remove(boxSetToRemove);
		}
	}
}
