using LibraryManagementApi.Entities;

namespace LibraryManagementApi.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(int id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
    Task<IEnumerable<Book>> SearchAsync(string? title, string? author, string? isbn);
    Task<IEnumerable<Book>> GetAvailableAsync();
}
