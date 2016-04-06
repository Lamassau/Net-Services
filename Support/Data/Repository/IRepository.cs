using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace tekno.Services.Data
{

    public interface IUnitOfWork : IDisposable
    {
        bool IsInTransaction { get ;}
        
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        void SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);

        teknoContext db { get; }
        
    }

    public interface IRepository<T> : IDisposable  where T : teknoData
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<T> AllItems { get; }

        IQueryable<T> Find(Expression<Func<T, bool>> criteria);

        T Single(Expression<Func<T, bool>> criteria);
        T First(Expression<Func<T, bool>> criteria);

        bool Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        bool IsValid(T entity);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> criteria);

        Task<T> FirstAsync(Expression<Func<T, bool>> criteria);
        Task<T> SingleAsync(Expression<Func<T, bool>> criteria);

    }

    
    
}
