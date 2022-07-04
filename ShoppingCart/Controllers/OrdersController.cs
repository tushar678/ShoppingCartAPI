﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        [HttpPost("AddBookToWishlist")]
        public ActionResult AddBookToWishlist([FromBody]AddWishListDto wishlist)
        {
            try
            {
                if (wishlist is null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _ordersRepository.AddToWishList(wishlist);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }

        }

        [HttpGet("GetWishListItem")]
        public ActionResult GetWishListItem( int userId)
        {
            try
            {
                if (userId < 0)
                {
                    return BadRequest();
                }
                
              var wishListItem=  _ordersRepository.GetWishListItemByUserId(userId);
                return Ok(wishListItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error:{ex}");
            }

        }

        [HttpGet("")]
        public async Task<IActionResult> GetOrders()
        {
            var orderdetail = await _ordersRepository.GetOrdersAsync();
            return Ok(orderdetail);
        }

        [HttpGet("GetOrderStatus")]
        public async Task<IActionResult> GetOrderStatus()
        {
            var orders = await _ordersRepository.GetOrdersStatus();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            var orderdetail = await _ordersRepository.GetOrderByIdAsync(id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            return Ok(orderdetail);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddOrder([FromBody] OrderDetailDto orderdto)
        {
            var orderdetail = await _ordersRepository.AddOrderAsync(orderdto);
            return CreatedAtAction(nameof(GetOrderById), new { id = orderdetail, Controller = "OrderDetail" }, orderdto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderDetailDto orderdto, [FromRoute] int id)
        {
            await _ordersRepository.UpdateOrderAsync(id, orderdto);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            await _ordersRepository.DeleteOrderAsync(id);
            return Ok();
        }
    }
}
