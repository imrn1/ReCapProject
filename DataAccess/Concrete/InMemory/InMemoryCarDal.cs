using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>() { 
                new Car(){id=1,BrandId=1,ColorId=1,DailyPrice=1000,Description="asdf1",ModelYear="2000"},
                new Car(){id=2,BrandId=2,ColorId=2,DailyPrice=5000,Description="asdf2",ModelYear="2003"},
                new Car(){id=3,BrandId=1,ColorId=2,DailyPrice=1500,Description="asdf3",ModelYear="2005"}
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car entity)
        {
            Car DeletedCar = _cars.SingleOrDefault(c => c.id == entity.id);
            _cars.Remove(DeletedCar);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            Console.WriteLine("filter body:"+filter);
            Console.WriteLine("filter body:" + filter.Body);
            // id=3 olan car nesnesini döndürür
            //  _cars.SingleOrDefault(filter) => filter'ı bu şekilde kullanamaz hata verir
            return _cars.SingleOrDefault(c=>c.id==3);

        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            return _cars;
        }

        public void Update(Car car)
        {
            Car updateToCar = _cars.SingleOrDefault(c => c.id == car.id);
           
            if (updateToCar!=null) 
            {
                updateToCar.ColorId = car.ColorId;
                updateToCar.BrandId = car.BrandId;
                updateToCar.DailyPrice = car.DailyPrice;
                updateToCar.Description = car.Description;
                updateToCar.ModelYear = car.ModelYear;
            }
            else
            {
                Console.WriteLine("id'si {0} ile eşleşen araba bulunmuyor!",car.id);
            }
        }

    }
}
