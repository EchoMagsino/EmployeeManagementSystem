using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Employee_Management_System.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public EmployeesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Employees
        public async Task<IActionResult> Index(string searchString)
        {
            var employees = from e in _context.Employees select e;

            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e =>
                e.FirstName.Contains(searchString) ||
                e.LastName.Contains(searchString) ||
                (e.FirstName + " " + e.LastName).Contains(searchString)
                );

            }
            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeNumber,FirstName,LastName,Email,PhoneNumber,Department,Position,HireDate,Role,IsActive")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeNumber,FirstName,LastName,Email,PhoneNumber,Department,Position,HireDate,Role,IsActive")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();

                    var user = await _userManager.FindByEmailAsync(employee.Email);

                    if (user != null)
                    {
                        var currentRoles = await _userManager.GetRolesAsync(user);

                        if (employee.Role == "Manager" && !currentRoles.Contains("Manager"))
                        {
                            await _userManager.AddToRoleAsync(user, "Manager");
                            await _userManager.RemoveFromRoleAsync(user, "Employee");

                        }
                        else if (employee.Role == "HR" && !currentRoles.Contains("HR"))
                        {
                            await _userManager.AddToRoleAsync(user, "HR");
                            await _userManager.RemoveFromRoleAsync(user, "Employee");
                        }


                        else if (employee.Role == "Employee" && !currentRoles.Contains("Employee"))
                        {
                            await _userManager.AddToRoleAsync(user, "Employee");
                            await _userManager.RemoveFromRoleAsync(user, "Manager");
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {



            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
                return NotFound();

            var reviews = _context.PerformanceReviews
                .Where(r => r.EmployeeId == id)
                .ToList();

            _context.PerformanceReviews.RemoveRange(reviews);
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        [HttpGet]
        public JsonResult LiveSearch(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
            {
                return Json(new List<object>());
            }

            term = term.ToLower();

            var results = _context.Employees
                .Where(e => (e.FirstName + " " + e.LastName).ToLower().Contains(term))
                .Select(e => new
                {
                    id = e.Id,
                    firstName = e.FirstName,
                    lastName = e.LastName,
                    email = e.Email,
                    phoneNumber = e.PhoneNumber,
                    department = e.Department,
                    position = e.Position,
                    hireDate = e.HireDate.ToString("yyyy-MM-dd"),
                    role = e.Role,
                    isActive = e.IsActive
                })
                .ToList();

            return Json(results);
        }
    }
}
