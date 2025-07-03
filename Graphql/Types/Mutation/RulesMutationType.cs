using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
    public class RulesMutationType:  ObjectTypeExtension<RulesMutation>
    {
        	protected override void Configure(IObjectTypeDescriptor<RulesMutation> descriptor)
		{
			descriptor.ExtendsType<AuthMutation>();
			descriptor.Field(e => e.UpdatePlatformRules(default!)).Authorize(UserRole.Admin.ToString());
		}
    }
}