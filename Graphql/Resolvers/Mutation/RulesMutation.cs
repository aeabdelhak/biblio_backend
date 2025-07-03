using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
    public class RulesMutation(IRulesService _service)
    {
        public Task<Rules> UpdatePlatformRules(RuleInput input)=>_service.UpdatePlatformRules(input);
        
    }
}