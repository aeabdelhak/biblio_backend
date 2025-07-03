using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
	public class SeoMutationTypes : ObjectTypeExtension<SeoMutation>
	{
		protected override void Configure(IObjectTypeDescriptor<SeoMutation> descriptor)
		{

			descriptor.ExtendsType<AuthMutation>();
			descriptor.Field(e => e.UpdateSeoData(default!,default!)).Authorize(UserRole.Admin.ToString());
	
		}
	}
}