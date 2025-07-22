using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class TodoAdminContext : DbContext
    {
        public TodoAdminContext (DbContextOptions<TodoAdminContext> options)
            : base(options)
        {
        }

        public DbSet<TodoApp.Models.Todo_Admin> Todo_Admin { get; set; } = default!;
    }
}
