using Microsoft.AspNetCore.Mvc;
using LibraryManagementApi.Services;
using LibraryManagementApi.DTOs.Books;

namespace LibraryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;
    public BooksController(IBookService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var b = await _service.GetByIdAsync(id);
        if (b == null) return NotFound();
        return Ok(b);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookRequest request)
    {
        var r = await _service.CreateBookAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookRequest request)
    {
        var r = await _service.UpdateBookAsync(id, request);
        if (r == null) return NotFound();
        return Ok(r);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteBookAsync(id);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? author, [FromQuery] string? isbn)
        => Ok(await _service.SearchBooksAsync(title, author, isbn));

    [HttpGet("available")]
    public async Task<IActionResult> Available() => Ok(await _service.GetAvailableAsync());

    [HttpGet("{id}/availability")]
    public async Task<IActionResult> Availability(int id) => Ok(new { Available = await _service.CheckAvailabilityAsync(id) });
}
