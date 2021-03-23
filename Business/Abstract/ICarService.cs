using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        //  GetById, GetAll, Add, Update, Delete

        List<Car> GetAll();

        List<Car> GetCarsByBrandId(int id);
        List<Car> GetCarsByColorId(int id);

        List<Car> GetByPrice(int min, int max);

        Car GetById(int id);




        void Add(Car car);

        void Update(Car car);

        void Delete(int id);

    }
}
