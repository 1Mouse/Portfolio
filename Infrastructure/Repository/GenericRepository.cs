using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Context _context;
        /*
         * If it's private and readonly, the benefit is that you can't inadvertently change 
         * it from another part of that class after it is initialized. The readonly modifier
         * ensures the field can only be given a value during its initialization or in its
         * class constructor. If something functionally should not change after initialization,
         * it's always good practice to use available language constructs to enforce that.
         */
        private DbSet<T> table = null;
        public GenericRepository(Context context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }
        public void Delete(object id)
        {
            T existing = GetById(id);
            table.Remove(existing);
        }
        public void Update(T entity)
        {
            table.Attach(entity); // to find an entity and edit it
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
