using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Business.Services
{
	public class SeoDataServices(ISeoDataDA _seoDataDA, ICommonDA _commonDA) : ISeoDataServices
	{
		private string RemoveChars(string input)
		{
			string output = input.ToLowerInvariant();

			output = output.Normalize(NormalizationForm.FormD);

			var sb = new StringBuilder();
			foreach (char c in output)
			{
				if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(c);
				}
			}
			output = sb.ToString();

			output = output.Replace(' ', '-');

			output = Regex.Replace(output, @"[^a-z0-9\-]", "-");

			output = Regex.Replace(output, @"-+", "-");

			output = output.Trim('-');

			return output;
		}
	

	public async Task<string> GenerateSlug(
		string baseName,
		string? current = null,
		bool? first = true
	)
	{
		var name = first is true
			?RemoveChars(baseName)
			: baseName + "-" + DateTime.Now.ToString("yyss");
		name = name.ToLower();
		if (current == name)
			return current;
		var exist = await _seoDataDA.GetSeoDatas().FirstOrDefaultAsync(e => e.Slug == name);
		if (exist is not null)
			return await GenerateSlug(name, current, false);
		return name;
	}

	public Task<SeoData?> GetSeoData(Guid id)
	{
		return _seoDataDA.GetSeoDatas().SingleOrDefaultAsync(e => e.Id == id);
	}

	public async Task<SeoData> UpdateSeoData(Guid id, SeoDataInput input)
	{
		var item = await _seoDataDA.GetSeoDatas().SingleAsync(e => e.Id == id);
		var oldSlug = item.Slug;
		item.Slug = await GenerateSlug(input.Slug, oldSlug);
		item.MetaTitle = input.MetaTitle;
		item.MetaDescription = input.MetaDescription;
		await _commonDA.DbUpdate(item);
		return item;
	}
}
}
