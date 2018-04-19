using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using dotnet.Models;
using System.Linq;

namespace dotnet.Controllers {
    
    [Route("api/[controller]")]
    public class TodoController : Controller {
        private readonly TodoContext context;

        public TodoController(TodoContext context) {
            this.context = context;

            if (this.context.TodoItems.Count() == 0) {
                this.context.TodoItems.Add(new TodoItem { Name = "Item1"});
                this.context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll() {
            return this.context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id) {
            var item = this.context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null) {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item) {
            if (item == null) {
                return BadRequest();
            }
            this.context.TodoItems.Add(item);
            this.context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item) {
            if (item == null || item.Id != id) {
                return BadRequest();
            }
            var todo = this.context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null) {
                return NotFound();
            }
            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            this.context.TodoItems.Update(todo);
            this.context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var todo = this.context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null) {
                return NotFound();
            }

            this.context.TodoItems.Remove(todo);
            this.context.SaveChanges();
            return new NoContentResult();
        }
    }
}