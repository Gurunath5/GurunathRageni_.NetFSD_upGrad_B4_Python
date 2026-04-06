using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class EmployeeController : Controller
{
    private readonly AppDbContext _context;

    public EmployeeController(AppDbContext context)
    {
        _context = context;
    }

    // 🔹 READ ALL
    public async Task<IActionResult> Index()
    {
        return View(await _context.Employees.ToListAsync());
    }

    // 🔹 CREATE
    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(Employee emp)
    {
        if (ModelState.IsValid)
        {
            _context.Add(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(emp);
    }

    // 🔹 EDIT
    public async Task<IActionResult> Edit(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        return View(emp);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee emp)
    {
        _context.Update(emp);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // 🔹 DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var emp = await _context.Employees.FindAsync(id);
        return View(emp);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Employee emp)
    {
        _context.Employees.Remove(emp);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    // 🔥 LINQ FEATURES 🔥

    // 🔍 SEARCH
    public async Task<IActionResult> Search(string name)
    {
        var data = await _context.Employees
            .Where(e => e.Name.Contains(name))
            .ToListAsync();

        return View("Index", data);
    }

    // 🔽 FILTER
    public async Task<IActionResult> Filter(string dept)
    {
        var data = await _context.Employees
            .Where(e => e.Department == dept)
            .ToListAsync();

        return View("Index", data);
    }

    // 🔼 SORT
    public async Task<IActionResult> Sort(bool asc)
    {
        var data = asc
            ? await _context.Employees.OrderBy(e => e.Salary).ToListAsync()
            : await _context.Employees.OrderByDescending(e => e.Salary).ToListAsync();

        return View("Index", data);
    }

    // 📊 GROUP BY DEPARTMENT
    public IActionResult GroupByDept()
    {
        var data = _context.Employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                Count = g.Count(),
                AvgSalary = g.Average(x => x.Salary)
            }).ToList();

        return View(data);
    }

    // 📊 TOTAL COUNT
    public IActionResult Count()
    {
        ViewBag.Total = _context.Employees.Count();
        return View();
    }

    // 💰 HIGHEST SALARY
    public IActionResult HighestSalary()
    {
        var emp = _context.Employees
            .OrderByDescending(e => e.Salary)
            .FirstOrDefault();

        return View(emp);
    }

    // 📅 DATE RANGE
    public async Task<IActionResult> DateFilter(DateTime start, DateTime end)
    {
        var data = await _context.Employees
            .Where(e => e.HireDate >= start && e.HireDate <= end)
            .ToListAsync();

        return View("Index", data);
    }
}