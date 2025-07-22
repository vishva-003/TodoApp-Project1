using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class Todo_AdminController : Controller
    {
        private readonly TodoAdminContext _context;

        public Todo_AdminController(TodoAdminContext context)
        {
            _context = context;
        }

        // GET: Todo_Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Todo_Admin.ToListAsync());
        }

        //admin task

        public async Task<IActionResult> Admin_Tasks()
        {
            return View(await _context.Todo_Admin.ToListAsync());
        }
        [HttpPost]

        public async Task<JsonResult> ReceiveData([FromBody] Todo_Admin AddAdmintask)
        {
            int? Id = TempData["Id"] as int?;
            string? Role = TempData["Role"] as string;
            
            if (AddAdmintask != null)
            {

                try
                {

                    var test = new Todo_Admin
                    {
                       
                        Admin_Title = AddAdmintask.Admin_Title,
                        Admin_Description = AddAdmintask.Admin_Description,
                        Id=ViewBag.Id,
                        Admin_TaskDate = DateTime.Now,
                    };
                    

                    _context.Todo_Admin.Add(test);
                    await _context.SaveChangesAsync();
                    return Json(new { message = $"Recived {AddAdmintask.Admin_Title} Datails", success = true, redirectUrl = "/Todo_Admin/Index" });
                    

                }

                catch (Exception ex)
                {
                    
                    return Json(new { message = $"Error {ex} Datails", data = AddAdmintask });

                }

            }
            return Json(new { message = $"Failed to upload users details", data = AddAdmintask });
        }



        // GET: Todo_Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo_Admin = await _context.Todo_Admin
                .FirstOrDefaultAsync(m => m.Admin_Id == id);
            if (todo_Admin == null)
            {
                return NotFound();
            }

            return View(todo_Admin);
        }

        // GET: Todo_Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo_Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Admin_Id,Admin_Title,Admin_Description,Admin_TaskDate,Id")] Todo_Admin todo_Admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo_Admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todo_Admin);
        }

        // GET: Todo_Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo_Admin = await _context.Todo_Admin.FindAsync(id);
            if (todo_Admin == null)
            {
                return NotFound();
            }
            return View(todo_Admin);
        }

        // POST: Todo_Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Admin_Id,Admin_Title,Admin_Description,Admin_TaskDate,Id")] Todo_Admin todo_Admin)
        {
            if (id != todo_Admin.Admin_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo_Admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Todo_AdminExists(todo_Admin.Admin_Id))
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
            return View(todo_Admin);
        }

        // GET: Todo_Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo_Admin = await _context.Todo_Admin
                .FirstOrDefaultAsync(m => m.Admin_Id == id);
            if (todo_Admin == null)
            {
                return NotFound();
            }

            return View(todo_Admin);
        }

        // POST: Todo_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo_Admin = await _context.Todo_Admin.FindAsync(id);
            if (todo_Admin != null)
            {
                _context.Todo_Admin.Remove(todo_Admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Todo_AdminExists(int id)
        {
            return _context.Todo_Admin.Any(e => e.Admin_Id == id);
        }
    }
}
