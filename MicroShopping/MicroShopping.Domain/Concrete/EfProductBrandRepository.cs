using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroShopping.Domain.Abstract;

namespace MicroShopping.Domain.Concrete
{
    public class EfProductBrandRepository : IProductBrandRepository, IDisposable
    {
        private readonly MicroshoppingEntities db = new MicroshoppingEntities();

        public IQueryable<ProductBrand> FindAllBrands()
        {
            return db.ProductBrands;
        }

        public ProductBrand FindBrandById(int id)
        {
            return db.ProductBrands.SingleOrDefault(x => x.ProductBrandId == id);
        }

        public void AddBrand(ProductBrand brand)
        {
            db.AddToProductBrands(brand);
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
