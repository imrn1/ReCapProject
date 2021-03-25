using Business.Abstract;
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

        public void Add(Color color)
        {
            _colorDal.Add(color);
        }

        public void Delete(int id)
        {
            _colorDal.Delete(_colorDal.Get(c=>c.id == id));
        }

        public List<Color> GetAll()
        {
            return _colorDal.GetAll();
        }

        public Color GetByColorName(string ColorName)
        {
            return _colorDal.Get(c => c.ColorName == ColorName);
        }

        public Color GetById(int id)
        {
            return _colorDal.Get(c => c.id == id);
        }

        public void Update(Color color)
        {
            _colorDal.Update(color);
        }
    }
}
