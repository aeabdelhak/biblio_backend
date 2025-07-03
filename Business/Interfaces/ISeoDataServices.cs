using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
    public interface ISeoDataServices
    {
        public Task<SeoData> UpdateSeoData(Guid id, SeoDataInput input);
        public Task<SeoData?> GetSeoData(Guid id);
        public Task<string> GenerateSlug(string baseName,string? current = null,bool? first=true);
    }
}