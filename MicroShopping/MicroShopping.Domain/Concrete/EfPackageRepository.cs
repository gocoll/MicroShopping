using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroShopping.Domain.Abstract;

namespace MicroShopping.Domain.Concrete
{
    public class EfPackageRepository : IPackageRepository, IDisposable
    {
        private MicroshoppingEntities db = new MicroshoppingEntities();

        public IQueryable<LancePackage> FindAllPackages()
        {
            return db.LancePackages;
        }

        public LancePackage FindPackageById(int packageId)
        {
            return db.LancePackages.SingleOrDefault(x => x.LancePackageId == packageId);
        }

        public IQueryable<BoughtPackage> FindAllBoughtPackages()
        {
            return db.BoughtPackages;
        }

        public IQueryable<BoughtPackage> FindAllBoughtPackagesForUserById(int id)
        {
            return db.BoughtPackages.Where(x => x.UserId == id);
        }

        public IQueryable<BoughtPackage> FindAllBoughtPackagesForUserByEmail(string email)
        {
            return db.BoughtPackages.Where(x => x.User.Email == email);
        }

        public void CreateNewPackagePurchase(BoughtPackage boughtPackage)
        {
            db.BoughtPackages.AddObject(boughtPackage);
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
