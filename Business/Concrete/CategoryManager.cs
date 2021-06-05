using Business.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Entities.Concrete;
using DataAccess.Abstract;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<Category> GetById(int Id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == Id), Messages.ProductListed);

        }
    }
}
