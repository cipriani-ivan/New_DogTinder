using System.Linq.Expressions;

namespace NewDogTinder.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "");

        TEntity Insert(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		Task SaveAsync();
		void Dispose(bool disposing);
		void Dispose();
	}
}