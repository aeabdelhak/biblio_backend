using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Business.Services
{
	public class CategoriesService(
		ICommonDA _commonDA,
		ISeoDataServices _seoService,
		ICategoriesDA _categoriesDA
	) : ICategoriesService
	{
		public async Task<Category> CreateCategory(CreateCategoryInput input)
		{
			string slug = await _seoService.GenerateSlug(input.Name);

			Category category =
				new()
				{
					Label = input.Name,
					Description = input.Description,
					SeoData = new()
					{
						Slug = slug,
						MetaTitle = input.Name,
						MetaDescription = input.Description
					}
				};

			await _commonDA.DbInsert(category);
			return category;
		}

		public IQueryable<Category> GetCategories()
		{
			return _categoriesDA.GetCategories().Include(e => e.SeoData);
		}

		public async Task<Category> UpdateCategory(Guid id, CreateCategoryInput input)
		{
			var item = await _categoriesDA.GetCategories().SingleAsync(e => e.Id == id);
			item.Description = input.Description;
			item.Label = input.Name;
			await _commonDA.DbUpdate(item);
			return item;
		}

		public async Task<BaseReturnEnum> RemoveCategory(Guid id)
		{
			var item = await _categoriesDA.GetCategories().SingleOrDefaultAsync(e => e.Id == id);
			if (item is null)
				return BaseReturnEnum.NotFound;

			await _commonDA.DbDelete(item);
			return BaseReturnEnum.Success;
		}

		public async Task<BaseReturnEnum> RemoveCategories(List<Guid> ids)
		{
			var item = await _categoriesDA
				.GetCategories()
				.Where(e => ids.Contains(e.Id))
				.ToListAsync();
			await _commonDA.DbDelete(item);
			return BaseReturnEnum.Success;
		}

		public IQueryable<Category> GetHomeCategories()
		{
			return _categoriesDA
			   .GetCategories()
			   .Include(e => e.BookCategories.Where(e=>e.Book.IsActive).Take(10)).ThenInclude(e => e.Book).ThenInclude(e => e.Options)
			   .Include(e => e.BookCategories.Where(e=>e.Book.IsActive).Take(10)).ThenInclude(e => e.Book).ThenInclude(e => e.SeoData)
			   .Include(e => e.BookCategories.Where(e=>e.Book.IsActive).Take(10)).ThenInclude(e => e.Book).ThenInclude(e => e.Cover)
			   .Include(e => e.BookCategories.Where(e=>e.Book.IsActive).Take(10)).ThenInclude(e => e.Book).ThenInclude(e => e.BookAuthors).ThenInclude(e => e.Author.Image)
			   .Include(e => e.SeoData)
			   .Where(e => e.BookCategories.Where(e=>e.Book.IsActive).Any())
			   .AsQueryable();
		}
	}
}
