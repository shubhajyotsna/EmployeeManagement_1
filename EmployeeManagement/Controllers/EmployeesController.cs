using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employees.Include(e => e.Designation).Include(e => e.PaymentRule).Include(e => e.WorkHour);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Designation)
                .Include(e => e.PaymentRule)
                .Include(e => e.WorkHour)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DesignationId"] = new SelectList(_context.Designations, "DesignationId", "DesignationName");
            ViewData["PaymentRuleId"] = new SelectList(_context.PaymentRules, "PaymentRuleId", "PaymentRuleName");
            ViewData["WorkHourId"] = new SelectList(_context.WorkHours, "WorkHourId", "WorkHour");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,DesignationId,WorkHourId,PaymentRuleId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignationId"] = new SelectList(_context.Designations, "DesignationId", "DesignationId", employee.DesignationId);
            ViewData["PaymentRuleId"] = new SelectList(_context.PaymentRules, "PaymentRuleId", "PaymentRuleId", employee.PaymentRuleId);
            ViewData["WorkHourId"] = new SelectList(_context.WorkHours, "WorkHourId", "WorkHourId", employee.WorkHourId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DesignationId"] = new SelectList(_context.Designations, "DesignationId", "DesignationId", employee.DesignationId);
            ViewData["PaymentRuleId"] = new SelectList(_context.PaymentRules, "PaymentRuleId", "PaymentRuleId", employee.PaymentRuleId);
            ViewData["WorkHourId"] = new SelectList(_context.WorkHours, "WorkHourId", "WorkHourId", employee.WorkHourId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,DesignationId,WorkHourId,PaymentRuleId")] Employee employee)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            ViewData["DesignationId"] = new SelectList(_context.Designations, "DesignationId", "DesignationName", employee.DesignationId);
            ViewData["PaymentRuleId"] = new SelectList(_context.PaymentRules, "PaymentRuleId", "PaymentRuleName", employee.PaymentRuleId);
            ViewData["WorkHourId"] = new SelectList(_context.WorkHours, "WorkHourId", "WorkHour", employee.WorkHourId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Designation)
                .Include(e => e.PaymentRule)
                .Include(e => e.WorkHour)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
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
            if (_context.Employees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
