using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroShopping.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> FindAllProducts();
        IQueryable<ProductPicture> FindAllPhotosForProduct(int id);
        Product FindProductById(int id);
        void AddProductPicture(ProductPicture picture);
        void AddProduct(Product product);
        void SaveChanges();
    }
}
