using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
	public class OrderMutationTypes : ObjectTypeExtension<OrderMutation>
	{
		protected override void Configure(IObjectTypeDescriptor<OrderMutation> descriptor)
		{
			descriptor.ExtendsType<AuthMutation>();
			descriptor.Field(e=>e.NewOrder(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.AddToCart(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.RemoveFromCart(default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.UpdateOrderStatus(default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.BorrowNow(default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.EndBorrow(default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.UpdatePaiment(default!,default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.UpdateDeliveringType(default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.SiteAddToCart(default!));
			descriptor.Field(e=>e.SiteRemoveFromCart(default!,default));
			descriptor.Field(e=>e.SiteUpdateCartDetail(default!,default,default));
			descriptor.Field(e=>e.SiteSetCartDeliveryType(default!,default));
			descriptor.Field(e=>e.SiteSetDeliveryAddress(default!,default,default)).Authorize();
			descriptor.Field(e=>e.SitePushOrder(default!)).Authorize();
		}
	}
}