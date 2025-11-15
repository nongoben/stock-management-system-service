using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Repositories;

namespace StockManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DropDownController : ControllerBase
{
	private readonly IProductRepository _productRepository;
	private readonly IOrderRepository _orderRepository;

	public DropDownController(IProductRepository productRepository, IOrderRepository orderRepository)
	{
		_productRepository = productRepository;
		_orderRepository = orderRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetDropdownProduct()
	{
		var products = await _productRepository.GetProductCodesAndDescriptionsAsync();
		return Ok(products);
	}

	[HttpGet]
	public async Task<IActionResult> GetSalesPerson()
	{
		var salesPersons = await _orderRepository.GetSalesPersonsAsync();
		return Ok(salesPersons);
	}

	[HttpGet]
	public async Task<IActionResult> GetCustomer()
	{
		var salesPersons = await _orderRepository.GetCustomersAsync();
		return Ok(salesPersons);
	}
}