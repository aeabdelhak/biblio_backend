using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
	public class CarouselMutationTypes :  ObjectTypeExtension<CarouselMutation>
	{
		protected override void Configure(IObjectTypeDescriptor<CarouselMutation> descriptor)
		{
			descriptor.ExtendsType<AuthMutation>();
			descriptor.Field(e => e.RemoveCarouselSlide(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.NewCarouselSlide(default!)).Authorize(UserRole.Admin.ToString());
			
		}
	}
}