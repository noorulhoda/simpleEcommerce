using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using tachy1.BusinessLogicLayer.Services;
using tachy1.BusinessLogicLayer.Services.Interfaces;
using tachy1.Models;

namespace tachy1.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("api/product")]
        public Task<IEnumerable<Product>> Get()
        {
            return _productService.GetAllProducts();
        }

        [HttpGet]
        [Route("api/product/getByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var product = await _productService.GetProductByName(name);
                if (product == null)
                {
                    return Json("No product found!");
                }
                return Json(product);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());

            }

        }

        [HttpGet]
        [Route("api/product/getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return Json("No product found!");
                }
                return Json(product);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());

            }

        }

        [HttpPost]
        [Route("api/product")]
        public async Task<IActionResult> Post([FromBody]  Product model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                    return BadRequest("Please enter product name");
                else if (string.IsNullOrWhiteSpace(model.Category))
                    return BadRequest("Please enter category");
                else if (model.Price <= 0)
                    return BadRequest("Please enter price");

                model.CreatedOn = DateTime.UtcNow;
                await _productService.AddProduct(model);
                return Ok("Your product has been added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("api/product/updatePrice/{id}")]
        public async Task<IActionResult> Update(string id,[FromBody] Product model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest("Product name missing");
            model.UpdatedOn = DateTime.UtcNow;
            var result = await _productService.UpdatePrice(id,model);
            if (result)
            {
                return Ok("Your product's price has been updated successfully");
            }
            return BadRequest("No product found to update");

        }

        [HttpDelete]
        [Route("api/product/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return BadRequest("Product name missing");
                await _productService.RemoveProduct(id);
                return Ok("Your product has been deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        [HttpDelete]
        [Route("api/product/deleteAll")]
        public IActionResult DeleteAll()
        {
            try
            {
                _productService.RemoveAllProducts();
                return Ok("Your all products has been deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

    }
}
