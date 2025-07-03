using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Graphql.Resolvers.Query;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Query
{
	public class BookReviewQueryTypes: ObjectTypeExtension<BookReviewQuery>
	{
		protected override void Configure(IObjectTypeDescriptor<BookReviewQuery> descriptor)
		{
			descriptor.ExtendsType<AuthQuery>();
			descriptor.Field(e => e.GetBookReviews(default)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.SiteGetBookReviews(default));
		}
	}
}