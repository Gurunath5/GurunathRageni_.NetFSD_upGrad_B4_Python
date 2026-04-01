using Microsoft.AspNetCore.Mvc;

namespace P1_Student_Controller.Controllers
{
        [Route("student")]
        public class StudentController : Controller
        {
            // ✅ GET: Display Form
            [HttpGet("register")]
            public IActionResult Register()
            {
                return View();
            }

            // ✅ POST: Handle Form Submission
            [HttpPost("register")]
            public IActionResult Register(string studentName, int age, string course)
            {
                // Store data in ViewBag
                ViewBag.Name = studentName;
                ViewBag.Age = age;
                ViewBag.Course = course;

                // Redirect to Display action
                return RedirectToAction("Display", new
                {
                    name = studentName,
                    age = age,
                    course = course
                });
            }

            // ✅ Display Page
            [HttpGet("display")]
            public IActionResult Display(string name, int age, string course)
            {
                ViewBag.Name = name;
                ViewBag.Age = age;
                ViewBag.Course = course;

                return View();
            }
    }
}
