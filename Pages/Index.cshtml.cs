using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TodoItem> TodoItems { get; set; } = new List <TodoItem>();

        public async Task OnGetAsync()
        {
            TodoItems = await _context.TodoItems.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var newItem = new TodoItem { Name = name, IsComplete = false };
                _context.TodoItems.Add(newItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleCompleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                item.IsComplete = !item.IsComplete;
                _context.TodoItems.Update(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
