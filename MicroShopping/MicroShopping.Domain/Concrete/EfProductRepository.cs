using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroShopping.Domain.Abstract;

namespace MicroShopping.Domain.Concrete
{
    public class EfProductRepository : IProductRepository, IDisposable
    {
        private readonly MicroshoppingEntities db = new MicroshoppingEntities();

        public IQueryable<Product> FindAllProducts()
        {
            return db.Products;
        }

        public Product FindProductById(int id)
        {
            return db.Products.SingleOrDefault(x => x.ProductId == id);
        }

        public void AddProduct(Product product)
        {
            db.AddToProducts(product);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
