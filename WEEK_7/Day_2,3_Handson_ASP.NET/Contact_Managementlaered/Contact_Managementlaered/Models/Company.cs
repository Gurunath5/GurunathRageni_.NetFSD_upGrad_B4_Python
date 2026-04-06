using Contact_Managementlaered.Models;

public class Company
{
    public int CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public List<ContactInfo> Contacts { get; set; }
}