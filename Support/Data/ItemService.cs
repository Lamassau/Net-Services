
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;



namespace tekno.Services.Data
{
    public interface IDataService<T> where T : teknoData 
    {
        IQueryable<T> Items {get;}
        
       

        void Update(T item, bool NoSave=false);
        void Add(T item, bool NoSave=false);
        void Delete(T item, bool NoSave=false);
        T Single(Expression<Func<T, bool>> criteria);
        T First(Expression<Func<T, bool>> criteria);

        IQueryable<T> Find(Expression<Func<T, bool>> criteria);

        Task<T> SingleAsync(Expression<Func<T, bool>> criteria);
        Task<T> FirstAsync(Expression<Func<T, bool>> criteria);


        Task<List<T>> FindAsync(Expression<Func<T, bool>> criteria);

        Task AddAsync(T item);
        Task DeleteAsync(T item);
        Task UpdateAsync(T item);

        
    }


    public class ItemService<T> :IUnitOfWork, IDisposable, IDataService<T> where T : teknoData
    {
        protected readonly IRepository<T> itemRepository;
       

        public ItemService()
        {
            itemRepository = new Repository<T>();
            
        }

        public ItemService(IRepository<T> itemRepository)
        {
            if (itemRepository == null)
                        throw new ArgumentNullException(" Repository");
            this.itemRepository = itemRepository;
        }


        public IQueryable<T> Items
        {
            get { return itemRepository.AllItems;}
        }


        public void Update(T item, bool NoSave=false)
        {
            if (item == null)
                throw new ArgumentNullException("Item to update");

            if (NoSave)
            {
                if (!itemRepository.UnitOfWork.IsInTransaction)
                    throw new Exception("Transaction does not exist, Use BeginTransaction() before the call");

                itemRepository.Update(item);
            }
            else
            {
                itemRepository.UnitOfWork.BeginTransaction();
                itemRepository.Update(item);
                itemRepository.UnitOfWork.CommitTransaction();
            }
        }

        public void Add(T item, bool NoSave=false)
        {
            if (item == null)
                throw new ArgumentNullException("item to add");

            if (NoSave)
            {
                if (!itemRepository.UnitOfWork.IsInTransaction)
                    throw new Exception("Transaction does not exist, Use BeginTransaction() before the call");

                itemRepository.Add(item);
            }
            else
            {
                itemRepository.UnitOfWork.BeginTransaction();
                itemRepository.Add(item);
                itemRepository.UnitOfWork.CommitTransaction();
            }


        }

        public void Delete(T item, bool NoSave = false)
        {
            if (item == null)
                throw new ArgumentNullException("item to add");

            if (NoSave)
            {
                if (!itemRepository.UnitOfWork.IsInTransaction)
                    throw new Exception("Transaction does not exist, Use BeginTransaction() before the call");

                itemRepository.Delete(item);
            }
            else
            {
                itemRepository.UnitOfWork.BeginTransaction();
                itemRepository.Delete(item);
                itemRepository.UnitOfWork.CommitTransaction();
            }
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> criteria)
        {
            return  itemRepository.Find(criteria);
        }

        public void BeginTransaction()
        { 
            itemRepository.UnitOfWork.BeginTransaction();
        }

        public void CommitTransaction()
        {
            itemRepository.UnitOfWork.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            itemRepository.UnitOfWork.RollBackTransaction(); 
        }

        public Task<int> SaveChangesAsync()
        {
            return itemRepository.UnitOfWork.SaveChangesAsync();
        }

        public void Dispose()
        {
            itemRepository.UnitOfWork.Dispose();
            itemRepository.Dispose();
        }


        public bool IsInTransaction
        {
            get { return itemRepository.UnitOfWork.IsInTransaction; }
        }

        public void SaveChanges()
        {
             itemRepository.UnitOfWork.SaveChanges();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return itemRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }


        public T Single(Expression<Func<T, bool>> criteria)
        {
            return itemRepository.Single(criteria);
        }

        public Task<T> SingleAsync(Expression<Func<T, bool>> criteria)
        {
            return itemRepository.SingleAsync(criteria);
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> criteria)
        {
            return itemRepository.FindAsync(criteria);
        }

        public Task AddAsync(T item)
        {
            itemRepository.Add(item);
            return itemRepository.UnitOfWork.SaveChangesAsync();
        }

        public Task DeleteAsync(T item)
        {
            itemRepository.Delete(item);
            return itemRepository.UnitOfWork.SaveChangesAsync();
        }

        public Task UpdateAsync(T item)
        {
            itemRepository.Update(item);
            return itemRepository.UnitOfWork.SaveChangesAsync();
        }




        public T First(Expression<Func<T, bool>> criteria)
        {
            return itemRepository.First(criteria);
        }

        public Task<T> FirstAsync(Expression<Func<T, bool>> criteria)
        {
            return itemRepository.FirstAsync(criteria);
        }


        public teknoContext db
        {
            get { return itemRepository.UnitOfWork.db; }
        }


    }

}
