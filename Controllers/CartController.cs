using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proba1.Data;
using proba1.DTOs;
using proba1.Models;

namespace proba1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetCartItems()
        {
            var result = await _context.CartItems
                .Include(ci => ci.Customer)
                .Include(ci => ci.Product)
                .Select(ci => new CartItemDto
                {
                    CartItemId = ci.Id,
                    CustomerId = ci.CustomerId,
                    CustomerName = ci.Customer.Name,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    Quantity = ci.Quantity
                })
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/cart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItemDto>> GetCartItem(int id)
        {
            var ci = await _context.CartItems
                .Include(c => c.Customer)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (ci == null)
                return NotFound();

            var dto = new CartItemDto
            {
                CartItemId = ci.Id,
                CustomerId = ci.CustomerId,
                CustomerName = ci.Customer.Name,
                ProductId = ci.ProductId,
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            };

            return Ok(dto);
        }

        // POST: api/cart
        [HttpPost]
        public async Task<ActionResult<CartItemDto>> PostCartItem(CartItemDto dto)
        {
            // Ellenőrizzük, hogy a Customer és Product létezik-e
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == dto.CustomerId);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);

            if (!customerExists || product == null)
                return BadRequest("Invalid CustomerId or ProductId");

            var cartItem = new CartItem
            {
                CustomerId = dto.CustomerId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            dto.CartItemId = cartItem.Id;
            dto.Price = product.Price;
            dto.CustomerName = (await _context.Customers.FindAsync(dto.CustomerId))?.Name;
            dto.ProductName = product.Name;

            return CreatedAtAction(nameof(GetCartItem), new { id = cartItem.Id }, dto);
        }

        // PUT: api/cart/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItem(int id, CartItemDto dto)
        {
            if (id != dto.CartItemId)
                return BadRequest("ID mismatch");

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
                return NotFound();

            // Frissítjük a mezőket
            cartItem.Quantity = dto.Quantity;
            cartItem.CustomerId = dto.CustomerId;
            cartItem.ProductId = dto.ProductId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
                return NotFound();

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.Id == id);
        }
    }
}
