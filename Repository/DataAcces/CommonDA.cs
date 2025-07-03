using BiblioPfe.Infrastructure;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
	public class CommonDA(AppDbContext _db) : ICommonDA
	{
		public async Task DbDelete<T>(T entity)
		{
		
			if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
			{
				var enumerable = ((IEnumerable<object>)entity).Cast<object>();
				_db.RemoveRange(enumerable);
			}
			else if (
				typeof(T).IsGenericType
				&& typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>)
			)
			{
				_db.RemoveRange(entity);
			}
			else
				_db.Remove(entity);
			await _db.SaveChangesAsync();
			_db.DetachAllEntities();
		}

		public async Task DbInsert<T>(T entity)
		{
		
			if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
			{
				var enumerable = ((IEnumerable<object>)entity).Cast<object>();
				await _db.AddRangeAsync(enumerable);
			}
			else if (
				typeof(T).IsGenericType
				&& typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>)
			)
			{
				await _db.AddRangeAsync(entity);
			}
			else
				await _db.AddAsync(entity);

			await _db.SaveChangesAsync();
			_db.DetachAllEntities();
			
		}

		public async Task DbUpdate<T>(T entity)
		{
		
			if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
			{
				var enumerable = ((IEnumerable<object>)entity).Cast<object>();
				_db.UpdateRange(enumerable);
			}
			else if (
				typeof(T).IsGenericType
				&& typeof(T).GetGenericTypeDefinition() == typeof(IEnumerable<>)
			)
			{
				_db.UpdateRange(entity);
			}
			else
				_db.Update(entity);
			await _db.SaveChangesAsync();
			_db.DetachAllEntities();
			
		}
	}
}
