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
    public class OrderItemsController : ControllerBase
    {
        private readonly FurnitureStoreContext _context;
        private readonly IMapper _mapper;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemsController(FurnitureStoreContext context, IMapper mapper, IOrderItemRepository orderItemRepository)
        {
            _context = context;
            _mapper = mapper;
            _orderItemRepository = orderItemRepository;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IQueryable<OrderItemDto>))]
        public IActionResult GetAllOrderItems()
        {
            var orderItems = _orderItemRepository.GetAllOrderItems();

            var responseList = _mapper.Map<List<OrderItemDto>>(orderItems);
            return Ok(responseList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetOrderItemById(int id)
        {
            if (!_orderItemRepository.OrderItemExists(id))
                return NotFound();

            var orderItem = _orderItemRepository.GetOrderItemById(id);
            var response = _mapper.Map<OrderItemDto>(orderItem);

            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]

        public IActionResult UpdateOrderItem([FromRoute] int id, [FromBody] OrderItemDto orderItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_orderItemRepository.OrderItemExists(id))
                return NotFound();

            var orderItemToUpdate = _mapper.Map<OrderItem>(orderItemDto);
            orderItemToUpdate.OrderItemId = id;

            if (!_orderItemRepository.UpdateOrderItem(orderItemToUpdate))
            {
                throw new DataException("Something went wrong while updating");
            }

            return NoContent();

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderItemDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public IActionResult CreateOrderItem([FromBody] OrderItemDto orderItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderItem = _mapper.Map<OrderItem>(orderItemDto);

            if (!_orderItemRepository.CreateOrderItem(orderItem))
            {
                return BadRequest(ModelState);
            }

            var createdOrderItemDto = _mapper.Map<OrderItemDto>(orderItem);

            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.OrderItemId }, createdOrderItemDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteOrderItem([FromRoute] int id)
        {
            if (!_orderItemRepository.OrderItemExists(id))
                return NotFound();

            var orderItemToDelete = _orderItemRepository.GetOrderItemById(id);

            if (!_orderItemRepository.DeleteOrderItem(orderItemToDelete))
            {
                throw new DataException("Something went wrong while deleting");
            }

            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private ActionResult<bool> OrderItemExists([FromRoute] int id)
        {
            return _orderItemRepository.OrderItemExists(id);
        }
    }
}
