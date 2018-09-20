using Spotzer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.DataLayer.DatabaseContext
{
    public interface IDataBaseContext
    {
        DbSet<Product> ProductSet { get; set; }
        DbSet<Order> OrderSet { get; set; }

        DbSet<T> Set<T>() where T : class;
        void SaveChanges();
    }
}
