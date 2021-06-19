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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
            
        }

        public IResult Delete(int id)
        {
            _colorDal.Delete(_colorDal.Get(c=>c.id == id));
            return new SuccessResult(Messages.SuccessDelete);
        }

        public IDataResult<List<Color>> GetAll()
        {
            // .....
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll()) ;
        }

        public IDataResult<Color> GetByColorName(string ColorName)
        {
            // ....
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorName == ColorName));
        }

        public IDataResult<Color> GetById(int id)
        {
            // ......
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.id == id));
        }

        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult();
        }
    }
}
