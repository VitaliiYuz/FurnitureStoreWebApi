﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FurnitureStoreWebApi.Models;
using FurnitureStoreWebApi.Interfaces;
using FurnitureStoreWebApi.Dto;
using AutoMapper;
using System.Data;
using System.Numerics;

namespace FurnitureStoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductsController(FurnitureStoreContext context, IMapper mapper, IProductRepository productRepository)
        {
            _context = context;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<ProductDto>))]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();

            var responseList = _mapper.Map<List<ProductDto>>(products);
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProductById(int id)
        {
            if (!_productRepository.ProductExists(id))
                return NotFound();

            var product = _productRepository.GetProductById(id);
            var response = _mapper.Map<ProductDto>(product);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]

        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_productRepository.ProductExists(id))
                return NotFound();

            var productToUpdate = _mapper.Map<Product>(productDto);
            productToUpdate.ProductId = id;

            if (!_productRepository.UpdateProduct(productToUpdate))
            {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<Product>(productDto);

            if (!_productRepository.CreateProduct(product))
            {
                return BadRequest(ModelState);
            }

            var createdProductDto = _mapper.Map<ProductDto>(product); 

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, createdProductDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            if (!_productRepository.ProductExists(id))
                return NotFound();

            var productToDelete = _productRepository.GetProductById(id);

            if (!_productRepository.DeleteProduct(productToDelete))
            {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private ActionResult<bool> ProductExists([FromRoute] int id)
        {
            return _productRepository.ProductExists(id);
        }
    }
}
