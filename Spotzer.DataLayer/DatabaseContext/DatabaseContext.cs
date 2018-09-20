using Spotzer.DataLayer.DatabaseContext;
using Spotzer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.DataLayer
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DbSet<Product> ProductSet { get; set; }
        public DbSet<Order> OrderSet { get; set; }

        //public DbSet<OrderProducts> OrderProductsSet { get; set; }
        //public DbSet<Deneme> DenemeSet { get; set; }

        public DataBaseContext() : base()
        {

        }

        public void SaveChanges()
        {
            base.SaveChanges();
        }

        public override DbSet Set(Type entityType)
        {
            return base.Set(entityType);
        }
    }
}
