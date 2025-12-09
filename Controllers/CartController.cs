namespace proba1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proba1.Data;
using proba1.DTOs;

[Route("api/[controller]")]
[ApiController]
    public class CartController: ControllerBase
    {
    private readonly AppDbContext _context;
    public CartController(AppDbContext context)
    {
        _context = context;
    }
    //GET: api/cart
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems()
    {
        var result = await _context.CartItems
            .Include(ci => ci.Customer)
            .Include (ci => ci.Product)
            .Select(ci => new CartItemDto
            {
                CartItemId = ci.Id,
                CustomerName = ci.Customer.Name, 
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity,
            })
            .ToListAsync();
        return Ok(result);
    }
    }

