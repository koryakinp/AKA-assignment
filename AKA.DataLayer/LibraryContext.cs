namespace AKA.DataLayer
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using AKA.Utils;

    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
            : base($"name={Consts.CONNECTION_NAME}")
        {
        }

        public virtual DbSet<BookSignedOut> BookSignedOuts { get; set; }
        public virtual DbSet<BookTitle> BookTitles { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<Library_Book> Library_Book { get; set; }
        public virtual DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookTitle>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<BookTitle>()
                .Property(e => e.ISBN)
                .IsUnicode(false);

            modelBuilder.Entity<BookTitle>()
                .HasMany(e => e.Library_Book)
                .WithRequired(e => e.BookTitle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Library>()
                .HasMany(e => e.Library_Book)
                .WithRequired(e => e.Library)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.BookSignedOuts)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);
        }
    }
}
