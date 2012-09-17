using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroShopping.Domain.Abstract
{
    public interface IGenderRepository
    {
        IQueryable<Gender> FindAllGenders();
        Gender FindById(int id);
        void SaveChanges();
    }
}
