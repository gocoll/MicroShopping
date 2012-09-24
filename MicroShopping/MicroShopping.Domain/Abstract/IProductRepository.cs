using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroShopping.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> FindAllProducts();
        Product FindProductById(int id);
        void AddProduct(Product product);
        void SaveChanges();
    }
}
