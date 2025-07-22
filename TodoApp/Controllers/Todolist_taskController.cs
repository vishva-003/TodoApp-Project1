using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TodoApp.Controllers
{
    public class Todolist_taskController : Controller
    {
        private readonly TodotaskContext _context;

        public Todolist_taskController(TodotaskContext context)
        {
            _context = context;
        }
        




        // GET: Todolist_task
        public async Task<IActionResult> Index()
        {
            return View(await _context.Todolist_task.ToListAsync());
        }



        //Todo task

        public async Task<IActionResult> Task()
        {
            return View(await _context.Todolist_task.ToListAsync());
        }
        [HttpPost]

        public async Task<JsonResult> ReceiveData([FromBody] Todolist_task Addtask)
        {
            int? Id = TempData["Id"] as int?;
            string? Role = TempData["Role"] as string;
            ViewBag.Id = Id;
            if (Addtask != null)
            {

                try
                {

                    var test = new Todolist_task
                    {
                        Title = Addtask.Title,
                        TaskDescription = Addtask.TaskDescription,
                        TaskDate = DateTime.Now,
                        Id = Id,


                    };

                    _context.Todolist_task.Add(test);
                    await _context.SaveChangesAsync();
                    return Json(new { message = $"Recived {Addtask.Title} Datails", success = true, redirectUrl = "/Todolist_task/Index" });

                }

                catch (Exception ex)
                {
                    return Json(new { message = $"Error {ex} Datails", data = Addtask });

                }

            }
            return Json(new { message = $"Failed to upload users details", data = Addtask });
        }

        //Edite

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var todolist_task = await _context.Todolist_task.FindAsync(Id);
            if (todolist_task == null)
            {
                return NotFound();
            }
            
            return View(todolist_task);
        }
        private bool Todolist_taskExists(int Id)
        {
            return _context.Todolist_task.Any(e => e.Id == Id);
        }

        //Edit and save 

        [HttpPost]
    
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("TaskId,Title,TaskDescription,TaskDate")] Todolist_task todolist_task)
        {
            //if (id != todolist_task.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid && todolist_task.TaskId!=null)
            {
                try
                {
                    var task = _context.Todolist_task.FirstOrDefault(e => e.TaskId == todolist_task.TaskId);

                    task.TaskDate = todolist_task.TaskDate;
                    task.TaskDescription = todolist_task.TaskDescription;
                    task.Title = todolist_task.Title;
                    //_context.Update(todolist_task);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    
                        throw ex;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todolist_task);
        }

    }

}
