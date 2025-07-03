using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
    public class CategoriesDA(AppDbContext _db) : ICategoriesDA
    {
        public IQueryable<Category> GetCategories()
        {
            return _db.Categories.AsQueryable();
        }
    }
}