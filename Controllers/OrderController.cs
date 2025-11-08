using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using StockManagementSystem.Repositories;
using StockManagementSystem.Models;

namespace StockManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderController : ControllerBase
{
	private readonly IOrderRepository _orderRepository;

	public OrderController(IOrderRepository orderRepository)
	{
		_orderRepository = orderRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllOrders()
	{
		var orders = await _orderRepository.GetAllOrdersAsync();
		return Ok(orders);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetOrderById(int id)
	{
		var order = await _orderRepository.GetOrderByIdAsync(id);
		if (order == null)
		{
			return NotFound();
		}
		return Ok(order);
	}

	[HttpPost]
	public async Task<IActionResult> CreateOrder([FromBody] Order order)
	{
		if (order == null)
		{
			return BadRequest();
		}

		var createdOrder = await _orderRepository.AddOrderAsync(order);
		return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Data ? order.Id : 0 }, createdOrder);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
	{
		if (id != order.Id)
		{
			return BadRequest();
		}

		var updatedOrder = await _orderRepository.UpdateOrderAsync(order);
		if (updatedOrder.Success == false)
		{
			return NotFound();
		}

		return Ok(updatedOrder);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		var deleted = await _orderRepository.DeleteOrderAsync(id);

		if (!deleted.Success)
		{
			return NotFound();
		}

		return NoContent();
	}

}