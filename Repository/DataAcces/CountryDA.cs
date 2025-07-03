using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
    public class CountryDA(AppDbContext _db) : ICountryDA
    {
        public IQueryable<Country> GeCountries()
        {
            return _db.Countries.AsQueryable();
        }
    }
}