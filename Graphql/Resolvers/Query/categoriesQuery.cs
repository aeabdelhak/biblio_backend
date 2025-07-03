using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Query
{
    public class CategoriesQuery(ICategoriesService _service)
    {
		public IQueryable<Category> GetCategories()
        
        {
        	return _service.GetCategories();
        }
		public IQueryable<Category> GetHomeCategories()
        {
        	return _service.GetHomeCategories();
        }
    }
}