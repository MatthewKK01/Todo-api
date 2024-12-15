using System.Data;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api")]
    [Controller]
    public class TodoController : Controller
    {
        private readonly AppDBContext _context;

        public TodoController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getTodos")]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _context.Todos.FromSql($"GetTodos").ToListAsync();
            return Ok(todos);
        }

        [HttpPost]
        [Route("addTodo")]
        public async Task<IActionResult> AddTodo([FromBody] ToDo newTodo)
        {
            await _context.Database.ExecuteSqlAsync($"PostTodo {newTodo.Id}, {newTodo.Title},{newTodo.Content},{newTodo.IsDone}");
            await _context.SaveChangesAsync();
            return Ok(newTodo);
        }

        [HttpPut]
        [Route("editTodo/{id}")]
        public async Task<IActionResult> updateTodo([FromRoute] Guid id, [FromBody] ToDo todoModel)
        {
            var myTodo = await _context.Todos.FindAsync(id);
            if (myTodo == null)
            {
                return NotFound();
            }

            myTodo.Title = todoModel.Title;
            myTodo.Content = todoModel.Content;
            myTodo.IsDone = todoModel.IsDone;
             _context.Update(myTodo);
             await _context.SaveChangesAsync();
             return Ok(myTodo);
        }
         

        [HttpDelete]
        [Route("deleteTodo/{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id) 
        {
            var parameter = new SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC DeleteTodo @Id", parameter);
            return Ok(new { Message = "Todo item deleted successfully." });
        }
    }
}