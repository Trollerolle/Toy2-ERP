using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toy2_ERP.ViewModels
{
	public class Order
	{
		private static int nextOrderId = 1;
		public static List<Order> orders = new List<Order>();
		public int OrderID { get; }
		public int CustomerID { get; }
		public List<(BoxSet boxSet, int amount)> BoxSets { get; }

		public Order(int customerID)
		{
			OrderID = nextOrderId++;
			CustomerID = customerID;
			BoxSets = new List<(BoxSet, int)>();
		}

		public Order()
		{
		}

		public void AddBoxSet(BoxSet boxSet, int amount)
		{
			foreach (var connector in boxSet.BoxProductList)
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
			foreach (var connector in boxSet.BoxProductList)
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
