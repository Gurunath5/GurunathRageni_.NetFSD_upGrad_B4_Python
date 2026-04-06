using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Contact_Managementlaered.Models;

public class ContactController : Controller
{
    private readonly IContactRepository _repo;
    private readonly AppDbContext _context;

    public ContactController(IContactRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    // ✅ READ ALL (INDEX)
    public IActionResult Index()
    {
        var contacts = _repo.GetAllContacts();
        return View(contacts);
    }

    // ✅ DETAILS
    public IActionResult Details(int id)
    {
        var contact = _repo.GetContactById(id);
        return View(contact);
    }

    // ✅ CREATE (GET)
    public IActionResult Create()
    {
        LoadDropdowns();
        return View();
    }

    // ✅ CREATE (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ContactInfo contact)
    {
        if (!ModelState.IsValid)
        {
            LoadDropdowns(); // 🔥 MUST reload dropdowns
            return View(contact);
        }

        _repo.AddContact(contact);
        return RedirectToAction("Index");
    }


    // ✅ EDIT (GET)
    public IActionResult Edit(int id)
    {
        var contact = _repo.GetContactById(id);
        LoadDropdowns();
        return View(contact);
    }

    // ✅ EDIT (POST)
    [HttpPost]
    public IActionResult Edit(ContactInfo contact)
    {
        if (ModelState.IsValid)
        {
            _repo.UpdateContact(contact);
            return RedirectToAction("Index");
        }
        LoadDropdowns();
        return View(contact);
    }

    // ✅ DELETE
    public IActionResult Delete(int id)
    {
        _repo.DeleteContact(id);
        return RedirectToAction("Index");
    }

    // ✅ DROPDOWN DATA
    private void LoadDropdowns()
    {
        ViewBag.Companies = new SelectList(_context.Companies.ToList(), "CompanyId", "CompanyName");
        ViewBag.Departments = new SelectList(_context.Departments.ToList(), "DepartmentId", "DepartmentName");
    }
}