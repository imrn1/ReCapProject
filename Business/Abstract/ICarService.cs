using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService
    {
        //  GetById, GetAll, Add, Update, Delete

        IDataResult<List<Car>> GetAll();

        IDataResult<List<Car>> GetCarsByBrandId(int id);
        IDataResult<List<Car>> GetCarsByColorId(int id);

        IDataResult<List<Car>> GetByPrice(int min, int max);

        IDataResult<Car> GetById(int id);

        IDataResult<List<CarDetailsDto>> GetCarDetails();


        IResult Add(Car car);

        IResult Update(Car car);

        IResult Delete(int id);

    }
}
