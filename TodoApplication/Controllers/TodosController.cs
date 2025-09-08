
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApplication.DTO;
using TodoApplication.Models;
using TodoApplication.Services;

namespace TodoApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoServices _serviceContext;

        public TodosController (TodoServices serviceContext)
        {
            _serviceContext = serviceContext;
        }

        //Getting All Todos 
        [HttpGet]   
        [Route("AllTodos")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
        {
            try
            {
                var todos = await _serviceContext.GetAllTodoAsync();

                if (todos == null)
                {
                    return NotFound(new { message = "No Todos Founds" });
                }

                return Ok(new { message = "Todos data Retrived Successfully", data = todos });
            }
            catch (Exception ex)
            {
                return BadRequest("Internal Server Error: "+ex.Message);  
            }
        }

        //Getting todo by ID
        [HttpGet("GetTodoByID/{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Todo ID");
                }
                var todo = await _serviceContext.GetTodoByIDAsync(id);

                if (todo == null)
                {
                    return NotFound("ID not Found");
                }

                return Ok(todo);
            }
            catch(Exception ex)
            {
                return BadRequest("Internal Server Error: " + ex.Message);
            }
        }

        //Updating Todo by ID
        [HttpPut("UpdateTodoByID/{id}")]
        public async Task<IActionResult> UpdateTodoByID(int id, [FromBody] Todo todo)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid ID");
                }

                try
                {
                    var updatedTodo = await _serviceContext.UpdateTodoByIDAsync(id, todo);
                    return Ok(new { message = "Todo Updated Successfully!!" });
                }
                catch (KeyNotFoundException)
                {
                    return NotFound("Todo Not Found");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Internal Server Error: " + ex.Message);
            }

        }

        [HttpPost]
        [Route("AddTodo")]

        public async Task<IActionResult> AddTodo([FromBody] todoDTO todo)
        {
            try
            {
                var newTodo = await _serviceContext.AddTodoAsync(todo);
                return Ok("Successfully Added");
            }
            catch (Exception ex)
            {
                return BadRequest("Internal Server Eror : " + ex.Message);
            }
        }


        [HttpDelete("DeleteTodoByID/{id}")]
        public async Task<IActionResult> DeleteTodoByID(int id)
        {
            try
            {
                if(id<= 0)
                {
                    return BadRequest("Invalid ID");
                }

                var deleteTodo = await _serviceContext.DeleteTodoAsync(id);

                if(deleteTodo == null)
                {
                    return NotFound("Todo Not Found by this ID");
                }

                return Ok(new {message="Todo has been Successfully deleted!", data = deleteTodo});
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal Server Error: {ex.Message}"); 
            }

        }

    }
}
