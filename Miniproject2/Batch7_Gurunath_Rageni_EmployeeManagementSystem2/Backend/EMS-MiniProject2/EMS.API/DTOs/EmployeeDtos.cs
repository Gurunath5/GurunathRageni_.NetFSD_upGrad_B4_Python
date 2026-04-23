using System.ComponentModel.DataAnnotations;

namespace EMS.API.DTOs
{
    // ── Request DTO (used for POST and PUT) ─────────────────────────────────────
    public class EmployeeRequestDto
    {
        [Required] public string FirstName { get; set; } = "";
        [Required] public string LastName { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";

        [Required] public string Phone { get; set; } = "";
        [Required] public string Department { get; set; } = "";
        [Required] public string Designation { get; set; } = "";

        [Required, Range(1, double.MaxValue, ErrorMessage = "Salary must be positive")]
        public decimal Salary { get; set; }

        [Required] public string JoinDate { get; set; } = "";   // "yyyy-MM-dd" from frontend date input
        [Required] public string Status { get; set; } = "";
    }

    // ── Response DTO (returned by all employee endpoints) ───────────────────────
    public class EmployeeResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";   // "FirstName LastName"
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Department { get; set; } = "";
        public string Designation { get; set; } = "";
        public decimal Salary { get; set; }
        public string JoinDate { get; set; } = "";   // formatted "dd MMM yyyy"
        public string Status { get; set; } = "";
    }

    // ── Paginated envelope ──────────────────────────────────────────────────────
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPrevPage { get; set; }
    }

    // ── Query parameters (bound from GET /api/employees query string) ───────────
    public class EmployeeQueryParams
    {
        public string? Search { get; set; }
        public string? Department { get; set; }
        public string? Status { get; set; }
        public string SortBy { get; set; } = "name";
        public string SortDir { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    // ── Auth DTOs ────────────────────────────────────────────────────────────────
    public class AuthRequestDto
    {
        [Required] public string Username { get; set; } = "";
        [Required] public string Password { get; set; } = "";
        public string Role { get; set; } = "Viewer";
    }

    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";
        public string Token { get; set; } = "";
        public string Message { get; set; } = "";
    }
}