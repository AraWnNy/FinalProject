using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public IDataResult<List<Category>> GetAll()
        {
            try
            {
                return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<Category>>();
            }
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            try
            {
                return new SuccessDataResult<Category>(_categoryDal.Get(c=>c.CategoryId == categoryId));
            }
            catch (Exception)
            {

                return new ErrorDataResult<Category>();
            }
        }
    }
}
