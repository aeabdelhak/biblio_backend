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
    public class CategoriesMutation(ICategoriesService _categoriesService)
    {
        public Task<Category> CreateCategory(CreateCategoryInput input)
        {
            return _categoriesService.CreateCategory(input);
        }

        public Task<Category> UpdateCategory(Guid id, CreateCategoryInput input) =>
            _categoriesService.UpdateCategory(id, input);

        public Task<BaseReturnEnum> RemoveCategory(Guid id) =>
            _categoriesService.RemoveCategory(id);

        public Task<BaseReturnEnum> RemoveCategories(List<Guid> ids) =>
            _categoriesService.RemoveCategories(ids);
    }
}
