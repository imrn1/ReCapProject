
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : ICarDal
    {
        public Car Get(Expression<Func<Car, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Car>().SingleOrDefault(filter);
            }
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null
                    ? context.Set<Car>().ToList()
                    : context.Set<Car>().Where(filter).ToList();
            }
        }

        public void Update(Car entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var UpdatedEntitiy = context.Entry(entity);
                UpdatedEntitiy.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Add(Car entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var AddedEntitiy = context.Entry(entity);
                AddedEntitiy.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Car entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var DeletedEntitiy = context.Entry(entity);
                DeletedEntitiy.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }

}
