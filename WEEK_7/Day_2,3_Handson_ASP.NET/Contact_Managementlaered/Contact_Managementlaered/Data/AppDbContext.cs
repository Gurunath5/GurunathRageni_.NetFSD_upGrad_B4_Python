using Microsoft.EntityFrameworkCore;
using Contact_Managementlaered.Models;

public class AppDbContext : DbContext
{
    // ✅ Constructor for DI
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ContactInfo> Contacts { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Company → Contacts (1-M)
        modelBuilder.Entity<ContactInfo>()
            .HasOne(c => c.Company)
            .WithMany(c => c.Contacts)
            .HasForeignKey(c => c.CompanyId);

        // Department → Contacts (1-M, optional)
        modelBuilder.Entity<ContactInfo>()
            .HasOne(c => c.Department)
            .WithMany(d => d.Contacts)
            .HasForeignKey(c => c.DepartmentId)
            .IsRequired(false);
    }
}