using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroShopping.Domain.Abstract
{
    public interface IPackageRepository
    {
        IQueryable<LancePackage> FindAllPackages();
        LancePackage FindPackageById(int packageId);

        IQueryable<BoughtPackage> FindAllBoughtPackages();
        IQueryable<BoughtPackage> FindAllBoughtPackagesForUserById(int id);
        IQueryable<BoughtPackage> FindAllBoughtPackagesForUserByEmail(string email);

        void CreateNewPackagePurchase(BoughtPackage boughtPackage);

        void SaveChanges();
    }
}
