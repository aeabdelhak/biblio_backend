using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Query
{
    public class SeoQueryTypes : ObjectTypeExtension<SeoQuery>
    {
        	protected override void Configure(IObjectTypeDescriptor<SeoQuery> descriptor)
		{
			descriptor.ExtendsType<AuthQuery>();
			descriptor.Field(e=>e.GetSeoData(default)).Authorize(UserRole.Admin.ToString());
		
		}
    }
}