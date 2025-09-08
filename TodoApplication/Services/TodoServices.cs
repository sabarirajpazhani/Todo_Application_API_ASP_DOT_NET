using Microsoft.AspNetCore.Mvc;
using TodoApplication.DataAccess;
using TodoApplication.DTO;
using TodoApplication.Models;

namespace TodoApplication.Services
{
    public class TodoServices
    {
        private readonly TodoDataAccesscs _dataAccessContext;

        public TodoServices(TodoDataAccesscs dataAccessContext)
        {
            _dataAccessContext = dataAccessContext; 
        }

        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodoAsync()
        {
            return await _dataAccessContext.GetAllTodosAsync();
        }

        public async Task<ActionResult<Todo>> GetTodoByIDAsync(int id)
        {
            return await _dataAccessContext.GetTodoByIDAsync(id);
        }

        public async Task<Todo> UpdateTodoByIDAsync(int id, Todo todo)
        {
            return await _dataAccessContext.UpdateTodoByIDAsync(id, todo);  

        }

        public async Task<Todo> AddTodoAsync(todoDTO todo)
        {
            var newTodo = new Todo
            {
                Title = todo.title,
                Description = todo.description,
                IsComplete = todo.isComplete,
            };

            return await _dataAccessContext.AddTodoAsync(newTodo);
        }


        public async Task<Todo> DeleteTodoAsync(int id)
        {
            return await _dataAccessContext.DeleteTodoAsync(id);    
        }
    }
}
