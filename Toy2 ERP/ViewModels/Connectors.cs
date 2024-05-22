using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toy2_ERP.Models;

namespace Toy2_ERP.ViewModels
{
	public class Connectors : Products
	{
		public double StorageValue { get; set; }
		public DataHandler DataHandler = new DataHandler();
		public Connectors()
		{
		}
		/// <summary>
		/// Til at dokumentere din kode, og dets eventuelle parametre.
		/// </summary>
		/// <param name="name">Navnet på connectoren</param>
		/// <param name="cost"></param>
		/// <param name="productId"></param>
		/// <param name="amount"></param>
		public Connectors(string name, double cost, int productId, int amount)
		{
			Name = name;
			Cost = cost;
			ProductId = productId;
			Amount = amount;

			DataHandler.storageList.Add(this);
		}
		public void UpdateProductCost(double newCost)
		{
			Cost = newCost;
		}
		public void UpdateStorageValue(double newStorageValue)
		{
			foreach (var item in storageList)
			{
				StorageValue += item.Cost * item.Amount;
			}
		}
	}
}
