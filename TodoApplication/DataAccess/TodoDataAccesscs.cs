using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TodoApplication.DTO;
using TodoApplication.Models;

namespace TodoApplication.DataAccess
{
    public class TodoDataAccesscs
    {
        private readonly TodoApplicationDbContext _context;

        public TodoDataAccesscs(TodoApplicationDbContext context)
        {
            _context = context; 
        }

        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodosAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<ActionResult<Todo>> GetTodoByIDAsync(int id)
        {
            return await _context.Todos.FindAsync(id);    
        }

        public async Task<Todo> UpdateTodoByIDAsync(int id, Todo todo)
        {
            var existingTodo = await _context.Todos.FindAsync(id);

            if(existingTodo == null)
            {
                throw new KeyNotFoundException("Todo Not Found this ID");
            }

            existingTodo.Title = todo.Title;
            existingTodo.Description = todo.Description;
            existingTodo.IsComplete = todo.IsComplete;  

            await _context.SaveChangesAsync();

            return existingTodo;
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return todo;
        }


        public async Task<Todo> DeleteTodoAsync(int id)
        {
            var deleteTodo =await _context.Todos.FindAsync(id);   

            _context.Todos.Remove(deleteTodo);
            await _context.SaveChangesAsync();  

            return deleteTodo;
        }
     }
}
