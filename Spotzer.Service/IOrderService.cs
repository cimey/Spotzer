using Spotzer.Model.Entities;
using Spotzer.Model.Inputs;
using Spotzer.Model.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotzer.Service
{
    public interface IOrderService
    {
        ICollection<OrderOutput> GetOrders();
        ICollection<Product> GetProducts();
        //ICollection<OrderOutput> GetOrders(GetOrderInput input);

        void InsertOrder(InsertOrderInput input);
    }
}
