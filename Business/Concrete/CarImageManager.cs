using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {

        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage)
        {
            carImage.Date = DateTime.UtcNow;
            _carImageDal.Add(carImage);
            return new SuccessResult("resim eklendi");
        }

        public IResult Delete(int id)
        {
            var result = _carImageDal.Get(img => img.Id == id);
            if (result == null)
            {
                return new ErrorResult(Messages.InvalidId);
            }
            _carImageDal.Delete(result);
            return new SuccessResult(Messages.SuccessDelete);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            var data = _carImageDal.GetAll();
            if (data.Count==0)
            {
                return new ErrorDataResult<List<CarImage>>(data, "hiç resim yok");
            }
            return new SuccessDataResult<List<CarImage>>(data);
        }

        public IDataResult<List<CarImage>> GetByCarId(int id)
        {
            var data = _carImageDal.GetAll(img => img.CarId == id);
            if (data == null)
            {
                return new ErrorDataResult<List<CarImage>>(data,"carId ye ait resim bulunmuyor");
            }
            return new SuccessDataResult<List<CarImage>>(data);
        }

        public IResult Update(CarImage carImage)
        {
            carImage.Date = DateTime.UtcNow;
            _carImageDal.Update(carImage);
            return new SuccessResult("resim güncellendi");
        }
    }
}
