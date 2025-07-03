using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
    public interface ICountryService
    {
        public IQueryable<Country> GeCountries();
    }
}