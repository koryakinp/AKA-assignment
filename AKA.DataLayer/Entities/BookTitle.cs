namespace AKA.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BookTitle")]
    public partial class BookTitle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BookTitle()
        {
            Library_Book = new HashSet<Library_Book>();
        }

        [Key]
        public int BookId { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(50)]
        public string ISBN { get; set; }

        public DateTime? DateOfPublication { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Library_Book> Library_Book { get; set; }
    }
}
