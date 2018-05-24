using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AKA.DataLayer;

namespace AKA.BusinessLayer.LibraryService
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryContext _ctx;

        public LibraryService(LibraryContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Library>> GetLibraries()
        {
            return await _ctx.Libraries.ToListAsync();
        }

        public async Task<Library> GetLinraryById(int id)
        {
            return await _ctx.Libraries.FindAsync(id);
        }
    }
}
