using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Query
{
    public class UserQueryTypes : ObjectTypeExtension<UserQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<UserQuery> descriptor)
        {
            descriptor.ExtendsType<AuthQuery>();
            descriptor.Field(e => e.GetUsers()).Authorize(UserRole.Admin.ToString()).UsePaging().UseSorting().UseFiltering();
            descriptor.Field(e => e.GetDeliveryAdresses()).Authorize();
        }
    }
}
