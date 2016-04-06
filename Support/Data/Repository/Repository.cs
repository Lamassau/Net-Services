
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace tekno.Services.Data
{
    public class Repository<T> :  IDisposable, IRepository<T> where T : teknoData 
    {

        private EFDbContext session;

        public IUnitOfWork UnitOfWork
        {

            get
            {
                if (session == null)
                    throw new InvalidOperationException("A session IUnitOfWork do not exist.");
                return (session as IUnitOfWork);
            }
        }



        public Repository()
        {
            SetSession(new EFDbContext());
        }


        public Repository(IUnitOfWork instance)
            : base()
        {
            SetSession(instance);
        }

        public IQueryable<T> AllItems
        {
            get { return session.Set<T>(); }
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> items = AllItems;
            return (items!=null) ? items.Where(criteria) : null;
        }

        public T First(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> items = AllItems;
            return  (items!=null) ?  items.FirstOrDefault(criteria) : null;
        }

        public T Single(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> items = AllItems;
            return (items!=null) ? items.SingleOrDefault(criteria) : null;
        }

        public Task<T> FirstAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> items = AllItems;
            return (items!=null) ? items.FirstOrDefaultAsync(criteria) : Task.FromResult((T) null);
        }

        public Task<T> SingleAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> items = AllItems;
            return (items!=null) ?   items.SingleOrDefaultAsync(criteria) : Task.FromResult((T) null);
        }

        public Task<List<T>> FindAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = Find(criteria);
            return query.ToListAsync<T>();

        }

        public bool Add(T entity)
        {
            if (!IsValid(entity))
                return false;

            entity.InitAdd();
            return true;

            //try
            //{
            //    //session.Set(typeof(T)).Add(entity);
            //    //return session.Entry(entity).GetValidationResult().IsValid;
            //}
            //catch (Exception ex)
            //{
            //    if (ex.InnerException != null)
            //        throw new Exception(ex.InnerException.Message, ex);
            //    throw new Exception(ex.Message, ex);
            //}
        }





        public bool Delete(T entity)
        {
            if (!IsValid(entity))
                return false;

            return true;

            //try
            //{
            //    //session.Set(typeof(T)).Remove(entity);
            //    //return session.Entry(entity).GetValidationResult().IsValid;
            //}
            //catch (Exception ex)
            //{
            //    if (ex.InnerException != null)
            //        throw new Exception(ex.InnerException.Message, ex);
            //    throw new Exception(ex.Message, ex);
            //}
        }

        public bool Update(T entity)
        {
            if (!IsValid(entity))
                return false;

            return true;
            //try
            //{
            //    //var entry = session.Entry<T>(entity);
            //    //if (entry.State == EntityState.Detached)
            //    //{
            //    //    var set = session.Set<T>();
            //    //    T attachedEntity = set.Local.SingleOrDefault<T>(e => e. == entity);  // You need to have access to key



            //    //    session.Set(typeof(T)).Attach(entity);
            //    //}

            //    //session.Entry(entity).State = EntityState.Modified;
            //    //return session.Entry(entity).GetValidationResult().IsValid;
            //}
            //catch (Exception ex)
            //{
            //    if (ex.InnerException != null)
            //        throw new Exception(ex.InnerException.Message, ex);
            //    throw new Exception(ex.Message, ex);
            //}
        }

        public bool IsValid(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("The entity is null.");
            return true;
        }

        public void Dispose()
        {
            session.Dispose();
        }


        public void SetSession(IUnitOfWork session)
        {
            SetUnitOfWork(session);
        }

        protected internal void SetUnitOfWork(IUnitOfWork session)
        {
            if (!(session is EFDbContext))
                throw new ArgumentException("The IUnitOfWork dose not support a DbContext instance.");

            this.session = (EFDbContext)session;
        }

        private class EFDbContext : teknoContext, IUnitOfWork,IDisposable
        {
            private DbContextTransaction _transaction;
            public bool IsInTransaction
            {
                get { return _transaction != null; }
            }

            //public DbSet<User> User { get; set; }

            public EFDbContext()
                : base()
            {
            }


            public override Task<int> SaveChangesAsync()
            {
                try
                {
                    return base.SaveChangesAsync();
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    throw new DbEntityValidationException(
                        "Entity Validation Failed - errors follow:\n" +
                        sb.ToString(), ex
                    ); // Add the original exception as the innerException
                }
            }

            public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
            {
                try
                {
                    return base.SaveChangesAsync(cancellationToken);
                }
                catch (DbEntityValidationException ex)
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var failure in ex.EntityValidationErrors)
                    {
                        sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                        foreach (var error in failure.ValidationErrors)
                        {
                            sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                            sb.AppendLine();
                        }
                    }

                    throw new DbEntityValidationException(
                        "Entity Validation Failed - errors follow:\n" +
                        sb.ToString(), ex
                    ); // Add the original exception as the innerException
                }
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
            }


            public void RollBackTransaction()
            {
                _transaction.Rollback();
            }


            public void BeginTransaction()
            {
                _transaction = base.Database.BeginTransaction();

            }

            public void CommitTransaction()
            {
                if (_transaction == null)
                {
                    throw new ApplicationException("Cannot roll back a transaction while there is no transaction running.");
                }

                try
                {
                    SaveChanges();
                    _transaction.Commit();
                }
                catch (DbEntityValidationException e)
                {
                    string error = string.Empty;
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        error += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw new Exception(error);
                }
                catch
                {
                    _transaction.Rollback();
                    throw;
                }
                finally
                {
                    _transaction.Dispose();
                }
            }



            public new void Dispose()
            {
                base.Dispose();
            }

            public new void SaveChanges()
            {
                base.SaveChanges();
            }

            internal object Set(Type type)
            {
                throw new NotImplementedException();
            }

            internal object Entry(T entity)
            {
                throw new NotImplementedException();
            }

            public teknoContext db
            {
                get { return (teknoContext)this; }
            }
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }


    }

}
