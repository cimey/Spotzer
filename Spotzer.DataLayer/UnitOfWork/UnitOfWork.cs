using Spotzer.DataLayer.DatabaseContext;
using Spotzer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Spotzer.DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IDataBaseContext _dbContext;
        //private readonly DataBaseContext _dbContext;
        private TransactionScope _transactionScope;
        private bool _isDisposed;

        private IRepository<Product> _productRepository;
        private IRepository<Order> _orderRepository;
        private IRepository<OrderProducts> _orderProductsRepository;
        //private IRepository<Deneme> _denemeRepository;

        public UnitOfWork(IDataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Product> ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new Repository<Product>(_dbContext)); }
        }

        public IRepository<Order> OrderRepository
        {
            get { return _orderRepository ?? (_orderRepository = new Repository<Order>(_dbContext)); }
        }

        public IRepository<OrderProducts> OrderProductsRepository
        {
            get { return _orderProductsRepository ?? (_orderProductsRepository = new Repository<OrderProducts>(_dbContext)); }
        }


        //public IRepository<Deneme> DenemeRepository
        //{
        //    get { return _denemeRepository ?? (_denemeRepository = new Repository<Deneme>(_dbContext)); }
        //}

        public void Begin()
        {
            try
            {
                _transactionScope = new TransactionScope(
                    TransactionScopeOption.Required,
                    new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted });
            }
            catch
            {
                Dispose();
                throw;
            }
        }
        /// <summary>
        /// Saves changes to the DbContext.
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Calls Save() first then completes the TransactionScope.
        /// </summary>
        public void End()
        {
            try
            {
                this.Save();
                if (_transactionScope != null)
                    _transactionScope.Complete();
            }
            finally
            {
                Dispose();
            }
        }
        /// <summary>
        /// Disposes the TransactionScope if it has been started and then disposes itself.
        /// </summary>
        public void Cancel()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            if (_transactionScope != null)
            {
                _transactionScope.Dispose();
                _transactionScope = null;
            }
            GC.SuppressFinalize(this);
        }
    }
}
