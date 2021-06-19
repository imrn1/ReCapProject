using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

       [SecuredOperation("admin,brand.add")]
       [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
            //ValidationTool.Validate(new BrandValidator(), brand);
            // ilgili brand de 2 araba varsa daha başkasını ekleme
            

            var result = BusinessRules.Run(CheckBrandName(brand.BrandName), 
                CheckBrandNameIsValid(brand.BrandName));
            if(result!= null)
            {
                return result;
            }

           _brandDal.Add(brand);
           return new SuccessResult(Messages.BrandAdded);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<Brand>>(Messages.MaintenanceTime); // List<Brand> => default = null
            }
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll());
        }

        public IResult Delete(int id)
        {
            if (_brandDal.Get(b => b.id == id) == null)
            {
                return new ErrorResult(Messages.InvalidId);
            }
              
            _brandDal.Delete(_brandDal.Get(b => b.id == id));
            return new SuccessResult(Messages.SuccessDelete);

        }

        public IDataResult<Brand> GetById(int id)
        {
            if( _brandDal.Get(b => b.id == id) == null )
            {
                return new ErrorDataResult<Brand>(Messages.InvalidId);
            }
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.id == id)); 

        }

        public IResult Update(Brand brand)
        {
            if (_brandDal.Get(b => b.id == brand.id) == null)
            {
                return new ErrorResult(Messages.InvalidId);
            }
            
            _brandDal.Update(brand);
            return new SuccessResult(Messages.SuccessUpdate);
        }

        public IDataResult<Brand> GetByBrandName(string name)
        {
            if (_brandDal.Get(b => b.BrandName == name) == null)
            {
                return new ErrorDataResult<Brand>(Messages.InvalidName);
            }
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandName == name));
        }



        private IResult CheckBrandNameIsValid(string name)
        {
            if(name.Length<2 || name.Length == 0)
            {
                return new ErrorResult("brand ismi en az 2 karakter içermelidir.");
            }
            return new SuccessResult();
        }
         
        private IResult CheckBrandName(string name)  // aynı isimli brand eklenemez. 
        {
            var result = _brandDal.GetAll().Where(b => b.BrandName == name).Any();
            if (result)  // true ise aynı isimde brand var
            {
                return new ErrorResult("brand birden fazla eklenemez");
            }
            return new SuccessResult();
        }

        
    }
}
