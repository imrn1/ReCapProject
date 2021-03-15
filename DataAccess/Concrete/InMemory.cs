using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete
{
    public class InMemory : ICarDal
    {
        List<Car> _cars;

        public InMemory()
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

        public void Delete(int id)
        {
            Car deleteToCar = _cars.SingleOrDefault(c => c.id == id);
            _cars.Remove(deleteToCar);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c => c.id == id);
        }


    }
}
