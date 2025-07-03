using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;

namespace BiblioPfe.Graphql.Types.Mutation
{
    public class AuthMutationType : ObjectType<AuthMutation>
    {
        protected override void Configure(IObjectTypeDescriptor<AuthMutation> descriptor)
        {
            descriptor.Field(e => e.Authenticate(default!,default!));
            descriptor.Field(e => e.Register(default!));
            descriptor.Field(e => e.RenewAccessToken(default!));
        }
    }
}