using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
    public class DeliveryAddressMutationTypes : ObjectTypeExtension<DeliveryAddressMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<DeliveryAddressMutation> descriptor)
        {
            descriptor.ExtendsType<AuthMutation>();
            descriptor.Field(e => e.NewAddress(default!)).Authorize(UserRole.Admin.ToString());
            descriptor.Field(e => e.RemoveAddress(default!)).Authorize(UserRole.Admin.ToString());
            descriptor
                .Field(e => e.UpdateAddress(default!, default!))
                .Authorize(UserRole.Admin.ToString());
        }
    }
}
