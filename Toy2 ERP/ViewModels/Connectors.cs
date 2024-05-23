using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toy2_ERP.Models;

namespace Toy2_ERP.ViewModels
{
	/// <summary>
	/// Denne her klasse skal være af objektet Connector.
	/// </summary>
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

			AddToStorageList(this);
		}
		public void UpdateProductCost(double newCost)
		{
			Cost = newCost;
		}
		public void UpdateStorageValue(double newStorageValue)
		{
			foreach (var item in DataHandler.storageList)
			{
				StorageValue += item.Cost * item.Amount;
			}
		}
		public void AddToStorageList(Connectors connector)
		{
			DataHandler.storageList.Add(connector);

		}
	}
}
