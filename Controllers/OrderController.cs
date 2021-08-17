

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    using tachy1.BusinessLogicLayer.Services;
    using tachy1.BusinessLogicLayer.Services.Interfaces;
    using tachy1.Models;

    namespace tachy1.Controllers
    {
        public class OrderController : Controller
        {
            private readonly IOrderService _orderService;
            public OrderController(IOrderService orderService)
            {
                _orderService = orderService;
            }
            [HttpGet]
            [Route("api/order")]
            public Task<IEnumerable<Order>> Get()
            {
                return _orderService.GetAllOrders();
            }

            [HttpGet]
            [Route("api/order/getByName/{name}")]
            public async Task<IActionResult> GetByName(string name)
            {
                try
                {
                    var order = await _orderService.GetOrderByName(name);
                    if (order == null)
                    {
                        return Json("No order found!");
                    }
                    return Json(order);
                }
                catch (Exception ex)
                {
                    return Json(ex.ToString());

                }

            }

            [HttpGet]
            [Route("api/order/getById/{id}")]
            public async Task<IActionResult> GetById(string id)
            {
                try
                {
                    var order = await _orderService.GetOrderById(id);
                    if (order == null)
                    {
                        return Json("No order found!");
                    }
                    return Json(order);
                }
                catch (Exception ex)
                {
                    return Json(ex.ToString());

                }

            }

            [HttpPost]
            [Route("api/order")]
            public async Task<IActionResult> Post([FromBody] Order model)
            {
                try
                {
                   
                    model.CreatedOn = DateTime.UtcNow;
                    await _orderService.AddOrder(model);
                    return Ok("Your order has been added successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

            [HttpPut]
            [Route("api/order/updatePrice/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AcceptOrder(string id, [FromBody] Order model)
            {
                
                model.UpdatedOn = DateTime.UtcNow;
                var result = await _orderService.AcceptOrder(id, model);
                if (result)
                {
                    return Ok("Your order's price has been updated successfully");
                }
                return BadRequest("No order found to update");

            }

        [HttpPut]
        [Route("api/order/updatePrice/{id}")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> ConfirmOrder(string orderId,string userId, [FromBody] Order model)
        {

            model.UpdatedOn = DateTime.UtcNow;
            
             var result = await _orderService.ConfirmOrder(orderId,userId, model);
            if (result)
            {
                return Ok("Your order's price has been updated successfully");
            }
            return BadRequest("No order found to update");

        }

        [HttpDelete]
            [Route("api/order/{id}")]
            public async Task<IActionResult> Delete(string id)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(id))
                        return BadRequest("Order name missing");
                    await _orderService.RemoveOrder(id);
                    return Ok("Your order has been deleted successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }
            [HttpDelete]
            [Route("api/order/deleteAll")]
            public IActionResult DeleteAll()
            {
                try
                {
                    _orderService.RemoveAllOrders();
                    return Ok("Your all orders has been deleted");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

        } }