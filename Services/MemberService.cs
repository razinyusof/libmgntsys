using AutoMapper;
using LibraryManagementApi.DTOs.Members;
using LibraryManagementApi.Entities;
using LibraryManagementApi.Repositories;

namespace LibraryManagementApi.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _repo;
    private readonly IBorrowRepository _borrowRepo;
    private readonly IMapper _mapper;

    public MemberService(IMemberRepository repo, IBorrowRepository borrowRepo, IMapper mapper)
    {
        _repo = repo;
        _borrowRepo = borrowRepo;
        _mapper = mapper;
    }

    public async Task<MemberResponse> CreateMemberAsync(CreateMemberRequest request)
    {
        var m = _mapper.Map<Member>(request);
        await _repo.AddAsync(m);
        await _repo.SaveChangesAsync();
        return _mapper.Map<MemberResponse>(m);
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        var m = await _repo.GetByIdAsync(id);
        if (m == null) return false;
        await _repo.DeleteAsync(id);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<MemberResponse>> GetAllAsync()
    {
        var ms = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<MemberResponse>>(ms);
    }

    public async Task<MemberResponse?> GetByIdAsync(int id)
    {
        var m = await _repo.GetByIdAsync(id);
        return m == null ? null : _mapper.Map<MemberResponse>(m);
    }

    public async Task<IEnumerable<object>> GetBorrowedBooksAsync(int memberId)
    {
        var records = await _borrowRepo.GetByMemberAsync(memberId);
        var list = records.Where(r => r.ReturnedAt == null)
            .Select(r => new {
                r.Id,
                r.BookId,
                r.BorrowedAt
            });
        return list.ToList();
    }

    public async Task<IEnumerable<object>> GetBorrowHistoryAsync(int memberId)
    {
        var records = await _borrowRepo.GetByMemberAsync(memberId);
        var list = records.Select(r => new {
            r.Id,
            r.BookId,
            r.BorrowedAt,
            r.ReturnedAt
        });
        return list.ToList();
    }

    public async Task<MemberResponse?> UpdateMemberAsync(int id, CreateMemberRequest request)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return null;
        existing.FullName = request.FullName;
        existing.Email = request.Email;
        existing.Phone = request.Phone;
        await _repo.UpdateAsync(existing);
        await _repo.SaveChangesAsync();
        return _mapper.Map<MemberResponse>(existing);
    }
}
