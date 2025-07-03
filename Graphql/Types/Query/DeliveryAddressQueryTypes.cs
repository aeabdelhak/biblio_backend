using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;

namespace BiblioPfe.Graphql.Types.Query
{
    public class DeliveryAddressQueryTypes : ObjectTypeExtension<DeliveryAddressQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<DeliveryAddressQuery> descriptor)
        {
            descriptor.ExtendsType<AuthQuery>();
            descriptor.Field(e => e.GetAddresses()).Authorize();
        }
    }
}
