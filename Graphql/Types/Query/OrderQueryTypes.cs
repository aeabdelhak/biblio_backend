using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Query
{
	public class OrderQueryTypes :ObjectTypeExtension<OrderQuery>
	{
		protected override void Configure(IObjectTypeDescriptor<OrderQuery> descriptor)
		{
			descriptor.ExtendsType<AuthQuery>();
			descriptor.Field(e=>e.SiteGetOrder(default));
			descriptor.Field(e=>e.GetOrder(default)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.GetOrders()).Authorize(UserRole.Admin.ToString()).UsePaging().UseFiltering();
		}
	}
}