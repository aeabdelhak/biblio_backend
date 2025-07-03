using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Business.Services
{
    public class CountryService(ICountryDA _countryDA) : ICountryService
    {
        public IQueryable<Country> GeCountries()
        {
            return _countryDA.GeCountries();
        }
    }
}