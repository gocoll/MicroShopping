using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroShopping.Domain.Abstract
{
    public interface IProductCategoryRepository
    {
        IQueryable<ProductCategory> FindAllCategories();
        ProductCategory FindCategoryById(int id);
        void AddCategory(ProductCategory category);
        void SaveChanges();
    }
}
