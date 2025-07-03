using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
	public class UserMutationTypes : ObjectTypeExtension<UserMutation>
	{
		protected override void Configure(IObjectTypeDescriptor<UserMutation> descriptor)
		{
			
			descriptor.ExtendsType<AuthMutation>();
			
			descriptor
				.Field(e => e.ToggleUser(default!, default))
				.Authorize(UserRole.Admin.ToString());
				
			descriptor
				.Field(e => e.ChangeUserRole(default!, default))
				.Authorize(UserRole.SuperAdmin.ToString());
				
		}
	}
}
