using LibraryManagementApi.Data;
using LibraryManagementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _context;
    public MemberRepository(LibraryDbContext context) => _context = context;

    public async Task AddAsync(Member member) => await _context.Members.AddAsync(member);
    public async Task DeleteAsync(int id)
    {
        var m = await GetByIdAsync(id);
        if (m != null) _context.Members.Remove(m);
    }

    public async Task<IEnumerable<Member>> GetAllAsync() => await _context.Members.ToListAsync();
    public async Task<Member?> GetByIdAsync(int id) => await _context.Members.FindAsync(id);

    public Task SaveChangesAsync() => _context.SaveChangesAsync();

    public Task UpdateAsync(Member member)
    {
        _context.Members.Update(member);
        return Task.CompletedTask;
    }
}
