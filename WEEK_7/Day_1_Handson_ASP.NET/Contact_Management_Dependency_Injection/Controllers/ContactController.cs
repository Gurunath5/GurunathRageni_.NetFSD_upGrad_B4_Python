using Microsoft.AspNetCore.Mvc;

public class ContactController : Controller
{
    private readonly IContactService _contactService;

    // Constructor Injection
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    // Show All Contacts
    public IActionResult ShowContacts()
    {
        var contacts = _contactService.GetAllContacts();
        return View(contacts);
    }

    // Get Contact By ID
    public IActionResult GetContactById(int id)
    {
        var contact = _contactService.GetContactById(id);
        return View(contact);
    }

    // GET: Add Contact
    public IActionResult AddContact()
    {
        return View();
    }

    // POST: Add Contact
    [HttpPost]
    public IActionResult AddContact(ContactInfo contactInfo)
    {
        _contactService.AddContact(contactInfo);
        return RedirectToAction("ShowContacts");
    }
}