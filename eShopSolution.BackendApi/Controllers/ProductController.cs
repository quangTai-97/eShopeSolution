using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModels.Catalog.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProdcutService _publicManageService;
        public ProductController(IPublicProductService publicProductService, IManageProdcutService publicManageService)
        {
            _publicProductService = publicProductService;
            _publicManageService = publicManageService;
        }
        //http://localhost:port/product
        [HttpGet("{languaegId}")]
        public async Task<IActionResult> Get(string languaegId)
        {
             var products =await _publicProductService.GetAll(languaegId);
            return Ok(products);
        }

        //http://localhost:port/product/public-paging
        [HttpGet("{public-paging}/{languaegId}")]
        public async Task<IActionResult> Get([FromQuery]GetPublicProductpagingRequest requets)
        {
            var products = await _publicProductService.GetAllByCategoryId(requets);
            //trả về 200
            return Ok(products);
        }

        //http://localhost:port/product/GetById/1/vi-VN
        [HttpGet("GetById/{id}/{languageId}")]
        public async Task<IActionResult> GetById(int id,string languageId)
        {
           var products = await _publicManageService.GetById(id, languageId);
            if (products == null)
                //trả về 404
                return BadRequest("Cannot find Product");
            return Ok(products);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest requets)
        {
            var productId = await _publicManageService.Create(requets);
            if (productId == 0)
                //trả về 404
                return BadRequest();

            var product = await _publicManageService.GetById(productId,requets.LanguageId);
            // return Created(nameof(GetById), product);
            return CreatedAtAction(nameof(GetById), new { id = productId},product);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm]ProductUpdateRequest requets)
        {
            var afectedResult = await _publicManageService.Update(requets);
            if (afectedResult == 0)
                //trả về 404
                return BadRequest();


            // return Created(nameof(GetById), product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var afectedResult = await _publicManageService.Delete(id);
            if (afectedResult == 0)
                //trả về 404
                return BadRequest();


            // return Created(nameof(GetById), product);
            return Ok();
        }

        [HttpPut("price/{id}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int id,decimal newPrice)
        {
            var iSuccessFull = await _publicManageService.UpdatePrice(id,newPrice);
            if (iSuccessFull == false)
                //trả về 404
                return BadRequest();


            // return Created(nameof(GetById), product);
            return Ok();
        }

    }
}