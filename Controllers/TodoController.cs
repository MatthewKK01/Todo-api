using Microsoft.AspNetCore.Mvc;
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
            var todos = await _context.Todos.ToListAsync();
            return Ok(todos);
        }

        [HttpPost]
        [Route("addTodo")]
        public async Task<IActionResult> AddTodo([FromBody] ToDo newTodo)
        {
            await _context.AddAsync(newTodo);
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
            var todo = await _context.FindAsync<ToDo>(id);
            _context.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}