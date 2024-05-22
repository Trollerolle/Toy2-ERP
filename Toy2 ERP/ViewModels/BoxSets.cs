using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toy2_ERP.ViewModels
{
	public class BoxSet : Products
	{
		public static ObservableCollection<Connectors> BoxProductList { get; set; }
		public BoxSet()
		{
			BoxProductList = new ObservableCollection<Connectors>();
		}

		public void AddConnector(Connectors connectors, int amount)
		{
			bool Found = false;

			foreach (var item in BoxProductList)
			{
				if (item.ProductId == connectors.ProductId)
				{
					item.Amount += amount;
					Found = true;
					break;
				}
			}

			if (!Found)
			{
				connectors.Amount = amount;
				BoxProductList.Add(connectors);
			}

			UpdateCost();
		}
		public void RemoveConnector(Connectors connectors, int amount)
		{
			foreach (var item in BoxProductList)
			{
				if (item.ProductId == connectors.ProductId)
				{
					if (item.Amount <= amount)
					{
						BoxProductList.Remove(connectors);
					}
					else
					{
						item.Amount -= amount;
					}
					break;
				}
			}

			UpdateCost();
		}

		public void UpdateCost()
		{
			Cost = 0;

			//Se på om metoden skal opdatere priserne på råvarerne i listen.
			foreach (var item in BoxProductList)
			{
				Cost += item.Cost * item.Amount;
			}
		}
	}
}
