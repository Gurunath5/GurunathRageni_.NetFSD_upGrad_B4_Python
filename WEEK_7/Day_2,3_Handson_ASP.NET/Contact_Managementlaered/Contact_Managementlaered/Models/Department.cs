using Contact_Managementlaered.Models;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public List<ContactInfo> Contacts { get; set; }
}