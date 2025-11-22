using Microsoft.AspNetCore.Mvc;
using LibraryManagementApi.Services;
using LibraryManagementApi.DTOs.Borrow;

namespace LibraryManagementApi.Controllers;

[ApiController]
[Route("api")]
public class BorrowController : ControllerBase
{
    private readonly IBorrowService _service;
    public BorrowController(IBorrowService service) => _service = service;

    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow(BorrowBookRequest request)
    {
        var r = await _service.BorrowBookAsync(request);
        if (r == null) return BadRequest(new { Message = "Cannot borrow (unavailable or invalid ids)" });
        return Ok(r);
    }

    [HttpPost("return")]
    public async Task<IActionResult> Return(ReturnBookRequest request)
    {
        var r = await _service.ReturnBookAsync(request);
        if (r == null) return NotFound(new { Message = "Borrow record not found or already returned" });
        return Ok(r);
    }

    [HttpGet("borrow/member/{memberId}")]
    public async Task<IActionResult> ByMember(int memberId) => Ok(await _service.GetBorrowedByMemberAsync(memberId));

    [HttpGet("borrow/book/{bookId}")]
    public async Task<IActionResult> ByBook(int bookId) => Ok(await _service.GetBorrowHistoryByBookAsync(bookId));

    [HttpGet("return/pending")]
    public async Task<IActionResult> Pending() => Ok(await _service.GetPendingReturnsAsync());
}
