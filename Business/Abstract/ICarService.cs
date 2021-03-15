using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        //  GetById, GetAll, Add, Update, Delete

        Car GetById(int id);

        List<Car> GetAll();

        void Add(Car car);

        void Update(Car car);

        void Delete(int id);

    }
}
