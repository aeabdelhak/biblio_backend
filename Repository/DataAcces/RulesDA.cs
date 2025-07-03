using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Repository.DataAcces
{
    public class RulesDA (AppDbContext _db): IRulesDA
    {
        public Task<Rules> GetPlatformRules()
        {
            return _db.Rules.FirstAsync();
        }
    }
}