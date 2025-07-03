using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
    public class SeoDataDA(AppDbContext _dbContext) : ISeoDataDA
    {
        public IQueryable<SeoData> GetSeoDatas()
        {
			return _dbContext.SeoDatas.AsQueryable();
        }
    }
}