using Microsoft.AspNetCore.Mvc;
using LibraryManagementApi.Services;
using LibraryManagementApi.DTOs.Members;

namespace LibraryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _service;
    public MembersController(IMemberService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var m = await _service.GetByIdAsync(id);
        if (m == null) return NotFound();
        return Ok(m);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberRequest request)
    {
        var r = await _service.CreateMemberAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateMemberRequest request)
    {
        var r = await _service.UpdateMemberAsync(id, request);
        if (r == null) return NotFound();
        return Ok(r);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _service.DeleteMemberAsync(id);
        if (!ok) return NotFound();
        return NoContent();
    }

    [HttpGet("{id}/borrowed")]
    public async Task<IActionResult> Borrowed(int id) => Ok(await _service.GetBorrowedBooksAsync(id));

    [HttpGet("{id}/history")]
    public async Task<IActionResult> History(int id) => Ok(await _service.GetBorrowHistoryAsync(id));
}
