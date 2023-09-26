using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System.Collections.Generic;

namespace BulkyBookWeb.BussinessLayer
{
    
    public class ProductBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductBLL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Product> GetProducts(string sortOrder,
         string searchString,
         string category)
        {
            var products = from product in _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType")
                           select product;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Title.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category.Name == category);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Title);
                    break;
                case "price":
                    products = products.OrderBy(s => s.Price100);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price100);
                    break;
                default:
                    products = products.OrderBy(s => s.Title);
                    break;
            }
            return products;

        }
    }
}
