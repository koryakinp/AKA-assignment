namespace AKA.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Library_Book
    {
        [Key]
        public int LibraryBookSId { get; set; }

        public int LibraryId { get; set; }

        public int BookId { get; set; }

        public int TotalPurchasedByLibrary { get; set; }

        public virtual BookTitle BookTitle { get; set; }

        public virtual Library Library { get; set; }
    }
}
