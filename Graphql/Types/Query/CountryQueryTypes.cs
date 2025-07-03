using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;

namespace BiblioPfe.Graphql.Types.Query
{
    public class CountryQueryTypes : ObjectTypeExtension<CountryQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<CountryQuery> descriptor)
        {
            descriptor.ExtendsType<AuthQuery>();
            descriptor.Field(e => e.GeCountries());
        }
    }
}
