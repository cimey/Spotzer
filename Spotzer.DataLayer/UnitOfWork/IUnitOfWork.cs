using Spotzer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.DataLayer.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        IRepository<Product> ProductRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderProducts> OrderProductsRepository { get; }
        //IRepository<Deneme> DenemeRepository { get; }

        void Begin();
        void Save();
        void End();
        void Cancel();
    }
}
