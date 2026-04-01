using Microsoft.AspNetCore.Mvc;

[Route("feedback")]
public class FeedbackController : Controller
{
    // GET: Show form
    [HttpGet("form")]
    public IActionResult Form()
    {
        return View();
    }

    // POST: Handle submission
    [HttpPost("submit")]
    public IActionResult Submit(IFormCollection form)
    {
        // Manual form handling (no model binding)
        string name = form["Name"];
        string comments = form["Comments"];
        int rating = int.Parse(form["Rating"]);

        // Conditional logic
        if (rating >= 4)
        {
            ViewData["Message"] = $"Thank You {name}! 😊";
        }
        else
        {
            ViewData["Message"] = $"Thanks {name}, we will improve 😔";
        }

        return View("Form");
    }
}