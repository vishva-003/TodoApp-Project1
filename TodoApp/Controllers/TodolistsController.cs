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
    public class TodolistsController : Controller
    {
        private readonly TodoAppContext _context;

        public TodolistsController(TodoAppContext context)
        {
            _context = context;
        }
        //git command
        // GET: Todolists
        public async Task<IActionResult> Index(string searchString)
        {
            var list=await _context.Todolist.ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                list = list.Where(n => n.Name.Contains(searchString) || n.Username.Contains(searchString)).ToList();
            }
            return View(list);
            
        }

        //Register 
        public async Task<IActionResult>Register()
        {
            return View(await _context.Todolist.ToListAsync());
        }

        [HttpPost]

        public async Task<JsonResult> ReceiveData([FromBody] Todolist userdata)
        {
            
            if(userdata != null)
            {
                try
                {

                    _context.Todolist.Add(userdata);
                    await _context.SaveChangesAsync();
                    return Json(new { message = $"Recived {userdata.Name} Datails", data = userdata });
                }
                catch (Exception ex)
                {
                    return Json(new { message = $"Error {ex} Datails", data = userdata });

                }
            }
            return Json(new { message = $"Failed to upload users details", data = userdata });
        }

        //Login 
        public async Task<IActionResult> Login()
        {
            return View(await _context.Todolist.ToListAsync());
        }

        [HttpPost]
        public async Task<JsonResult>Todocheck([FromBody] Todolist Userscheck)
        {
            var user = _context.Todolist.FirstOrDefault(u => u.Email == Userscheck.Email && u.Password == Userscheck.Password);
            if (user != null)
            {
                TempData["Id"] = user.Id;
                TempData["useId"] = user.Id;

                if (user.Role == "Admin")
                {
                    return Json(new { success = true, redirectUrl = "/Todo_Admin/Admin_Tasks"});
               
                }
                else if (user.Role == "user" || user.Role == "User")
                {
                    return Json(new { success = true, redirectUrl = "/Todolist_task/Task" });
                }
                else
                {
                    return Json("user not admin/User");
                }

                
              //  return RedirectToAction("Task","Todolist_task");


            }
            else
            {
                return Json(new { success = false, message = "Invalid login" });
            }

        }

        // GET: Todolists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todolist == null)
            {
                return NotFound();
            }

            return View(todolist);
        }

        // GET: Todolists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todolists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Username,Password,Role")] Todolist todolist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todolist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todolist);
        }

        // GET: Todolists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist.FindAsync(id);
            if (todolist == null)
            {
                return NotFound();
            }
            return View(todolist);
        }

        // POST: Todolists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Username,Password,Role")] Todolist todolist)
        {
            if (id != todolist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todolist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodolistExists(todolist.Id))
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
            return View(todolist);
        }

        // GET: Todolists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todolist = await _context.Todolist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todolist == null)
            {
                return NotFound();
            }

            return View(todolist);
        }

        // POST: Todolists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todolist = await _context.Todolist.FindAsync(id);
            if (todolist != null)
            {
                _context.Todolist.Remove(todolist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodolistExists(int id)
        {
            return _context.Todolist.Any(e => e.Id == id);
        }
    }
}
