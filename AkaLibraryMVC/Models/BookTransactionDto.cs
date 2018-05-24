using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkaLibraryMVC.Models
{
    public class BookTransactionDto
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public int LibraryId { get; set; }
    }
}
