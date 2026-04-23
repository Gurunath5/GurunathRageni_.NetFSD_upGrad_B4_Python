using EMS.API.DTOs;
using EMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;
        public async Task<bool> EmailExistsAsync(string email, int? excludeId = null)
    => await _repository.EmailExistsAsync(email, excludeId);

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // ── GET all (search / filter / sort / paginate) ─────────────────────────
        public async Task<PagedResult<EmployeeResponseDto>> GetEmployeesAsync(EmployeeQueryParams q)
        {
            var query = _repository.GetAllAsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(q.Search))
            {
                var term = q.Search.ToLower();
                query = query.Where(e =>
                    (e.FirstName + " " + e.LastName).ToLower().Contains(term) ||
                    e.Email.ToLower().Contains(term));
            }

            // Filter
            if (!string.IsNullOrWhiteSpace(q.Department) && q.Department != "All")
                query = query.Where(e => e.Department == q.Department);

            if (!string.IsNullOrWhiteSpace(q.Status) && q.Status != "All")
                query = query.Where(e => e.Status == q.Status);

            // Sort
            bool isDesc = q.SortDir?.ToLower() == "desc";
            query = q.SortBy?.ToLower() switch
            {
                "salary" => isDesc ? query.OrderByDescending(e => e.Salary) : query.OrderBy(e => e.Salary),
                "joindate" => isDesc ? query.OrderByDescending(e => e.JoinDate) : query.OrderBy(e => e.JoinDate),
                _ => isDesc
                    ? query.OrderByDescending(e => e.LastName).ThenByDescending(e => e.FirstName)
                    : query.OrderBy(e => e.LastName).ThenBy(e => e.FirstName)
            };

            // Paginate
            int pageSize = Math.Min(q.PageSize, 100);   // cap at 100
            int page = Math.Max(q.Page, 1);
            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<EmployeeResponseDto>
            {
                Data = items.Select(MapToDto).ToList(),
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                HasNextPage = page < totalPages,
                HasPrevPage = page > 1
            };
        }

        // ── GET dashboard ───────────────────────────────────────────────────────
        public async Task<object> GetDashboardAsync()
        {
            var query = _repository.GetAllAsQueryable();

            var total = await query.CountAsync();
            var active = await query.CountAsync(e => e.Status == "Active");
            var inactive = await query.CountAsync(e => e.Status == "Inactive");
            var deptCount = await query.Select(e => e.Department).Distinct().CountAsync();

            var breakdown = await query
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    name = g.Key,
                    count = g.Count(),
                    percent = total == 0 ? 0 : (int)Math.Round((double)g.Count() / total * 100)
                })
                .OrderBy(d => d.name)
                .ToListAsync();

            var recent = await query
                .OrderByDescending(e => e.CreatedAt)
                .ThenByDescending(e => e.Id)
                .Take(5)
                .ToListAsync();

            return new
            {
                total,
                active,
                inactive,
                departments = deptCount,
                breakdown,
                recent = recent.Select(MapToDto).ToList()
            };
        }

        // ── GET by id ───────────────────────────────────────────────────────────
        public async Task<EmployeeResponseDto?> GetByIdAsync(int id)
        {
            var emp = await _repository.GetByIdAsync(id);
            return emp == null ? null : MapToDto(emp);
        }

        // ── POST create ─────────────────────────────────────────────────────────
        public async Task<EmployeeResponseDto> CreateAsync(EmployeeRequestDto dto)
        {
            var emp = new Employee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                Department = dto.Department,
                Designation = dto.Designation,
                Salary = dto.Salary,
                JoinDate = DateTime.Parse(dto.JoinDate),
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(emp);
            await _repository.SaveChangesAsync();
            return MapToDto(emp);
        }

        // ── PUT update ──────────────────────────────────────────────────────────
        public async Task<EmployeeResponseDto?> UpdateAsync(int id, EmployeeRequestDto dto)
        {
            var emp = await _repository.GetByIdAsync(id);
            if (emp == null) return null;

            emp.FirstName = dto.FirstName;
            emp.LastName = dto.LastName;
            emp.Email = dto.Email;
            emp.Phone = dto.Phone;
            emp.Department = dto.Department;
            emp.Designation = dto.Designation;
            emp.Salary = dto.Salary;
            emp.JoinDate = DateTime.Parse(dto.JoinDate);
            emp.Status = dto.Status;
            emp.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(emp);
            await _repository.SaveChangesAsync();
            return MapToDto(emp);
        }

        // ── DELETE ──────────────────────────────────────────────────────────────
        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _repository.GetByIdAsync(id);
            if (emp == null) return false;

            await _repository.DeleteAsync(emp);
            await _repository.SaveChangesAsync();
            return true;
        }

        // ── Mapper ──────────────────────────────────────────────────────────────
        private static EmployeeResponseDto MapToDto(Employee e) => new()
        {
            Id = e.Id,
            Name = $"{e.FirstName} {e.LastName}",
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Phone = e.Phone,
            Department = e.Department,
            Designation = e.Designation,
            Salary = e.Salary,
            JoinDate = e.JoinDate.ToString("dd MMM yyyy"),
            Status = e.Status
        };
    }
}