using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Repository.Interfaces
{
    public interface IBookDA
    {
                public IQueryable<Book> GetBooks();
                public IQueryable<BookOrderOption> GetBooksOptions();

    }
}