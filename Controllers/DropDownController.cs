using Microsoft.AspNetCore.Mvc;
using StockManagementSystem.Repositories;

namespace training_api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DropDownController : ControllerBase
{
	private readonly IProductRepository _productRepository;

	public DropDownController(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetDropdownProducts()
	{
		var products = await _productRepository.GetProductCodesAndDescriptionsAsync();
		return Ok(products);
	}
}