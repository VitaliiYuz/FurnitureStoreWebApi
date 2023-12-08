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
    public class CustomersController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(FurnitureStoreContext context, IMapper mapper, ICustomerRepository customerRepository)
        {
            _context = context;
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        // GET: api/Customers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<CustomerDto>))]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();

            var responseList = _mapper.Map<List<CustomerDto>>(customers);
            return Ok(responseList);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCustomerById(int id)
        {
            if (!_customerRepository.CustomerExists(id))
                return NotFound();

            var customer = _customerRepository.GetCustomerById(id);
            var response = _mapper.Map<CustomerDto>(customer);

            return Ok(response);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]

        public IActionResult UpdateCustomer([FromRoute] int id, [FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_customerRepository.CustomerExists(id))
                return NotFound();

            var customerToUpdate = _mapper.Map<Customer>(customerDto);
            customerToUpdate.CustomerId = id;

            if (!_customerRepository.UpdateCustomer(customerToUpdate))
            {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();

        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateCustomer([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(customerDto);

            if (!_customerRepository.CreateCustomer(customer))
            {
                return BadRequest(ModelState);
            }

            var createdCustomerDto = _mapper.Map<CustomerDto>(customer);

            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, createdCustomerDto);
        }


        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteCustomer([FromRoute] int id)
        {
            if (!_customerRepository.CustomerExists(id))
                return NotFound();

            var customerToDelete = _customerRepository.GetCustomerById(id);

            if (!_customerRepository.DeleteCustomer(customerToDelete))
            {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private ActionResult<bool> CustomerExists([FromRoute] int id)
        {
            return _customerRepository.CustomerExists(id);
        }
    }
}
