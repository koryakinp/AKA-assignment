using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AKA.DataLayer;

namespace AKA.BusinessLayer.BookService
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _ctx;

        public BookService(LibraryContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<BookTitle>> GetAllBooks(int libraryId)
        {
            return await _ctx.Library_Book
                .Where(q => q.LibraryId == libraryId)
                .Select(q => q.BookTitle)
                .ToListAsync();
        }

        public async Task<List<BookTitle>> GetAvailableBooks(int libraryId)
        {
            return await GetAvailableBooksQuery(libraryId)
                .Select(q => q.BookTitle)
                .ToListAsync();
        }

        public async Task<List<BookTitle>> GetSignedOutBooks(int libraryId, int memberId)
        {
            return await _ctx
                .Library_Book
                .Where(q => q.LibraryId == libraryId)
                .Join(_ctx.BookSignedOuts, q => q.LibraryBookSId, q => q.LibraryBookSId, (lb, so) => new { lb, so })
                .Where(q => q.so.MemberId == memberId)
                .Select(q => q.lb.BookTitle)
                .ToListAsync();
        }

        private IQueryable<Library_Book> GetAvailableBooksQuery(int libraryId)
        {
            return _ctx
                .Library_Book
                .Where(q => q.LibraryId == libraryId)
                .Where(q => q.TotalPurchasedByLibrary > _ctx.BookSignedOuts
                    .Count(w => w.LibraryBookSId == q.LibraryBookSId));
        }
    }
}
