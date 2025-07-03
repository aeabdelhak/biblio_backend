using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
	public class BookReviewMutationTypes:ObjectTypeExtension<BookReviewMutation>
	{
		protected override void Configure(IObjectTypeDescriptor<BookReviewMutation> descriptor)
		{
			descriptor.ExtendsType<AuthMutation>();
			descriptor.Field(e => e.AddBookReview(default!,default!)).Authorize();
			descriptor.Field(e => e.RemoveBookReview(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.SiteRemoveBookReview(default!)).Authorize();
			descriptor.Field(e => e.ToggleReview(default!,default)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.UpdateBookReview(default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.SiteUpdateBookReview(default!,default!)).Authorize();
		}
	}
}