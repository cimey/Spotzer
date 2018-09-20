using Spotzer.Model.Inputs;
using Spotzer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Spotzer.API.Controllers
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        } 

        [Route("InsertOrder")]
        [HttpPost]
        public IHttpActionResult InsertOrder([FromBody]InsertOrderInput input)
        {
            _orderService.InsertOrder(input);
            return Ok();
        }


        [Route("getOrders")]
        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            return Ok(_orderService.GetOrders());
        }
    }
}
