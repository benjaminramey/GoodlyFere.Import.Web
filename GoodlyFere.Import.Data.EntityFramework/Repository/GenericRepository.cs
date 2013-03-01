#region Usings

using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Repository
{
    public class GenericRepository<TInterface, TConcrete> : IWriteRepository<TInterface>
        where TConcrete : class, TInterface, new()
        where TInterface : IModelBase
    {
        #region Public Methods

        public virtual void Delete(TInterface objectToDelete)
        {
            DeleteMany(new[] { objectToDelete });
        }

        public virtual void DeleteMany(TInterface[] objectsToDelete)
        {
            using (var context = new ImportEntities())
            {
                var entities = context.Set<TConcrete>();
                foreach (var entity in objectsToDelete.Cast<TConcrete>())
                {
                    entities.Attach(entity);
                    entities.Remove(entity);
                }
                context.SaveChanges();
            }
        }

        public virtual TInterface[] Get(Expression<Func<TInterface, bool>> filter)
        {
            var concreteFilter = RepositoryHelper.ConvertToConcreteExpression<TConcrete, TInterface>(filter);
            using (var context = new ImportEntities())
            {
                var entities = GetQueryable(context);
                entities.Where(concreteFilter).Load();
                return context.Set<TConcrete>().Local.Cast<TInterface>().ToArray();
            }
        }

        public virtual TInterface Get(object key)
        {
            int id = (int)key;

            using (var context = new ImportEntities())
            {
                var entities = GetQueryable(context);
                return entities.SingleOrDefault(p => p.Id == id);
            }
        }

        public virtual TInterface[] GetAll()
        {
            using (var context = new ImportEntities())
            {
                var entities = GetQueryable(context);
                entities.Load();
                return context.Set<TConcrete>().Local.Cast<TInterface>().ToArray();
            }
        }

        public virtual TInterface Save(TInterface newObject)
        {
            return SaveMany(new[] { newObject }).FirstOrDefault();
        }

        public virtual TInterface[] SaveMany(TInterface[] newObjects)
        {
            using (var context = new ImportEntities())
            {
                var entities = context.Set<TConcrete>();
                foreach (var entity in newObjects.Cast<TConcrete>())
                {
                    entities.Add(entity);
                }
                context.SaveChanges();
                return newObjects;
            }
        }

        public virtual TInterface Update(TInterface updatedObject)
        {
            return UpdateMany(new[] { updatedObject }).FirstOrDefault();
        }

        public virtual TInterface[] UpdateMany(TInterface[] updatedObjects)
        {
            using (var context = new ImportEntities())
            {
                DbSet<TConcrete> set = context.Set<TConcrete>();
                foreach (var entity in updatedObjects.Cast<TConcrete>())
                {
                    var attachedEntity = set.Find(entity.Id);
                    if (attachedEntity != null)
                    {
                        var attachedEntry = context.Entry(attachedEntity);
                        attachedEntry.CurrentValues.SetValues(entity);
                    }
                    else
                    {
                        set.Attach(entity);
                        var entry = context.Entry(entity);
                        entry.State = EntityState.Modified;
                    }
                }

                context.SaveChanges();

                int[] ids = updatedObjects.Select(ent => ent.Id).ToArray();
                return GetQueryable(context).Where(ent => ids.Contains(ent.Id)).ToArray();
            }

        }

        #endregion

        #region Methods

        protected virtual IQueryable<TConcrete> GetQueryable(ImportEntities context)
        {
            return context.Set<TConcrete>();
        }

        #endregion
    }
}