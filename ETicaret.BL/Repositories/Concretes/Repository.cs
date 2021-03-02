using ETicaret.BL.Repositories.Abstracts;
using ETicaret.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ETicaret.BL.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ETicaretContext _context;
        public Repository()
        {
            _context = ProviderService.GetContext();
        }
        public DbSet<T> GetTable()
        {
            return _context.Set<T>();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
        public List<T> Get()
        {
            return GetTable().ToList();
        }
        public T GetById(int id)
        {
            return null;
        }
        public List<T> GetWhere(Expression<Func<T, bool>> metot)
        {
            return GetTable().Where(metot).ToList();
        }
        public T GetSingle(Expression<Func<T, bool>> metot)
        {
            return GetTable().FirstOrDefault(metot);
        }
        public bool Remove(T model)
        {
            try
            {
                GetTable().Remove(model);
                return Save() > 0 ? true : false;

            }
            catch 
            {
                return false;
            }
        }
        public bool Update(T model)
        {
            _context.Entry<T>(model).State = EntityState.Modified; 
            return Save() > 0 ? true : false;
        }


        public T Add(T model)
        {
            GetTable().Add(model);
            Save();
            return model;
        }

        public T GetLast<Key>(Expression<Func<T, Key>> metot)
        {
            return GetTable().OrderByDescending(metot).Take(1).FirstOrDefault();
        }
    }
}
