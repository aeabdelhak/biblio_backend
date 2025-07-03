using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
    public class AuthorDA(AppDbContext _dbContext) : IAuthorDA
    {
        public IQueryable<Author> GetAuthors()
        {
            return _dbContext.Authors.AsQueryable();
        }
    }
}