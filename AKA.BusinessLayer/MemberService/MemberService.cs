using AKA.DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AKA.BusinessLayer.MemberService
{
    public class MemberService : IMemberService
    {
        private readonly LibraryContext _ctx;

        public MemberService(LibraryContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Member>> GetAllMembers()
        {
            return await _ctx.Members.ToListAsync();
        }

        public async Task<bool> HasPendingSignOut(int memberId, int libraryId, int bookId)
        {
            return await _ctx.BookSignedOuts
                .Join(_ctx.Library_Book,
                    q => q.LibraryBookSId,
                    q => q.LibraryBookSId,
                    (q, w) => new { so = q, lb = w })
                    .Where(q => !q.so.WhenReturned.HasValue)
                    .AnyAsync(q => q.lb.LibraryId == libraryId && q.so.MemberId == memberId && q.lb.BookId == bookId);
        }

        public async Task<int> GetMemberSignOutCount(int memberId, int libraryId)
        {
            return await _ctx.BookSignedOuts
                .Join(_ctx.Library_Book,
                    q => q.LibraryBookSId,
                    q => q.LibraryBookSId,
                    (q, w) => new { so = q, lb = w })
                    .CountAsync(q => q.lb.LibraryId == libraryId && q.so.MemberId == memberId);
        }

        public async Task ReturnBook(int bookId, int libraryId, int memberId)
        {
            var bookSignOut = _ctx.BookSignedOuts
                .Join(_ctx.Library_Book,
                    q => q.LibraryBookSId,
                    q => q.LibraryBookSId,
                    (q, w) => new { so = q, lb = w })
                .Where(q => q.lb.LibraryId == libraryId && q.lb.BookId == bookId && q.so.MemberId == memberId)
                .Select(q => q.so)
                .First();

            bookSignOut.WhenReturned = DateTime.UtcNow;
            await _ctx.SaveChangesAsync();
        }

        public async Task SignOutBookAsync(int bookId, int libraryId, int memberId)
        {
            var libraryBook = await _ctx.Library_Book
                .SingleAsync(q => q.BookId == bookId && q.LibraryId == libraryId);

            _ctx.BookSignedOuts
                .Add(new BookSignedOut
                {
                    MemberId = memberId,
                    LibraryBookSId = libraryBook.LibraryBookSId,
                    WhenSignedOut = DateTime.UtcNow
                });

            await _ctx.SaveChangesAsync();
        }

        public async Task<List<Member>> GetMembersWithPendingSignOuts(int libraryId)
        {
            return await _ctx.BookSignedOuts
                .Join(_ctx.Library_Book,
                    q => q.LibraryBookSId,
                    q => q.LibraryBookSId,
                    (q, w) => new { so = q, lb = w })
                .Where(q => q.lb.LibraryId == libraryId)
                .Where(q => !q.so.WhenReturned.HasValue)
                .Select(q => q.so.Member)
                .Distinct()
                .ToListAsync();
        }
    }
}
