using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager: ICarService
    {
        ICarDal _carService;
        public CarManager(ICarDal carService)
        {
            _carService = carService;
        }

        public void Add(Car car)
        {
            // process...
            _carService.Add(car);
        }

        public void Delete(int id)
        {
            _carService.Delete(id);
        }

        public List<Car> GetAll()
        {
            return _carService.GetAll();
        }

        public Car GetById(int id)
        {
            return _carService.GetById(id);
        }

        public void Update(Car car)
        {
            _carService.Update(car);
        }
    }
}
