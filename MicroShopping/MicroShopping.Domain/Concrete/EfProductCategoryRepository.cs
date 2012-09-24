using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroShopping.Domain.Abstract;

namespace MicroShopping.Domain.Concrete
{
    public class EfProductCategoryRepository : IProductCategoryRepository, IDisposable
    {
        private readonly MicroshoppingEntities db = new MicroshoppingEntities();

        public IQueryable<ProductCategory> FindAllCategories()
        {
            return db.ProductCategories;
        }

        public ProductCategory FindCategoryById(int id)
        {
            return db.ProductCategories.SingleOrDefault(x => x.ProductCategoryId == id);
        }

        public void AddCategory(ProductCategory category)
        {
            db.AddToProductCategories(category);
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
