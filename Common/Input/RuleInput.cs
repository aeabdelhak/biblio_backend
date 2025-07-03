using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Common.Input
{
    public class RuleInput
    {
        public int WarrantyDays { get; set; }
		public decimal ShippingFees { get; set; }
		public OnOutOfStockEnum OnOutOfStock { get; set; } = OnOutOfStockEnum.Hide;
		public CurrencyEnum Currency { get; set; } = CurrencyEnum.MAD;
    }
}