using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
    public class BookMutation(IBookServices _service)
    {
        public Task<Book> UpdateBook(Guid id, BookInput input) => _service.UpdateBook(id, input);

        public Task<Book> NewBook(BookInput input) => _service.NewBook(input);

        public Task<Book> ToggleBook(Guid id, bool active) => _service.ToggleBook(id, active);

        public Task<BaseReturnEnum> DeleteBook(Guid id) => _service.DeleteBook(id);

        public Task<List<BookOrderOption>> UpdateBookOptions(
            Guid id,
            BookOptionInput selling,
            BookOptionInput borrowing
        ) => _service.UpdateBookOptions(id, selling, borrowing);
    }
}
