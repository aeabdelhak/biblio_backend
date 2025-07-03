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
	public class BookServices(
		IBookDA _bookDA,
		ICategoriesDA _categoriesDA,
		ISeoDataDA _seoDataDA,
		IAuthorDA _authorDA,
		ISeoDataServices _seoService,
		ICommonDA _commonDA,
		DocumentHelpers _doc
	) : IBookServices
	{
		public async Task<BaseReturnEnum> DeleteBook(Guid id)
		{
			var book = await _bookDA.GetBooks().FirstOrDefaultAsync(e => e.Id == id);
			if (book is null)
				return BaseReturnEnum.NotFound;
			await _commonDA.DbDelete(book);
			return BaseReturnEnum.Success;
		}

		public async Task<Book?> GetBookInernly(Guid id)
		{
			return await _bookDA
				.GetBooks()
				.Include(e => e.Options)
				.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<Book?> GetBookAsAdmin(Guid id)
		{
			return await _bookDA
				.GetBooks()
				.Include(e => e.Cover)
				.Include(e => e.Options)
				.Include(e => e.SeoData)
				.Include(e => e.BookCategories)
				.ThenInclude(e => e.Category)
				.Include(e => e.BookAuthors)
				.ThenInclude(e => e.Author.Image)
				.FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task<Book?> GetBookBySlug(string slug)
		{
			return await _bookDA
				.GetBooks()
				.Include(e => e.Cover)
				.Include(e => e.SeoData)
				.Include(e => e.Options)
				.Include(e => e.BookAuthors)
				.ThenInclude(e => e.Author.Image)
				.Include(e => e.BookAuthors)
				.ThenInclude(e => e.Author.SeoData)
				.Include(e => e.BookCategories)
				.ThenInclude(e => e.Category)
				.FirstOrDefaultAsync(e => e.IsActive && e.SeoData.Slug == slug);
		}

		public IQueryable<Book> GetBooks()
		{
			return _bookDA
				.GetBooks()
				.Include(e => e.Cover)
				.Include(e => e.Options)
				.Include(e => e.BookAuthors)
				.ThenInclude(e => e.Author.Image)
				.Include(e => e.BookCategories);
		}

		public async Task<Book> NewBook(BookInput input)
		{
			Book book = new() { Name = input.Name, Resume = input.Resume };

			book.CoverId = input.Cover is not null
				? (await _doc.uploadAndSave(input.Cover))?.Id
				: book.CoverId;
			book.InStock = input.InStock;
			book.IsActive = false;
			book.BookCategories = input
				.BookCategories.Select(e => new BookCategory() { BookId = book.Id, CategoryId = e })
				.ToList();

			book.BookAuthors = input
				.BookAuthors.Select(e => new BookAuthor() { BookId = book.Id, AuthorId = e })
				.ToList();

			book.SeoData = new SeoData
			{
				MetaTitle = input.Name,
				MetaDescription = "",
				Slug = await _seoService.GenerateSlug(input.Name)
			};
			book.Options =
			[
				new BookOrderOption
				{
					Option = BookOrderOptionEnum.Selling,
					IsActive = false,
					Price = 0,
				},
				new BookOrderOption
				{
					Option = BookOrderOptionEnum.Borrowing,
					IsActive = false,
					Price = 0,
				}
			];

			await _commonDA.DbInsert(book);

			return (await GetBookAsAdmin(book.Id))!;
		}

		public async Task<Book> ToggleBook(Guid id, bool active)
		{
			var book = await _bookDA.GetBooks().FirstOrDefaultAsync(e => e.Id == id);
			book.IsActive = active;
			await _commonDA.DbUpdate(book);
			return book;
		}

		public async Task<Book> UpdateBook(Guid id, BookInput input)
		{
			try
			{

				var book = await _bookDA
					.GetBooks()
					.Include(e => e.BookAuthors)
					.Include(e => e.BookCategories)
					.FirstOrDefaultAsync(e => e.Id == id);
				book.Name = input.Name;
				book.Resume = input.Resume;
				book.CoverId = input.Cover is not null
					? (await _doc.uploadAndSave(input.Cover)).Id
					: book.CoverId;
				book.InStock = input.InStock;
				var authorsToAdd = input.BookAuthors.Where(Id =>
					!book.BookAuthors.Any(e => e.AuthorId == Id)
				);
				var categoriesToAdd = input.BookCategories.Where(Id =>
					!book.BookCategories.Any(e => e.CategoryId == Id)
				);
				if (categoriesToAdd.Any())
				{
					var categories = await _categoriesDA
						.GetCategories()
						.Where(e => categoriesToAdd.Contains(e.Id))
						.ToListAsync();
					if (!categoriesToAdd.All(a => categories.Any(e => e.Id == a)))
						throw new Exception("Invalid Category");
				}
				if (authorsToAdd.Any())
				{
					var authors = await _authorDA
						.GetAuthors()
						.Where(e => authorsToAdd.Contains(e.Id))
						.ToListAsync();
					if (!authorsToAdd.All(a => authors.Any(e => e.Id == a)))
						throw new Exception("Invalid Author");
				}

				var authorsToRemove = book
					.BookAuthors.Where(e => !input.BookAuthors.Contains(e.AuthorId))
					.ToList();
				var categoriesToRemove = book
					.BookCategories.Where(e => !input.BookCategories.Contains(e.CategoryId))
					.ToList();

				await _commonDA.DbUpdate(book);

				if (categoriesToRemove.Count != 0)
				{
					await _commonDA.DbDelete(categoriesToRemove);
				}
				if (authorsToRemove.Count != 0)
				{
					await _commonDA.DbDelete(authorsToRemove);
				}
				if (categoriesToAdd.Any())
				{
					var items = categoriesToAdd
						.Select(e => new BookCategory() { BookId = book.Id, CategoryId = e })
						.ToList();
					await _commonDA.DbInsert(items);
				}
				if (authorsToAdd.Any())
				{
					var items = authorsToAdd
						.Select(e => new BookAuthor() { BookId = book.Id, AuthorId = e })
						.ToList();
					await _commonDA.DbInsert(items);
				}
				return (await GetBookAsAdmin(book.Id))!;
			}
			catch (Exception ex)
			{
				await _commonDA.DbInsert(new ErrorLog
				{
					Message = ex.ToString()
				});
				throw new Exception();
			}
		}

		public async Task<List<BookOrderOption>> UpdateBookOptions(
			Guid id,
			BookOptionInput selling,
			BookOptionInput borrowing
		)
		{
			var options = await _bookDA.GetBooksOptions().Where(e => e.BookId == id).ToListAsync();
			foreach (var item in options)
			{
				if (item.Option == BookOrderOptionEnum.Selling)
				{
					item.Price = selling.Price;
					item.IsActive = selling.IsActive;
				}
				else
				{
					item.Price = borrowing.Price;
					item.IsActive = borrowing.IsActive;
				}
				await _commonDA.DbUpdate(item);
			}

			return await _bookDA.GetBooksOptions().Where(e => e.BookId == id).ToListAsync();

		}

		public async Task<List<Book>> GetSimilarToBook(string slug)
		{
			var BookCategories = await _bookDA.GetBooks().Where(e => e.SeoData.Slug == slug).SelectMany(e => e.BookCategories.Select(e => e.CategoryId)).ToListAsync();
			return await _bookDA.GetBooks()
			.Where(e =>e.IsActive && e.BookCategories.Any(e => BookCategories.Contains(e.CategoryId)))
			.Include(e => e.BookAuthors).ThenInclude(e => e.Author)
			.Include(e => e.Options)
			.Include(e => e.Cover)
			.Select(e => new Book
			{
				BookAuthors = e.BookAuthors,
				Cover = e.Cover,
				InStock = e.InStock,
				Options = e.Options,
				Name = e.Name,
				SeoData = new SeoData
				{
					Id = e.Id,
					Slug = e.SeoData.Slug
				}
			}).Take(20).ToListAsync();
		}

		public async Task<List<Book>> GetSameAuthorBooks(string slug)
		{
			var BookAuthors = await _bookDA.GetBooks().Where(e => e.SeoData.Slug == slug).SelectMany(e => e.BookAuthors.Select(e => e.AuthorId)).ToListAsync();
			return await _bookDA.GetBooks()
			.Where(e =>e.IsActive && e.BookAuthors.Any(e => BookAuthors.Contains(e.AuthorId)))
			.Include(e => e.BookAuthors).ThenInclude(e => e.Author)
			.Include(e => e.Options)
			.Include(e => e.Cover)
			
			.Select(e => new Book
			{
				BookAuthors = e.BookAuthors,
				Cover = e.Cover,
				InStock = e.InStock,
				Options = e.Options,
				Name = e.Name,
				SeoData = new SeoData
				{
					Id = e.Id,
					Slug = e.SeoData.Slug
				}
			}).Take(20).ToListAsync();
		}
	}
}
