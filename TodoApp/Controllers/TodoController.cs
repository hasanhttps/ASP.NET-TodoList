using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Contexts;
using TodoApp.Models.Dtos;
using TodoApp.Models.Entities.Concretes;

namespace TodoApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase {

    // Fields

    private readonly AppDbContext _context;

    // Constructor

    public TodoController(AppDbContext context) {
        _context = context;
    }

    // Methods

    [HttpGet("GetAll")]
    public  IActionResult GetAll() {
        return Ok("Hershey okaydir");
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddTodoItem([FromBody] AddTodoItemDTO addTodoItemDTO) {

        _context.TodoItems.Add(new TodoItem { 
            Title = addTodoItemDTO.Title,
            Description = addTodoItemDTO.Description,
            ExpireDateTime = addTodoItemDTO.ExpireDateTime,
            UserId = addTodoItemDTO.UserId
        });
        _context.SaveChanges();

        return Ok("Item added");
    }
}
