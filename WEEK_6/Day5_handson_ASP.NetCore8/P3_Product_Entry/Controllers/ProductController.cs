using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[Route("product")]
public class ProductController : Controller
{
    // GET: Show page
    [HttpGet("index")]
    public IActionResult Index()
    {
        var products = GetProductsFromSession();
        ViewBag.Products = products;
        return View();
    }

    // POST: Add product
    [HttpPost("add")]
    public IActionResult Add(IFormCollection form)
    {
        var products = GetProductsFromSession();

        // Manual form handling (no model binding)
        string name = form["Name"];
        int price = int.Parse(form["Price"]);
        int quantity = int.Parse(form["Quantity"]);

        products.Add(new Product
        {
            Name = name,
            Price = price,
            Quantity = quantity
        });

        SaveProductsToSession(products);

        ViewBag.Products = products;
        return View("Index");
    }

    // Helper: Get list from session
    private List<Product> GetProductsFromSession()
    {
        var data = HttpContext.Session.GetString("Products");

        if (data == null)
            return new List<Product>();

        return JsonSerializer.Deserialize<List<Product>>(data);
    }

    // Helper: Save list to session
    private void SaveProductsToSession(List<Product> products)
    {
        var data = JsonSerializer.Serialize(products);
        HttpContext.Session.SetString("Products", data);
    }
}