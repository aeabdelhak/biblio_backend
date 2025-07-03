using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class Rules : BaseEntity
	{
		public int WarrantyDays { get; set; }
		public decimal ShippingFees { get; set; }
		public OnOutOfStockEnum OnOutOfStock { get; set; } = OnOutOfStockEnum.Hide;
		public CurrencyEnum Currency { get; set; } = CurrencyEnum.MAD;
	}
	public enum OnOutOfStockEnum
	{
		Hide,
		Message,
		Available
	}
	
	public enum CurrencyEnum
	{
		MAD,
		EURO
	}

}