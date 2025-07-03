using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using System.Linq;

namespace backend.Business.Helpers
{
	class SavedFile
	{
		public string? Type { get; set; }
		public string Path { get; set; }
		public string Url { get; set; }
	}

	public class DocumentHelpers(ICommonDA _commonDA)
	{
		private string ConvertToBase64(string plainText)
		{
			byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

			return Convert.ToBase64String(plainTextBytes);
		}

		private async Task<SavedFile?> Save(dynamic file)
		{
		   	string fileName = DateTime.Now.Ticks+"-"+ file.Name;


			var path = System.IO.Path.Combine("/app/wwwroot/", fileName);

			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			SavedFile doc =
				new()
				{
					Path = path,
					Url = $"/{fileName}",
					Type = file.ContentType
				};
			return doc;
		}

		public async Task<Document?> uploadAndSave(dynamic file)
		{
			try
			{
				var saved = await Save(file);
				if (saved == null)
					return null;
				Document doc =
					new()
					{
						Path = saved.Path,
						Url = saved.Url,
						Type = saved.Type,
						Name = file.Name
					};

				await _commonDA.DbInsert(doc);
				return doc;
			}
			catch (Exception ex)
			{
				await _commonDA.DbInsert(new ErrorLog { Message = ex.ToString() });
				return null;
			}
		}

		public async Task<Document?> UploadFormFile(IFormFile file)
		{
			try
			{
				var saved = await Save(file);
				if (saved == null)
					return null;
				Document doc = new Document()
				{
					Path = saved.Path,
					Url = saved.Url,
					Type = saved.Type
				};

				await _commonDA.DbInsert(doc);
				return doc;
			}
			catch
			{
				return null;
			}
		}

		public async Task<bool> DeleteAsync(Document document)
		{
			File.Delete(document.Path);
			await _commonDA.DbDelete(document);
			return true;
		}

		private bool IsPhoto(string fileExtension)
		{
			string[] photoExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
			return Array.Exists(
				photoExtensions,
				ext => ext.Equals(fileExtension, StringComparison.OrdinalIgnoreCase)
			);
		}

		private bool IsVideo(string fileExtension)
		{
			string[] videoExtensions = { ".mp4", ".avi", ".mov", ".wmv" };
			return Array.Exists(
				videoExtensions,
				ext => ext.Equals(fileExtension, StringComparison.OrdinalIgnoreCase)
			);
		}
	}
}
