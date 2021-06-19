using Business.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarManager: ICarService
    {
        ICarDal _carDal;
        IBrandService _brandService;
        public CarManager(ICarDal carService, IBrandService brandService)
        {
            _carDal = carService;
            _brandService = brandService;
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>( _carDal.Get(c => c.id == id));
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }
       
        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>( _carDal.GetAll(c => c.ColorId == id));
        }

        public IDataResult<List<Car>> GetByPrice(int min, int max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.DailyPrice >= min && c.DailyPrice <= max));
        }

        public IResult Add(Car car)
        {
            // aynı isimde 2 ürün eklenemez
            // bir brand'de en fazla 5 araba olabilir.
          var result =  BusinessRules.Run(
              CheckCarName(car.CarName),
              CheckCarCountOfBrandCorrect(car.BrandId),
              CheckBrandCount());
                      
            if (result!=null)
            {
                return result;
            }
            _carDal.Add(car);
            return new SuccessResult("ekleme başarılı");

        }

        public IResult Delete(int id)
        {
            // iş kodları...
            _carDal.Delete(_carDal.Get(c => c.id == id)); 
            return new SuccessResult("silme işlemi başarılı");
        }
       
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetails()
        {
            return new SuccessDataResult< List < CarDetailsDto>>(_carDal.GetCarDetails());
        }


        private IResult CheckCarCountOfBrandCorrect(int BrandId)
        {
            int count = _carDal.GetAll(c => c.BrandId == BrandId).Count;
            if (count >= 5)
            {
                return new ErrorResult("aynı markada 5'den fazla araba eklenemez.İşlem başarısız.");
            }
            return new SuccessResult();
        }

        private IResult CheckCarName(string carName)
        {
            
            bool result = _carDal.GetAll(c => c.CarName == carName).Any();
            if (result)
            {
                return new ErrorResult("aynı isimde araba mevcut.İşlem başarısız.");
            }
            return new SuccessResult();
        }

       private IResult CheckBrandCount()
        {
            int BrandCount = _brandService.GetAll().Data.Count;
            Console.WriteLine("toplam brand sayısı= "+BrandCount);
            if(BrandCount >= 15)
            {
                return new ErrorResult("brand sayısı hatası");
            }
            return new SuccessResult();
        }

    }
}
