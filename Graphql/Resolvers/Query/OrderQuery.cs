using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Query
{
	public class OrderQuery(IOrderService _service)
	{
		public  Task<Order?> GetOrder(Guid id) => _service.GetOrder(id);
		public IQueryable<Order> GetOrders() => _service.GetOrders();
		public Task<Order?> SiteGetOrder(Guid id) => _service.SiteGetOrder(id);

	}
}