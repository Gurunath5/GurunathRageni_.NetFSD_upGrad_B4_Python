using Contact_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Contact_Management.Models;


namespace YourApp.Controllers
{
    public class ContactController : Controller
    {
        // In-memory storage
        private static List<ContactInfo> contacts = new List<ContactInfo>()
        {
            new ContactInfo { ContactId = 1, FirstName = "Gurunath", LastName = "Rageni", CompanyName = "Cognizant", EmailId = "gurunath918231@gmail.com", MobileNo = 9182310674, Designation = "Manager" }
        };

        // 1. Show All Contacts
        public ActionResult ShowContacts()
        {
            return View(contacts);
        }

        // 2. Get Contact By ID
        public ActionResult GetContactById(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.ContactId == id);
            return View(contact);
        }

        // 3. GET: Add Contact Page
        public ActionResult AddContact()
        {
            return View();
        }

        // 4. POST: Add Contact
        [HttpPost]
        public ActionResult AddContact(ContactInfo contactInfo)
        {
            contactInfo.ContactId = contacts.Count + 1;
            contacts.Add(contactInfo);

            return RedirectToAction("ShowContacts");
        }




        [HttpPost]
        public ActionResult SearchById(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.ContactId == id);

            if (contact == null)
            {
                ViewBag.Message = "Contact not found!";
                return View("ShowContacts", contacts);
            }

            return View("GetContactById", contact);
        }
    }
}