using Microsoft.AspNetCore.Mvc;

namespace AutoReviewLab.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static readonly string[] Categories =
        [
            "Electronics", "Books", "Clothing", "Home & Kitchen", "Sports", "Toys"
        ];

        private static readonly string[] ProductNames =
        [
            "Wireless Headphones", "Mechanical Keyboard", "Smart Watch", "Ergonomic Chair", "Coffee Mug",
            "Stainless Steel Water Bottle", "Running Shoes", "Backpack", "Desk Lamp", "Bluetooth Speaker"
        ];

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<Product> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Product
            {
                Id = index,
                Name = ProductNames[Random.Shared.Next(ProductNames.Length)],
                Category = Categories[Random.Shared.Next(Categories.Length)],
                Price = Math.Round((decimal)(Random.Shared.NextDouble() * 100 + 10), 2),
                InStock = Random.Shared.Next(0, 2) == 1,
                CreatedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 30))
            })
            .ToArray();
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        public ActionResult<Product> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid product ID.");
            }

            return Ok(new Product
            {
                Id = id,
                Name = ProductNames[Random.Shared.Next(ProductNames.Length)],
                Category = Categories[Random.Shared.Next(Categories.Length)],
                Price = Math.Round((decimal)(Random.Shared.NextDouble() * 100 + 10), 2),
                InStock = true,
                CreatedAt = DateTime.UtcNow
            });
        }
    }
}
