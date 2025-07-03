using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
    public class BookDA(AppDbContext _db) : IBookDA
    {
        public IQueryable<Book> GetBooks()
        {
           return  _db.Books.AsQueryable();
        }

        public IQueryable<BookOrderOption> GetBooksOptions()
        {
            return _db.BookOrderOptions.AsQueryable();
        }
    }
}