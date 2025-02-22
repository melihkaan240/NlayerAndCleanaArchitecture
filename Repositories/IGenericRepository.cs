using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace App.Repositories.Extensions
{
    public interface IGenericRepository<T,TId> where T : class where TId: struct
    {

        public Task<bool> AnyAsync(TId id);
        ValueTask AddAsync(T entity);

        // Bir entity'yi siler
        void Delete(T entity);

        // Tüm veriyi sorgular, asenkron değil
        IQueryable<T> GetAll();

        // Id ile bir entity'yi getirir
        ValueTask<T?> GetByIdAsync(int id);

        // Bir entity'yi günceller
        void Update(T entity);

        // Bir predikat ile entity'leri sorgular
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}