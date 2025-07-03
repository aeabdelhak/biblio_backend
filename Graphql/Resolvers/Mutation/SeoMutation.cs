using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
    public class SeoMutation(ISeoDataServices _service)
    {
        public  Task<SeoData> UpdateSeoData(Guid id, SeoDataInput input)=>_service.UpdateSeoData(id,input);

    }
}