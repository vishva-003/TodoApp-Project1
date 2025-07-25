using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IActionResult> Index(string searchString)
        {
            var list = await _context.Todolist_task.ToListAsync();

            if (list != null)
            {
                list = list.Where(t => !t.IsDeleted).ToList();

                if (!string.IsNullOrEmpty(searchString)){
                    list = list.Where(n => n.Title.Contains(searchString) || n.TaskDescription.Contains(searchString)).ToList();
                }
            }
            return View(list);
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
                        TaskDeadline = Addtask.TaskDeadline,
                        //bdate= DateTime.Today.ToString("yyyy,mm,dd") subract
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



       
        [HttpPost]
        public async Task<JsonResult> Completed([FromBody] Todolist_task change)
        {
            if (change == null)
            {
                return Json(new { message = "Error: change parameter is null" });
            }

            try
            {
                var task = _context.Todolist_task.FirstOrDefault(e => e.Title == change.Title);
                {
                    task.IsCompleted = change.IsCompleted;
                   
                }
              ;

                _context.Todolist_task.Update(task);
                await _context.SaveChangesAsync();
                if (task.IsCompleted == true)
                {
                    return Json(new { message = "Status changed to completed" });
                }
                else
                {
                    return Json(new { message = "Status changed to Not completed" });
                }
                   
            }
            catch (Exception ex)
            {
                return Json(new { message = $"Error {ex.Message} Details" });
            }
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
        public async Task<IActionResult> Update([Bind("TaskId,Title,TaskDescription,TaskDate,UpdatedTime,TaskDeadline")] Todolist_task todolist_task)
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
                    task.TaskDeadline = todolist_task.TaskDeadline;
                    task.UpdatedTime = DateTime.Now;
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


        // Delete

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todolist_task = await _context.Todolist_task
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (todolist_task == null)
            {
                return NotFound();
            }

            return View(todolist_task);
        }

        // POST: Todo_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Softdelete(int id)
        {
            var task = await _context.Todolist_task.FindAsync(id);
            if (task != null)
            {
                task.IsDeleted = true;
                task.DeletedTime = DateTime.Now;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


    }

}
