using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.Utilities.Exceptions;
using eShopSolution.ViewModels.Catalog.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    //api/products
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProdcutService _publicManageService;
        public ProductsController(IPublicProductService publicProductService, IManageProdcutService publicManageService)
        {
            _publicProductService = publicProductService;
            _publicManageService = publicManageService;
        }

        //http://localhost:port/product?pageIndex=1&pageSize=10&CategoryId=
        [HttpGet("{languaegId}")]
        public async Task<IActionResult> Get(string languaegId,[FromQuery]GetPublicProductpagingRequest requets)
        {
            var products = await _publicProductService.GetAllByCategoryId(languaegId,requets);
            //trả về 200
            return Ok(products);
        }

        //http://localhost:port/product/GetById/1/vi-VN
        [HttpGet("GetById/{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId,string languageId)
        {
           var products = await _publicManageService.GetById(productId, languageId);
            if (products == null)
                //trả về 404
                return BadRequest("Cannot find Product");
            return Ok(products);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest requets)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _publicManageService.Create(requets);
            if (productId == 0)
                //trả về 404
                return BadRequest();

            var product = await _publicManageService.GetById(productId,requets.LanguageId);
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

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var afectedResult = await _publicManageService.Delete(productId);
            if (afectedResult == 0)
                //trả về 404
                return BadRequest();


            // return Created(nameof(GetById), product);
            return Ok();
        }

        

        //Update one part
        [HttpPatch("price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var iSuccessFull = await _publicManageService.UpdatePrice(productId, newPrice);
            if (iSuccessFull == false)
                //trả về 404
                return BadRequest();


            // return Created(nameof(GetById), product);
            return Ok();
        }

        [HttpPost("{productId}/image")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _publicManageService.AddImage(productId, request);
            if (productId == 0)
                return BadRequest();
            var image = await _publicManageService.GetByImageId(imageId);
            return CreatedAtAction(nameof(GetByImageId), new { id = imageId }, image);

        }
        [HttpPut("{productId}/image/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _publicManageService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();
            // var image = await _publicManageService.GetByImageId(imageId);
            return Ok();

        }
        [HttpDelete("{ProductId}/image/{ImageId}")]
          public async Task<IActionResult> RemoveImage(int imageid)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            var image =await _publicManageService.DeleteImage(imageid);
            if (imageid == 0) 
                return BadRequest();
            return Ok();
        }

        [HttpGet("{productId}/image/{ImageId}")]
        public async Task<IActionResult> GetByImageId (int productId, int ImageId)
        {
            var image = await _publicManageService.GetByImageId(ImageId);
            if (image == null)
                return BadRequest("Cannot find product");
            return Ok(image);
        }

    }
}