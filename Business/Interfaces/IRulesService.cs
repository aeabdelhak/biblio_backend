using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
    public interface IRulesService
    {
        public Task<Rules> UpdatePlatformRules(RuleInput input);
        public Task<Rules> GetPlatformRules();
    }
}