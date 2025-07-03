using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Business.Helpers;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Business.Services
{
	public class AuthorService(
		ICommonDA _commonDA,
		DocumentHelpers documentHelpers,
		IAuthorDA _authorDA,
		ISeoDataServices _seo
	) : IAuthorService
	{
		public Task<Author?> GetAuthor(Guid id)
		{
			return _authorDA
				.GetAuthors()
				.Include(e => e.Image)
				.FirstOrDefaultAsync(e => e.Id == id);
		}

		public Task<Author?> GetAuthorBySlug(string slug)
		{
			return _authorDA
				.GetAuthors()
				.Include(e => e.Image)
				.Include(e => e.BookAuthors)
				.ThenInclude(w => w.Book)
				.FirstOrDefaultAsync(e => e.SeoData.Slug == slug);
		}

		public IQueryable<Author> GetAuthors()
		{
			return _authorDA
				.GetAuthors()
				.Include(e => e.Image)
				.Include(e => e.Nationalities)
				.ThenInclude(e => e.Country)
				.AsQueryable();
		}

		public async Task<Author> NewAuthor(AuthorInput input)
		{
			try
			{
				
		
			var DisplayName = input.LastName.Trim() + " " + input.FirstName.Trim();
			var item = new Author
			{
				Resume = input.Resume,
				FirstName = input.FirstName.Trim(),
				LastName = input.LastName.Trim(),
				DisplayName = DisplayName,
				Nationalities = input
					.Nationalities.Select(e => new AuthorNationality { CountryId = e })
					.ToList(),
				SeoData = new SeoData
				{
					Slug = await _seo.GenerateSlug(DisplayName),
					MetaDescription = input.Resume,
					MetaTitle = DisplayName
				},
				ImageId = input.Image is not null
					? (await documentHelpers.uploadAndSave(input.Image))?.Id
					: null
			};
			await _commonDA.DbInsert(item);
			return item;
				}
			catch (System.Exception ex)
			{
                await _commonDA.DbInsert(new ErrorLog { Message = ex.ToString() });
				
				throw;
			}
		}

		public async Task<BaseReturnEnum> RemoveAuthor(Guid id)
		{
			var author = await GetAuthor(id);
			if (author is null)
				return BaseReturnEnum.NotFound;
			await _commonDA.DbDelete(author);
			return BaseReturnEnum.Success;
		}

		public async Task<BaseReturnEnum> RemoveAuthors(List<Guid> ids)
		{
			var items = await _authorDA.GetAuthors().Where(e => ids.Contains(e.Id)).ToListAsync();
			await _commonDA.DbDelete(items);
			return BaseReturnEnum.Success;
		}

		public async Task<Author> UpdateAuthor(Guid id, AuthorInput input)
		{
			var item = await _authorDA.GetAuthors().SingleAsync(e => e.Id == id);
			item.Resume = input.Resume;
			item.FirstName = input.FirstName.Trim();
			item.LastName = input.LastName.Trim();
			item.DisplayName = input.LastName.Trim() + " " + input.FirstName.Trim();
			item.ImageId = input.Image is not null
				? (await documentHelpers.uploadAndSave(input.Image))?.Id ?? item.ImageId
				: item.ImageId;
			await _commonDA.DbUpdate(item);
			return item;
		}
	}
}
