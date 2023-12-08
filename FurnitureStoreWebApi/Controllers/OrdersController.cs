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
    public class OrdersController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(FurnitureStoreContext context, IMapper mapper, IOrderRepository orderRepository)
        {
            _context = context;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<OrderDto>))]
        public IActionResult GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();

            var responseList = _mapper.Map<List<OrderDto>>(orders);
            return Ok(responseList);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrderById(int id)
        {
            if (!_orderRepository.OrderExists(id))
                return NotFound();
            
            var order = _orderRepository.GetOrderById(id);
            var response = _mapper.Map<OrderDto>(order);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]

        public IActionResult UpdateOrder([FromRoute] int id, [FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderRepository.OrderExists(id))
                return NotFound();

            var orderToUpdate = _mapper.Map<Order>(orderDto);
            orderToUpdate.OrderId = id;

            if (!_orderRepository.UpdateOrder(orderToUpdate))
            {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(orderDto);

            if (!_orderRepository.CreateOrder(order))
            {
                
                return BadRequest(ModelState);
            }

            var createdOrderDto = _mapper.Map<OrderDto>(order); 

            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, createdOrderDto);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteOrder([FromRoute] int id)
        {
            if (!_orderRepository.OrderExists(id))
                return NotFound();

            var orderToDelete = _orderRepository.GetOrderById(id);

            if (!_orderRepository.DeleteOrder(orderToDelete))
            {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private ActionResult<bool> OrderExists([FromRoute] int id)
        {
            return _orderRepository.OrderExists(id);
        }
    }
}
