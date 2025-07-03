using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;

namespace BiblioPfe.Graphql.Types.Query
{
	public class CarouselQueryTypes:ObjectTypeExtension<CarouselQuery>
	{
		protected override void Configure(IObjectTypeDescriptor<CarouselQuery> descriptor)
		{
			descriptor.ExtendsType<AuthQuery>();
			descriptor.Field(e => e.GetCarouselSlides());
		}
	}
}