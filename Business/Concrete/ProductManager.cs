using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //İş Kodları - If yapıları olur genellikle
            //Örneğin yetkisi varsa
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategoryID(int id)
        {
            return _productDal.GetAll(p=>p.CategoryId == id).ToList();
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=>p.UnitPrice >= min && p.UnitPrice <= max);
        }
    }
}