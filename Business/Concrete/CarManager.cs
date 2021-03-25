using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager: ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carService)
        {
            _carDal = carService;
        }

     

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public Car GetById(int id)
        {
            return _carDal.Get(c => c.id == id);
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetAll(c => c.BrandId == id);
        }
        public List<Car> GetCarsByColorId(int id)
        {
            return _carDal.GetAll(c => c.ColorId == id);
        }

        public List<Car> GetByPrice(int min, int max)
        {
            return _carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max);
        }

        public void Add(Car car)
        {
            // iş kodları...
            //Araba ismi minimum 2 karakter olmalıdır
            //Araba günlük fiyatı 0'dan büyük olmalıdır.
            if(car.CarName.Length>=2 && car.DailyPrice > 0)
            {
                _carDal.Add(car);
            }
            else
            {
                Console.WriteLine("araba eklenemedi!\n " +
                    "Araba ismi minimum 2 karakter olmalıdır ve günlük fiyatı 0'dan büyük olmalıdır.");
            }
            
        }

        public void Delete(int id)
        {
            // iş kodları...

            //Car DeletedCar = GetById(id);    // 1.yol
            //_carDal.Delete(DeletedCar);        

            _carDal.Delete(_carDal.Get(c => c.id == id));  // 2.yol
        }
        public void Update(Car car)
        {
            _carDal.Update(car);
        }

        public List<CarDetailsDto> GetCarDetails()
        {
           return  _carDal.GetCarDetails();
        }
    }
}
