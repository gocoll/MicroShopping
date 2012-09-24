using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroShopping.Domain.Abstract
{
    public interface IProductBrandRepository
    {
        IQueryable<ProductBrand> FindAllBrands();
        ProductBrand FindBrandById(int id);
        void AddBrand(ProductBrand brand);
        void SaveChanges();
    }
}
