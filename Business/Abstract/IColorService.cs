using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {

        IDataResult<List<Color>> GetAll();
        IDataResult<Color> GetById(int id);
        IDataResult<Color> GetByColorName(string ColorName);
        IResult Add(Color color);
        IResult Update(Color color);
        IResult Delete(int id);

    }
}
