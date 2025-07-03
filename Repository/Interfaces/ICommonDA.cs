namespace BiblioPfe.Repository.Interfaces
{
    public interface ICommonDA
    {
        public Task DbInsert<T>(T entity);
        public Task DbUpdate<T>(T entity);
        public Task DbDelete<T>(T entity);
    }
}
