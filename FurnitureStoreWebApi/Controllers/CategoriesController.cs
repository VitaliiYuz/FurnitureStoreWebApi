using System;
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
    public class CategoriesController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(FurnitureStoreContext context, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _context = context;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<CategoryDto>))]
        public IActionResult GetAllCategories()
        {
            var category = _categoryRepository.GetAllCategories();

            var responseList = _mapper.Map<List<CategoryDto>>(category);
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategoryById(int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var category = _categoryRepository.GetCategoryById(id);
            var response = _mapper.Map<CategoryDto>(category);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]

        public IActionResult UpdateCategory([FromRoute] int id, [FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var categoryToUpdate = _mapper.Map<Category>(categoryDto);
            categoryToUpdate.CategoryId = id;

            if (!_categoryRepository.UpdateCategory(categoryToUpdate))
            {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<Category>(categoryDto);

            if (!_categoryRepository.CreateCategory(category))
            {
                return BadRequest(ModelState);
            }

            var createdCategoryDto = _mapper.Map<CategoryDto>(category);

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, createdCategoryDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteCategory([FromRoute] int id)
        {
            if (!_categoryRepository.CategoryExists(id))
                return NotFound();

            var categoryToDelete = _categoryRepository.GetCategoryById(id);

            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private ActionResult<bool> CategoryExists([FromRoute] int id)
        {
            return _categoryRepository.CategoryExists(id);
        }
    }
}

