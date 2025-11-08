using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using StockManagementSystem.Repositories;
using StockManagementSystem.Models;

namespace training_api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : ControllerBase
{
	private readonly IProductRepository _productRepository;

	public ProductController(IProductRepository productRepository)
	{
		_productRepository = productRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllProducts()
	{
		var products = await _productRepository.GetAllProductsAsync();
		return Ok(products);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetProductById(int id)
	{
		var product = await _productRepository.GetProductByIdAsync(id);
		if (product == null)
		{
			return NotFound();
		}
		return Ok(product);
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] Product product)
	{
		if (product == null)
		{
			return BadRequest();
		}

		var createdProduct = await _productRepository.AddProductAsync(product);
		return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Data ? product.Id : 0 }, createdProduct);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
	{
		if (id != product.Id)
		{
			return BadRequest();
		}

		var updatedProduct = await _productRepository.UpdateProductAsync(product);
		if (updatedProduct.Success == false)
		{
			return NotFound();
		}

		return Ok(updatedProduct);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteProduct(int id)
	{
		var deleted = await _productRepository.DeleteProductAsync(id);

		if (!deleted.Success)
		{
			return NotFound();
		}

		return NoContent();
	}

}