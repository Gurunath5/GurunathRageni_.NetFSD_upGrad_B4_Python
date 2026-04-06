using System.Collections.Generic;

public interface IContactService
{
    List<ContactInfo> GetAllContacts();
    ContactInfo GetContactById(int id);
    void AddContact(ContactInfo contact);
}