using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroShopping.Domain.Abstract;

namespace MicroShopping.Domain.Concrete
{
    public class EfGenderRepository : IGenderRepository, IDisposable
    {
        private MicroshoppingEntities db = new MicroshoppingEntities();

        public IQueryable<Gender> FindAllGenders()
        {
            return db.Genders;
        }

        public Gender FindById(int id)
        {
            return db.Genders.SingleOrDefault(x => x.GenderId == id);
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
