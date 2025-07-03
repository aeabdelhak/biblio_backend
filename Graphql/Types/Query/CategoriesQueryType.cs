using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;

namespace BiblioPfe.Graphql.Types.Query
{
	public class CategoriesQueryType  :ObjectTypeExtension<CategoriesQuery>
	{

		protected override void Configure(IObjectTypeDescriptor<CategoriesQuery> descriptor)
		{
			descriptor.ExtendsType<AuthQuery>();
			descriptor.Field(e=>e.GetCategories());
			descriptor.Field(e=>e.GetHomeCategories()).UsePaging();
		}
		
	}
}