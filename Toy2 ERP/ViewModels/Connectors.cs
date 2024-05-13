using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toy2_ERP.ViewModels
{
	public class Connectors : Products
	{
		public static List<Connectors> StorageList = new List<Connectors>();
		public double StorageValue { get; set; }
		public Connectors()
		{
		}

		public Connectors(string name, double cost, int productId, int amount)
		{
			Name = name;
			Cost = cost;
			ProductId = productId;
			Amount = amount;

			StorageList.Add(this);
		}
		public void UpdateProductCost(double newCost)
		{
			Cost = newCost;
		}
		public void UpdateStorageValue(double newStorageValue)
		{
			foreach (var item in StorageList)
			{
				StorageValue += item.Cost * item.Amount;
			}
		}
	}
}
