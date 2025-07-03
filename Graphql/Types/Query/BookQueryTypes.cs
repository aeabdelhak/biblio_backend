using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Query
{
    public class BookQueryTypes:ObjectTypeExtension<BookQuery>
    {
        	protected override void Configure(IObjectTypeDescriptor<BookQuery> descriptor)
		{
			descriptor.ExtendsType<AuthQuery>();
			descriptor.Field(e=>e.GetBookAsAdmin(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e=>e.GetBookBySlug(default!));
			descriptor.Field(e=>e.GetSimilarToBook(default!));
			descriptor.Field(e=>e.GetSameAuthorBooks(default!));
			descriptor.Field(e=>e.GetBooks()).Authorize(UserRole.Admin.ToString()).UsePaging().UseSorting().UseFiltering();
		}
    }
}