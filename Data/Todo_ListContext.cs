using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo_List.Models;

namespace Todo_List.Data
{
    public class Todo_ListContext : DbContext
    {
        public Todo_ListContext (DbContextOptions<Todo_ListContext> options)
            : base(options)
        {
        }

        public DbSet<Todo_List.Models.List> List { get; set; } = default!;

        public DbSet<Todo_List.Models.Task> Task { get; set; } = default!;
    }
}
