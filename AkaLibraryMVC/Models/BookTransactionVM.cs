using System.Collections.Generic;
using System.Web.Mvc;

namespace AkaLibraryMVC.Models
{
    public class SignOutBookVM
    {
        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> Members { get; set; }
        public int LibraryId { get; set; }
    }

    public class ReturnBookVM
    {
        public List<SelectListItem> Members { get; set; }
        public int LibraryId { get; set; }
    }
}