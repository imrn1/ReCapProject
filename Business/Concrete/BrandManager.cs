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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            this._brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
           if( brand.BrandName.Length < 2 )
           {
                return new ErrorResult(Messages.BrandNameInvalid);
           }
                 
           _brandDal.Add(brand);
           return new SuccessResult(Messages.BrandAdded);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Brand>>(Messages.MaintenanceTime); // List<Brand> => default = null
            }
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public void Delete(int id)
        {
            _brandDal.Delete(_brandDal.Get(b => b.id == id));
        }

        public Brand GetById(int id)
        {
            return _brandDal.Get(b => b.id == id);
        }

        public void Update(Brand brand)
        {
            _brandDal.Update(brand);
        }
    }
}
