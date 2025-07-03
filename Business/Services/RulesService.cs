using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Business.Services
{
	public class RulesService(IRulesDA _rulesDA,ICommonDA _commonDA ) : IRulesService
	{
		public Task<Rules> GetPlatformRules()
		{
			return _rulesDA.GetPlatformRules();
		}

		public async Task<Rules> UpdatePlatformRules(RuleInput input)
		{
			var rules=await _rulesDA.GetPlatformRules();
			rules.OnOutOfStock = input.OnOutOfStock;
			rules.WarrantyDays = input.WarrantyDays;
			rules.Currency = input.Currency;
			rules.ShippingFees = input.ShippingFees;
			await _commonDA.DbUpdate(rules);
			return rules;
			}
	}
}