using Microsoft.AspNetCore.Mvc;

[Route("calculator")]
public class CalculatorController : Controller
{
    // GET: Show form
    [HttpGet("add")]
    public IActionResult Add()
    {
        return View();
    }

    // POST: Handle form submission
    [HttpPost("add")]
    public IActionResult Add(IFormCollection form)
    {
        // No model binding → using FormCollection
        int num1 = Convert.ToInt32(form["num1"]);
        int num2 = Convert.ToInt32(form["num2"]);

        int result = num1 + num2;

        // Pass result using ViewData
        ViewData["Result"] = result;

        return View();
    }
}