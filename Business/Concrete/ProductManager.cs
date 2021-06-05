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
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _iProdcutDal;
        ICategoryService _categoryService;
        int _limit = 15;

        public ProductManager(IProductDal iProdcutDal, ICategoryService categoryService)
        {
            _iProdcutDal = iProdcutDal;
            _categoryService = categoryService;
        }

        public IDataResult<List<Product>> GetAll()
        {
            // İş Kodları (Business Code)
            //Yetkisi var mı? (Validation)
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_iProdcutDal.GetAll(), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int Id)
        {
            return new SuccessDataResult<List<Product>>(_iProdcutDal.GetAll(p => p.CategoryId == Id), Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_iProdcutDal.GetAll(p => p.UnitPrice >= min & p.UnitPrice <= max), Messages.ProductListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_iProdcutDal.GetProductDetails(), Messages.ProductListed);
        }

        [SecuredOperation("product.add, admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {

           IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _iProdcutDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_iProdcutDal.Get(p => p.ProductId == productId), Messages.ProductListed);
        }

        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                _iProdcutDal.Update(product);
                return new SuccessResult(Messages.ProductUpdated);
            }
            return new ErrorResult();
        }


        // Bir kategoride maks 15 ürün olabilir.
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _iProdcutDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= _limit)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExist(string productName)
        {
            var result = _iProdcutDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();

        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if(result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
