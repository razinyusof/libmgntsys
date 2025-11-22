using LibraryManagementApi.Data;
using LibraryManagementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;
    public BookRepository(LibraryDbContext context) => _context = context;

    public async Task AddAsync(Book book) => await _context.Books.AddAsync(book);
    public async Task DeleteAsync(int id)
    {
        var b = await GetByIdAsync(id);
        if (b != null) _context.Books.Remove(b);
    }

    public async Task<IEnumerable<Book>> GetAllAsync() => await _context.Books.ToListAsync();
    public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FindAsync(id);

    public async Task<IEnumerable<Book>> GetAvailableAsync() =>
        await _context.Books.Where(b => b.AvailableCopies > 0).ToListAsync();

    public async Task<IEnumerable<Book>> SearchAsync(string? title, string? author, string? isbn)
    {
        var q = _context.Books.AsQueryable();
        if (!string.IsNullOrWhiteSpace(title)) q = q.Where(b => b.Title.Contains(title));
        if (!string.IsNullOrWhiteSpace(author)) q = q.Where(b => b.Author.Contains(author));
        if (!string.IsNullOrWhiteSpace(isbn)) q = q.Where(b => b.ISBN.Contains(isbn));
        return await q.ToListAsync();
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();

    public Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        return Task.CompletedTask;
    }
}
