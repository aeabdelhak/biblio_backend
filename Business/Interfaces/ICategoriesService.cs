using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
	public interface ICategoriesService
	{
		public Task<Category> CreateCategory(CreateCategoryInput input);
		public IQueryable<Category> GetCategories();
		public Task<Category> UpdateCategory(Guid id, CreateCategoryInput input);
		public  Task<BaseReturnEnum> RemoveCategory(Guid id);
		public Task<BaseReturnEnum> RemoveCategories(List<Guid> ids);
		public IQueryable<Category> GetHomeCategories();
		

		
		
		
	}
}