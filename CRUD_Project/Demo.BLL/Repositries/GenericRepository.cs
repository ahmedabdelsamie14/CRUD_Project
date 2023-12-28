using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositries
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly AppDbContext _dbcontext;
        public GenericRepository(AppDbContext dbcontext)
        {

            _dbcontext = dbcontext;

        }
        public void Add(T entity)
        {
            //_dbcontext.Set<T>().Add(entity);
            _dbcontext.Add(entity); //EF Core 3.1 Feature
         
        }

        public void  Update(T entity)
        {
            _dbcontext.Update(entity);
            //return _dbcontext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbcontext.Remove(entity);
            //return _dbcontext.SaveChanges();
        }

        public T Get(int id)
        {
            return _dbcontext.Find<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                   return  (IEnumerable<T>) _dbcontext.Employees.Include(E => E.Department).AsNoTracking().ToList();
            
           else
                   
                   return  _dbcontext.Set<T>().AsNoTracking().ToList();

        }


    }

}

