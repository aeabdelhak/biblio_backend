using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Query
{
    public class AuthQueryType  : ObjectType<AuthQuery>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthQuery> descriptor)
        {
            descriptor.Field(e => e.CurrentUser()).Authorize();
        }
    }
}