using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Query
{
    public class SeoQuery(ISeoDataServices _service)
    {
        public  Task<SeoData?> GetSeoData(Guid id)=>_service.GetSeoData(id);
        
    }
}