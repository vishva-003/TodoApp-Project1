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

        public async Task<JsonResult>DataRec([FromBody] Todo_Admin AddAdmintask)
        {
            int? Id = TempData["useId"] as int?;
            ViewBag.useId = Id;

            if (AddAdmintask != null)
            {

                try
                {

                    var test = new Todo_Admin
                    {
                       
                        Admin_Title = AddAdmintask.Admin_Title,
                        Admin_Description = AddAdmintask.Admin_Description,
                        Id=Id,
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

        private bool Todolist_taskExists(int Id)
        {
            return _context.Todo_Admin.Any(e => e.Id == Id);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Admin_Id,Admin_Title,Admin_Description,Admin_TaskDate")] Todo_Admin todo_Admin)
        {


            if (ModelState.IsValid && todo_Admin.Admin_Id != null)
            {
                try
                {
                    var task = _context.Todo_Admin.FirstOrDefault(e => e.Admin_Id == todo_Admin.Admin_Id);

                    task.Admin_TaskDate = todo_Admin.Admin_TaskDate;
                    task.Admin_Description = todo_Admin.Admin_Description;
                    task.Admin_Title = todo_Admin.Admin_Title;
                    //_context.Update(todolist_task);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw ex;
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
