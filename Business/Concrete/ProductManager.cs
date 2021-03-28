using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Core.Utilities.Results;
using Business.Constants;
using FluentValidation;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Aspects.Autofac.Validation;
using Business.CCS;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            //Bir entitymanager kendi data access layer harici başka bir entitinin data accessini layerini injection edemez.
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //                   Claim   Claim
        //[SecuredOperation("admin,editor")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //validation(doğrulama)
            IResult result = BusinessRules.Run(
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),
                CheckIfCategoryLimitExceeded());

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAddedMessage);

        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş Kodları - If yapıları olur genellikle
            //Örneğin yetkisi varsa
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryID(int id)
        {
            try
            {
                return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id).ToList(), Messages.ProductGettedByCategory);
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id).ToList(), Messages.ProductCantGettedByCategory);
            }
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductsDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductsDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {

            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result <= 9)
            {

                return new ErrorResult();

            }
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductAddedMessage);

        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result <= 9)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameExists);
            }
            return new SuccessResult();
        }
        
        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll().Data.Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.CategoryLimitError);
            }
            return new SuccessResult();
        }
    }
}