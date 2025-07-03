using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;

namespace BiblioPfe.Graphql.Types.Query
{
    public class RulesQueryTypes:ObjectTypeExtension<RulesQuery>
	{

		protected override void Configure(IObjectTypeDescriptor<RulesQuery> descriptor)
		{
			descriptor.ExtendsType<AuthQuery>();
			descriptor.Field(e=>e.GetPlatformRules());
		
		}
    
        
    }
}