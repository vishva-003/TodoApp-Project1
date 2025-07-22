using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodotaskContext : DbContext
    {
        public TodotaskContext (DbContextOptions<TodotaskContext> options)
            : base(options)
        {
        }

        public DbSet<TodoApp.Models.Todolist_task> Todolist_task { get; set; } = default!;
    }
}
